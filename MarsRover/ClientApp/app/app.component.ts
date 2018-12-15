import { Component, OnInit, OnDestroy } from '@angular/core';
import { NasaService } from './service/nasa.service'
import { Rover } from  './model/Rover';
import { Photo, DefaultPhoto } from './model/Photo';
import { NgbCarouselConfig, NgbDropdownConfig } from '@ng-bootstrap/ng-bootstrap';
import { DomSanitizer } from '@angular/platform-browser';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
    title = 'Mars Rover Photo Viewer';
    defaultData: DefaultPhoto;
    rovers: Rover[];
    photos: Photo[];
    selectedRover: string;
    dateString: string;
    base64Image: any;
    hasDefaultData: boolean;
    infoMessage: string;
    hasHttpError: boolean;
    photoSub: Subscription;
    roverSub: Subscription;
    defaultSub: Subscription;
    imageSub: Subscription;

    constructor(
        private nasaService: NasaService, 
        private config: NgbCarouselConfig
    ) {
        //interval on images
        config.interval = 2500;
    }

    ngOnInit(): void {
        this.loadDefaultData();
    }

    loadDefaultData() {
        this.hasHttpError = false;
        this.infoMessage = "Loading, please hang on";
        this.roverSub = this.nasaService.getRovers().subscribe(result => {
            this.rovers = result;
            //console.log(result);
            this.selectedRover = result[0].name;
            this.defaultSub = this.nasaService.getDefaultData(this.selectedRover).subscribe(result => {
                this.defaultData = result;
                this.hasDefaultData = true;
                this.photos = undefined;
                //console.log(this.defaultData);
            });
        });
    }

    getRover(roverName: string) {
        this.defaultData = null;
        this.selectedRover = roverName;
        this.defaultSub =this.nasaService.getDefaultData(roverName).subscribe(result => {
            //console.log(result);
            this.defaultData = result;
        }, result => {
            this.hasHttpError = true;
            this.infoMessage = result.error.message;
        });
    }

    onDateSelect(event) {
        this.infoMessage = "Loading Data, please Hang on";
        this.defaultData = undefined;
        let earthDate = `${event.year}-${event.month}-${event.day}`;

        this.photoSub = this.nasaService.getPhotos(this.selectedRover, earthDate).subscribe(result => {
            this.hasHttpError = false;
            this.photos = result;
            this.defaultData = new DefaultPhoto(earthDate, result);
            this.dateString = earthDate;
            //console.log(this.defaultData);
        }, result => {
            this.photos = undefined;
            this.hasDefaultData = false;
            this.hasHttpError = true;
            this.infoMessage = result.error.message;
        });
    }

    ngOnDestroy() {
        this.defaultSub.unsubscribe();
        this.roverSub.unsubscribe();
        this.photoSub.unsubscribe();
        this.imageSub.unsubscribe();
    }
    //getBase64ImageFromURL(url: string) {
    //    return Observable.create((observer: Observable<string>) => {
    //        // create an image object
    //        let img = new Image();
    //        img.crossOrigin = 'Anonymous';
    //        img.src = url;
    //        if (!img.complete) {
    //            // This will call another method that will create image from url
    //            img.onload = () => {
    //                observer.next(this.getBase64Image(img));
    //                observer.complete();
    //            };
    //            img.onerror = (err) => {
    //                observer.error(err);
    //            };
    //        } else {
    //            observer.next(this.getBase64Image(img));
    //            observer.complete();
    //        }
    //    });
    //}

    //getBase64Image(img: HTMLImageElement) {
    //    // We create a HTML canvas object that will create a 2d image
    //    var canvas = document.createElement("canvas");
    //    canvas.width = img.width;
    //    canvas.height = img.height;
    //    var ctx = canvas.getContext("2d");
    //    // This will draw image    
    //    ctx.drawImage(img, 0, 0);
    //    // Convert the drawn image to Data URL
    //    var dataURL = canvas.toDataURL("image/png");
    //    return dataURL.replace(/^data:image\/(png|jpg);base64,/, "");
    //}

    //getImageForDownload(imageUrl) {
    //    this.imageSub = this.getBase64ImageFromURL(imageUrl).subscribe(base64data => {    
    //        console.log(base64data);
    //        // this is the image as dataUrl
    //        return 'data:image/jpg;base64,' + base64data;
    //    });
    //}
}