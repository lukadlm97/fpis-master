import { City } from "./City";

export class Location{
    id:number;

    city:City;

    streetName:string;

    number:number;

    storey:number;

    door:number;

    constructor(id:number,city:City,streetName:string,number:number,storey:number,door:number){
        this.id=id;
        this.city=city;
        this.streetName=streetName;
        this.number=number;
        this.storey=storey;
        this.door=door;
    }
}