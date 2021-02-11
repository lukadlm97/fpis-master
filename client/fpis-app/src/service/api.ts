import {Contact} from '../model/Contact'
import {ContactResponse} from '../model/ContactResponse'
import axios from 'axios'
import { Company } from '../model/Company'
import {Location} from '../model/Location'
import {RequestForCooperation} from '../model/RequestForCooperation'

const baseUrl = "https://localhost:44360/api"

export async function getAllContacts(){
   let res = await fetch(baseUrl+'/company/contacts')
    let contacts = await res.json();
    var dataStr = JSON.stringify(contacts)

    console.log(dataStr)
    try{
        contacts = JSON.parse(dataStr)
    }catch(e){
        console.log(false)
    }
    
    return contacts.map((contact:any)=>({...contact}));
}

export async function getAllEmployee(){
    let res = await fetch(baseUrl+'/cooperation/employee')
    let employees = await res.json()
    var dataStr = JSON.stringify(employees)
    console.log(dataStr)
    try{
        employees = JSON.parse(dataStr)
    }catch(e){
        console.log(e)
    }
    return employees.map((employee:any)=>({...employee}))
}

export async function getAllCompanies(){
    let res = await fetch(baseUrl+"/company")
    let companies = await res.json()
    var datStr = JSON.stringify(companies)
    console.log(datStr)
    try{
        companies = JSON.parse(datStr)
    }catch(e){
        console.log(e)
    }
    return companies.map((company:any)=>({...company}))
}

export async function getAllRequests(){
    let res = await fetch(baseUrl+'/cooperation')
    let cooperations = await res.json()
    var dataStr = JSON.stringify(cooperations)
    try{
        cooperations = JSON.parse(dataStr)
    }catch(e){
        console.log(e)
    }
    return cooperations.map((cooperation:any)=>({...cooperation}))
}

export async function getAllCities(){
    let res = await fetch(baseUrl+'/company/cities')
    let cities = await res.json()
    var dataStr = JSON.stringify(cities)
    try{
        cities = JSON.parse(dataStr)
    }catch(e){
        console.log(e)
    }
    return cities.map((cities:any)=>({...cities}))
}

export async function addNewCompany(company:Company) {
    let {id,...comp} = company
    let res = await fetch(baseUrl+'/company',{
        method:'POST',
        body:JSON.stringify(comp),
        headers:{
            'Content-Type':'application/json'
        }
    });
    return await res.json()
}
export async function addNewRequest(request:RequestForCooperation){
    let {id,...req}=request
    let res =await fetch(baseUrl+'/cooperation',{
        method:'POST',
        body:JSON.stringify(req),
        headers:{
            'Content-Type':'application/json'
        }
    });
    return await res.json()
}

export async function addNewContact(contact:Contact,id:number){
    let res = await fetch(baseUrl+`/company/${id}/contact`,{
        method:'POST',
        body:JSON.stringify(contact),
        headers:{
            'Content-Type':'application/json'
        }
    });
    return await res.json();
}

export async function addNewLocation(location:Location,id:number){
    let res = await fetch(baseUrl+`/company/${id}/location`,{
        method:'POST',
        body:JSON.stringify(location),
        headers:{
            'Content-Type':'application/json'
        }
    });
    console.log(res)
    return await res.json();
}

export async function addMoreDescription(description:string,id:number){
    let res = await fetch(baseUrl+`/cooperation/${id}/appendRequest`,{
        method:'POST',
        body:JSON.stringify(description),
        headers:{
            'Content-Type':'application/json'
        }
    });
    return await res.json();
}

export async function updateRequest(request:RequestForCooperation){
    let {id,...req} = request;
    let res = await fetch(baseUrl+`/cooperation/${id}`,{
        method:'PUT',
        body:JSON.stringify(req),
        headers: {
            'Content-Type': 'application/json'
        }
    });
    return await res.json();
}

export async function removeCompany(id:number){
    let res = await fetch(baseUrl+`/company/${id}`,{
        method:'DELETE'
    });
}

export async function removeRequest(id:number){
    let res = await fetch(baseUrl+`/cooperation/${id}`,{
        method:'DELETE'
    })
}