import React from 'react';
import TextField from '@material-ui/core/TextField'
import Button from '@material-ui/core/Button'
import Box from '@material-ui/core/Box'
import {useForm} from 'react-hook-form'
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import Select from '@material-ui/core/Select';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import FormControl from '@material-ui/core/FormControl';
import Autocomplete from '@material-ui/lab/Autocomplete';

import {ContactType} from '../model/enum/ContactType'
import {Contact} from '../model/Contact'
import {City} from '../model/City'
import {Company} from '../model/Company'
import * as yup from 'yup'
import useStyles from './style'

const CompanyEntrySchema = yup.object().shape({
    id:yup.number()
        .default(0),
    name:yup
    .string()
    .trim()
    .required('Obavezno polje')
    .min(2,'Naziv kompanije mora imati najmanje dva karaktera')
    .max(30,"Naziv kompanije je predugacak"),
    locations:yup.array()
                    .of(
                        yup.object().shape({
                            id:yup.number()
                                .default(0),
                            streetName:yup.string()
                                        .required('Naziv ulice je obavezno polje')
                                        .min(3,'Naziv ulice mora biti duzi od 3 karaktera.')
                                        .max(30,'Naziv ulice ne moze biti duzi od 30 karaktera'),
                            number:yup.number()
                                        .required('Morate uneti broj u ulici. (0-ukoliko je bez broja)'),
                            storey:yup.number()
                                        .notRequired()
                                        .default(0),
                            door:yup.number()
                                        .notRequired()
                                        .default(0),            
                            city:yup.object().shape({
                                            id:yup.number(),
                                            name:yup.string(),
                                            postalCode:yup.string()
                            }).required("Morate odabrati grad!")
                        })
                    ).required('Mora se uneti adresa!'),
        contacts:yup.array()
        .of(
            yup.object().shape({
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
        )
});

interface Props{
    cities:City[],
    onAddCompany:(company:Company)=>Promise<any>;
    setVisibleCompanyForm:(visibleCompanyForm:boolean)=>any;  
}

interface State{
    contacts:Contact[]
}

function CompanyEntryForm (props:Props){

    const {register,handleSubmit,errors,reset} = useForm<Company>({
        validationSchema:CompanyEntrySchema
    })


    const onSubmit = (data:Company):void=>{
        if(city != null){
            let cityForInsert:City = props.cities.find((element:City) => element.id === city)!;
            data.locations[0].city = cityForInsert;    
        }
        if(contact != null){
            data.contacts[0].contactType = contact
        }
        
        console.log(data)
        props.onAddCompany(new Company(0,data.locations,data.contacts,data.name,data.name+"new",data.name+"new"))
        reset()
    }
    const classes=useStyles()

    const [open, setOpen] = React.useState(false);
    const [city, setCity] = React.useState(0);
    const [contact,setContact] = React.useState(0)
    const [openContact,setOpenContact] = React.useState(false)
   

    const handleClose = () => {
        setOpen(false);
      };
    
      const handleOpen = () => {
        setOpen(true);
      };
      const handleChange = (event: React.ChangeEvent<{ value: any }>) => {
        let city_id = event.target.value
        setCity(city_id);
      };

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
          props.setVisibleCompanyForm(false);
      }

    return(
        <>
            <form onSubmit={handleSubmit(onSubmit)} noValidate className={classes.formContainer}>
                <TextField
                inputRef={register}
                label="Naziv kompanije"
                name="name"
                variant="outlined"
                fullWidth
                error={!!errors.name}
                helperText={errors.name?errors.name.message:''}
                />

                <TextField
                inputRef={register}
                label="Naziv ulice"
                name='locations[0].streetName'
                variant="outlined"
                fullWidth
                error={!!errors.locations}
                helperText={errors.locations?errors.locations.values:''}
                />
                <TextField
                inputRef={register}
                label="Broj u ulici"
                name="locations[0].number"
                variant="outlined"
                fullWidth
                error={!!errors.locations}
                helperText={errors.locations?errors.locations.values:''}
                />
                <TextField
                inputRef={register}
                label="Sprat na kom se nalazi kompanija"
                name="locations[0].storey"
                variant="outlined"
                fullWidth
                error={!!errors.locations}
                helperText={errors.locations?errors.locations.values:''}
                />
                <TextField
                inputRef={register}
                label="Broj stana u kom se nalazi kompanija"
                name="locations[0].door"
                variant="outlined"
                fullWidth
                error={!!errors.locations}
                helperText={errors.locations?errors.locations.values:''}
                />


                <FormControl fullWidth className={classes.formControl}>
                        <InputLabel id="demo-controlled-open-select-label">Grad odakle je kompanija</InputLabel>
                        <Select
                        labelId="demo-controlled-open-select-label"
                        id="demo-controlled-open-select"
                        open={open}
                        onClose={handleClose}
                        onOpen={handleOpen}
                        value={city}
                        onChange={handleChange}
                        defaultValue = {props.cities[1]}
                        >
                            {props.cities.map((cityM)=>(
                            
                            <MenuItem value={cityM.id} key={cityM.name}>
                                {cityM.name}
                            </MenuItem>
                            ))}
                        </Select>
                </FormControl>

                


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
                name="contacts[0].content"
                variant="outlined"
                fullWidth
                error={!!errors.contacts}
                helperText={errors.contacts?errors.contacts.values:''}
                />


                <Box display="flex" justifyContent="flex-end">
                <Button color="secondary" variant="contained" onClick={BackAction}>Otkazi</Button>
                {"  "}
                    <Button type="submit" color="primary" variant="contained" >Dodaj kompaniju</Button>
                </Box>
            </form>
        </>
    )
}

export default CompanyEntryForm;