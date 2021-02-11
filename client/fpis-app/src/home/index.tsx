import React from 'react'
import CompanyEntryForm from '../company/companyForm'
import {City} from '../model/City'
import {Company} from '../model/Company'

interface Props{
  cities:City[],
  onAddCompany:(company:Company)=>Promise<any>;
}

function Home(props:Props){
    return(
        <>
            <h1>Pocetna strana</h1>
           
        </>
    );
}

export default Home