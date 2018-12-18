var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { DOCUMENT } from '@angular/common';
var NasaService = /** @class */ (function () {
    function NasaService(httpClient, document) {
        this.httpClient = httpClient;
        this.document = document;
        this.baseUrl = "http://localhost:8841";
        this.baseUrl = this.document.location.href + "api/";
        this.headers = new HttpHeaders();
        this.headers.append('Access-Control-Allow-Headers', 'Content-Type');
        this.headers.append('Access-Control-Allow-Methods', 'GET');
        this.headers.append('Access-Control-Allow-Origin', '*');
    }
    NasaService.prototype.getRovers = function () {
        var url = this.baseUrl + "nasa/rovers";
        return this.httpClient.get(url);
    };
    NasaService.prototype.getPhotos = function (rover, earthDate) {
        var url = this.baseUrl + "nasa/photos?rover=" + rover + "&earthDate=" + earthDate;
        return this.httpClient.get(url);
    };
    NasaService.prototype.getDefaultData = function (rover) {
        var url = this.baseUrl + "nasa/defaultDate?rover=" + rover;
        return this.httpClient.get(url);
    };
    NasaService.prototype.getImage2 = function (imageUrl) {
        return this.httpClient.get(imageUrl, { headers: this.headers, responseType: 'blob' });
    };
    //getImage(imageUrl: string, photoId: string) {
    //    return this.httpClient.get(imageUrl, { headers: this.headers, observe: 'response', responseType: 'blob' })
    //        .pipe(map((res) => {
    //            return new Blob([res.body], { type: res.headers.get('Content-Type') });
    //        }));
    //}
    NasaService.prototype.getImage = function (imageUrl, photoId) {
        var url = this.baseUrl + "image/download?requestUri=" + imageUrl + "&filename=" + photoId + ".jpg";
        alert(url);
        return this.httpClient.post(url, { headers: this.headers, observe: 'response', responseType: 'blob' })
            .pipe(map(function (res) {
            console.log(res);
            alert("ok");
            return new Blob([res.body], { type: res.headers.get('Content-Type') });
        }));
        //return this.httpClient.get(imageUrl, { headers: this.headers, observe: 'response', responseType: 'blob' })
        //    .pipe(map((res) => {
        //        return new Blob([res.body], { type: res.headers.get('Content-Type') });
        //    }));
    };
    NasaService = __decorate([
        Injectable({
            providedIn: 'root'
        }),
        __param(1, Inject(DOCUMENT)),
        __metadata("design:paramtypes", [HttpClient, Document])
    ], NasaService);
    return NasaService;
}());
export { NasaService };
//# sourceMappingURL=nasa.service.js.map