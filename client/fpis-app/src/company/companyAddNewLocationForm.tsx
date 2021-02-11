import React from 'react'
import TextField from '@material-ui/core/TextField'
import Button from '@material-ui/core/Button'
import Box from '@material-ui/core/Box'
import {Location} from '../model/Location'
import {City} from '../model/City'
import * as yup from 'yup'
import {useForm} from 'react-hook-form'
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import Select from '@material-ui/core/Select';
import FormControl from '@material-ui/core/FormControl';
import useStyles from './style'
import {Contact} from '../model/Contact'

interface Props{
    cities:City[];
    selectedRowCompany:number|null;
    onAddLocation:(location:Location,id:number|null)=>Promise<any>;
    setVisibleAddressForm:(visibleCompanyForm:boolean)=>any;  
}

const LocationEntrySchema = yup.object().shape({
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
    



function LocationForm(props:Props){
    const [open, setOpen] = React.useState(false);
    const [city, setCity] = React.useState(0);
    const {register,handleSubmit,errors,reset} = useForm<Location>({
        validationSchema:LocationEntrySchema
    })
    const classes=useStyles()

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

      const onSubmit = (data:Location):void=>{
        if(city != null){
            let cityForInsert:City = props.cities.find((element:City) => element.id === city)!;
            data.city = cityForInsert;    
        }   
        console.log(data)
        props.onAddLocation(new Location(0,data.city,data.streetName,data.number,data.storey,data.door),props.selectedRowCompany)
        reset()
    }

    const onBackAction = ()=>{
        props.setVisibleAddressForm(false);
    }


    return(
        <>
        <form onSubmit={handleSubmit(onSubmit)} noValidate className={classes.formContainer}>
        <TextField
                inputRef={register}
                label="Naziv ulice"
                name='streetName'
                variant="outlined"
                fullWidth
                error={!!errors.streetName}
                helperText={errors.streetName?errors.streetName:''}
                />
                <TextField
                inputRef={register}
                label="Broj u ulici"
                name="number"
                variant="outlined"
                fullWidth
                error={!!errors.number}
                helperText={errors.number?errors.number:''}
                />
                <TextField
                inputRef={register}
                label="Sprat na kom se nalazi kompanija"
                name="storey"
                variant="outlined"
                fullWidth
                error={!!errors.storey}
                helperText={errors.storey?errors.storey:''}
                />
                <TextField
                inputRef={register}
                label="Broj stana u kom se nalazi kompanija"
                name="door"
                variant="outlined"
                fullWidth
                error={!!errors.door}
                helperText={errors.door?errors.door:''}
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

                <Box display="flex" justifyContent="flex-end">
                    <Button  color="secondary" variant="contained" onClick={onBackAction}>Otkazi</Button>
                    {"  "}
                    <Button type="submit" color="primary" variant="contained" >Dodaj novu adresu</Button>
                </Box>
        </form>
        </>
    );
}

export default LocationForm;