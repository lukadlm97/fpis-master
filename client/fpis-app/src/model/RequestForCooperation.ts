import { Employee } from "./Employee";
import {Company} from './Company';

export class RequestForCooperation{
    id:number;

    title:string;

    descriptionOfProposal:string;

    date:Date;

    company:Company|null;

    employee:Employee|null;

    constructor(id:number,title:string,descriptionOfProposal:string,date:Date,company:Company|null,employee:Employee|null){
        this.id=id;
        this.title=title;
        this.descriptionOfProposal=descriptionOfProposal;
        this.date=date;
        this.company=company;
        this.employee=employee;
    }
}