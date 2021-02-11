import {ContactType} from './enum/ContactType'
export class Contact{

    id:number;

    content:string;

    contactType:ContactType;

    constructor(id:number,content:string,contactType:ContactType){
        this.id=id;
        this.content=content;
        this.contactType=contactType;
    }
}