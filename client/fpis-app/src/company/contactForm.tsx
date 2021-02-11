import React from 'react'
import {Contact} from '../model/Contact'
import FormControl from '@material-ui/core/FormControl';
import Button from '@material-ui/core/Button'
import Box from '@material-ui/core/Box'
import {useForm} from 'react-hook-form'
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import Select from '@material-ui/core/Select';
import TextField from '@material-ui/core/TextField'
import useStyles from './style'
import * as yup from 'yup'

import {ContactType} from '../model/enum/ContactType'


interface Props{
    onAddContact:(contact:Contact,id:number|null)=>Promise<any>;
    selectedRowCompany:number|null;
    setVisibleContactForm:(visibleCompanyForm:boolean)=>any;  
}

const ContactEntrySchema = yup.object().shape({
    id:yup.number()
        .default(0),
    content:yup.string()
            .required(),
    contactType:yup.mixed()
            .oneOf([
                'Email',
                'Phone',
                'MobilePhone',
                'Fax',
                'LinkedIn',
                'Instagram',
                'Facebook',
                'Twitter'
            ]),      
})


function ContactFrom(props:Props){
    const [contact,setContact] = React.useState(0)
    const [openContact,setOpenContact] = React.useState(false)
    const classes=useStyles()
    const {register,handleSubmit,errors,reset} = useForm<Contact>({
        validationSchema:ContactEntrySchema
    })

    const onSubmit = (data:Contact):void=>{
            if(contact!= null){
                data.contactType = contact
            }
            console.log(data);
            props.onAddContact(new Contact(0,data.content,data.contactType),props.selectedRowCompany)
            reset()
    }

    const handleChangeContact = (event:React.ChangeEvent<{value:any}>)=>{
        setContact(event.target.value)
    }

    const handleCloseContact = () => {
      setOpenContact(false);
    };
  
    const handleOpenContact = () => {
      setOpenContact(true);
    };

    const BackAction = () =>{
        props.setVisibleContactForm(false);
    }

    return(

        <form onSubmit={handleSubmit(onSubmit)} noValidate className={classes.formContainer}>
             <FormControl fullWidth className={classes.formControl}>
                        <InputLabel id="demo-controlled-open-select-label">Vrsta kontakta</InputLabel>
                        <Select
                        labelId="demo-controlled-open-select-label"
                        id="demo-controlled-open-select"
                        open={openContact}
                        onClose={handleCloseContact}
                        onOpen={handleOpenContact}
                        value={contact}
                        onChange={handleChangeContact}
                        >
                            {
                                Object.keys(ContactType)
                                            .filter(a=>parseInt(a,10)>=0)
                                            .map((a:any)=>(
                                            <MenuItem value={a} key={a}>
                                                {ContactType[a]}
                                            </MenuItem> 
                                            ))
                            }
                          
                        </Select>
                </FormControl>
                
                <TextField
                inputRef={register}
                label="Sadrzaj kontakta"
                name="content"
                variant="outlined"
                fullWidth
                error={!!errors.content}
                helperText={errors.content?errors.content:''}
                />

                <Box display="flex" justifyContent="flex-end">
                    <Button  color="secondary" variant="contained" onClick={BackAction}>Otkazi</Button>
                    {"  "}
                    <Button type="submit" color="primary" variant="contained" >Dodaj novi kontakt</Button>
                </Box>
        </form>
    );
}



export default ContactFrom;