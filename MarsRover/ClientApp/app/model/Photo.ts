import {Camera} from './Camera';
import { Rover } from './Rover';

export class Photo {
    id: number;
    sol: number;
    camera: Camera;
    imageSrc: string;
    earthDate: string;
    rover: Rover; 
}

export class DefaultPhoto {

    dateString: string;
    photos: Photo[];

    constructor(date: string, pics: Photo[]) {
        this.dateString = date;
        this.photos = pics;
    }
}