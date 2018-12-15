import {RoverCamera} from './rovercamera'

export class Rover {
    id: number;
    name: string;
    landingDate: string;
    launchDate: string;
    status: string;
    maxSol: number;
    maxDate: string;
    totalPhotos: number;
    cameras: RoverCamera[];
}