import React from 'react'
import {Contact as ContactItem} from '../model/Contact'
import {Company as CompanyItem} from '../model/Company'
import TableContacts from './contactsDisplay'

interface Props{
    contacts:ContactItem[],
    companies:CompanyItem[]
}

function Contact(props:Props){
    return(
        <>
            <h1>Stranica za kontakte</h1>
            <TableContacts contacts={props.contacts} />
           </>
    );
}

export default Contact