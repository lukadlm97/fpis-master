import {Location} from './Location'
import {Contact} from './Contact'
export class Company{
    id:number;

    locations:Location[];

    contacts:Contact[];

    name:string;

    username:string;

    password:string;

    constructor(id:number,locations:Location[],contacts:Contact[],name:string,username:string,password:string){
        this.id=id;
        this.locations=locations;
        this.contacts=contacts;
        this.name=name;
        this.username=username;
        this.password=password;
    }
}