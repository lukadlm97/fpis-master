import { Position } from "./Position";

export class AtPostion{
    id:number;

    position:Position;

    dateFrom:Date;

    dateTo:Date;

    constructor(id:number,position:Position,dateFrom:Date,dateTo:Date){
        this.id = id;
        this.position=position;
        this.dateFrom = dateFrom;
        this.dateTo = dateTo;
    }
}