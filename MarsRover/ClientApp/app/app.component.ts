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
            this.selectedRover = result[0].name;
            this.defaultSub = this.nasaService.getDefaultData(this.selectedRover).subscribe(result => {
                this.defaultData = result;
                this.hasDefaultData = true;
                this.photos = undefined;
            });
        });
    }

    getRover(roverName: string) {
        this.defaultData = null;
        this.selectedRover = roverName;
        this.defaultSub =this.nasaService.getDefaultData(roverName).subscribe(result => {
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

    displayDownloadInfo(downloadLink: string, photoId: string) {       
        this.nasaService.getImage(downloadLink, photoId).subscribe(
            (res) => {
                console.log(res);
                const a = document.createElement('a');
                a.href = URL.createObjectURL(res);
                a.download = photoId + ".jpg";
                document.body.appendChild(a);
                a.click();
            }, error => {
                console.log(error);
            }
        );
    }
}