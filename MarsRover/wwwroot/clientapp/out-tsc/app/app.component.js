var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { NasaService } from './service/nasa.service';
import { DefaultPhoto } from './model/Photo';
import { NgbCarouselConfig } from '@ng-bootstrap/ng-bootstrap';
var AppComponent = /** @class */ (function () {
    function AppComponent(nasaService, config) {
        this.nasaService = nasaService;
        this.config = config;
        this.title = 'Mars Rover Photo Viewer';
        //interval on images
        config.interval = 2500;
    }
    AppComponent.prototype.ngOnInit = function () {
        this.loadDefaultData();
    };
    AppComponent.prototype.loadDefaultData = function () {
        var _this = this;
        this.hasHttpError = false;
        this.infoMessage = "Loading, please hang on";
        this.roverSub = this.nasaService.getRovers().subscribe(function (result) {
            _this.rovers = result;
            _this.selectedRover = result[0].name;
            _this.defaultSub = _this.nasaService.getDefaultData(_this.selectedRover).subscribe(function (result) {
                _this.defaultData = result;
                _this.hasDefaultData = true;
                _this.photos = undefined;
            });
        });
    };
    AppComponent.prototype.getRover = function (roverName) {
        var _this = this;
        this.defaultData = null;
        this.selectedRover = roverName;
        this.defaultSub = this.nasaService.getDefaultData(roverName).subscribe(function (result) {
            _this.defaultData = result;
        }, function (result) {
            _this.hasHttpError = true;
            _this.infoMessage = result.error.message;
        });
    };
    AppComponent.prototype.onDateSelect = function (event) {
        var _this = this;
        this.infoMessage = "Loading Data, please Hang on";
        this.defaultData = undefined;
        var earthDate = event.year + "-" + event.month + "-" + event.day;
        this.photoSub = this.nasaService.getPhotos(this.selectedRover, earthDate).subscribe(function (result) {
            _this.hasHttpError = false;
            _this.photos = result;
            _this.defaultData = new DefaultPhoto(earthDate, result);
            _this.dateString = earthDate;
        }, function (result) {
            _this.photos = undefined;
            _this.hasDefaultData = false;
            _this.hasHttpError = true;
            _this.infoMessage = result.error.message;
        });
    };
    AppComponent.prototype.ngOnDestroy = function () {
        this.defaultSub.unsubscribe();
        this.roverSub.unsubscribe();
        this.photoSub.unsubscribe();
        this.imageSub.unsubscribe();
    };
    AppComponent.prototype.displayDownloadInfo = function (downloadLink, photoId) {
        this.nasaService.getImage(downloadLink, photoId).subscribe(function (res) {
            console.log(res);
            var a = document.createElement('a');
            a.href = URL.createObjectURL(res);
            a.download = photoId + ".jpg";
            document.body.appendChild(a);
            a.click();
        }, function (error) {
            console.log(error);
        });
    };
    AppComponent = __decorate([
        Component({
            selector: 'app-root',
            templateUrl: './app.component.html',
            styleUrls: ['./app.component.css']
        }),
        __metadata("design:paramtypes", [NasaService,
            NgbCarouselConfig])
    ], AppComponent);
    return AppComponent;
}());
export { AppComponent };
//# sourceMappingURL=app.component.js.map