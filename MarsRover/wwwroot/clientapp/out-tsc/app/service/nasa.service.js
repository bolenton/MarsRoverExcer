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
import { HttpClient } from '@angular/common/http';
import { DOCUMENT } from '@angular/common';
var NasaService = /** @class */ (function () {
    function NasaService(httpClient, document) {
        this.httpClient = httpClient;
        this.document = document;
        this.baseUrl = "http://localhost:8841";
        this.baseUrl = this.document.location.href + "api/nasa/";
        console.log(this.baseUrl);
    }
    NasaService.prototype.getRovers = function () {
        var url = this.baseUrl + "rovers";
        return this.httpClient.get(url);
    };
    NasaService.prototype.getPhotos = function (rover, earthDate) {
        var url = this.baseUrl + "photos?rover=" + rover + "&earthDate=" + earthDate;
        return this.httpClient.get(url);
    };
    NasaService.prototype.getDefaultData = function (rover) {
        var url = this.baseUrl + "defaultDate?rover=" + rover;
        return this.httpClient.get(url);
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