export class Employee{
    id:number;

    firstName:string;

    lastName:string;

    personalNumber:string;

    expirience:number;


    constructor(id:number,firstName:string,lastName:string,personalNumber:string,expirience:number){
        this.id=id;
        this.firstName=firstName;
        this.lastName=lastName;
        this.personalNumber=personalNumber;
        this.expirience=expirience;
    }

}