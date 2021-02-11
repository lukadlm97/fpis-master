import React from 'react'
import { Button,Box } from '@material-ui/core';
import DeleteIcon from '@material-ui/icons/Delete';
import ContactsIcon from '@material-ui/icons/Contacts';
import HouseIcon from '@material-ui/icons/House';
import AddIcon from '@material-ui/icons/Add';





interface Props{
    onRemoveCompany:()=>Promise<any>;
    selectedRowCompany:number|null;
    setVisibleCompanyForm:(visibleCompanyForm:boolean)=>any;
    visibleCompanyForm:boolean;
    setVisibleContactForm:(visibleContactForm:boolean)=>any;
    visibleContactForm:boolean;
    setVisibleAddressForm:(visibleAddressForm:boolean)=>any;
    visibleAddressForm:boolean;
}


function Functionality(props:Props){
    
    const onRemove = async (e:any)=>{
        e.preventDefault();
        await props.onRemoveCompany();
    }

    const onVisibleEntryForm = async (e:any)=>{
        e.preventDefault();
        props.setVisibleCompanyForm(!props.visibleCompanyForm);
    }

    const onVisibleContactForm = async (e:any)=>{
        e.preventDefault();
        props.setVisibleContactForm(!props.visibleContactForm);
    }

    const onVisibleAddressForm = async (e:any)=>{
        e.preventDefault();
        props.setVisibleAddressForm(!props.visibleAddressForm);
    }

    return(
        <Box ml={5} pl={2} mt={4} mb={4} justifyContent>
                <Button disabled={props.visibleCompanyForm || !(!props.selectedRowCompany)} variant="contained" color="default" onClick={onVisibleEntryForm}>
                    <AddIcon style={{fontSize:20,color:"#727bb8"}} />
                    Dodaj kompaniju
                </Button>
                {" "}
                <Button variant="contained" color="secondary" onClick={onRemove} disabled={props.selectedRowCompany === null}>
                    <DeleteIcon style={{fontSize:20,color:"#727bb8"}} />
                    Obrisi kompaniju
                </Button>
                {" "}
                <Button variant="contained" color="default"  disabled={props.selectedRowCompany === null || props.visibleAddressForm || props.visibleCompanyForm} onClick={onVisibleContactForm}>
                    <ContactsIcon style={{fontSize:20,color:"#727bb8"}} />
                    Dodaj novi kontakt
                </Button>
                {" "}
                <Button variant="contained" color="default"  disabled={props.selectedRowCompany === null || props.visibleCompanyForm || props.visibleContactForm} onClick={onVisibleAddressForm}>
                    <HouseIcon style={{fontSize:20,color:"#727bb8"}} />
                    Promeni adresu
                </Button>

        </Box>
    )
}

export default Functionality;