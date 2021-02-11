import React, { useState, useEffect } from 'react';
import TextField from '@material-ui/core/TextField'
import Button from '@material-ui/core/Button'
import Box from '@material-ui/core/Box'
import { makeStyles } from '@material-ui/core/styles';
import RestoreIcon from '@material-ui/icons/Restore';



interface Props{
    updateSearch:(e:any)=>void;
    getSearch:(e:any)=>void;
}

const useStyles = makeStyles({
    txtField: {
      width: 275,
    },
  });
function SearchForm(props:Props){
    const classes = useStyles();
    

    return(
        <Box display="flex" justifyContent="flex-useState" mt={5} mb={5}>
            
            <form onSubmit={props.getSearch}>
                <TextField
                        onChange={props.updateSearch}
                        label="Unesite kriterijum pretrage:"
                        name="search"
                        variant="outlined"
                        className={classes.txtField}

                />
                <Box mt={2}>
                    <Button  color="default" type='submit'>
                        <RestoreIcon style={{fontSize:20,color:"#727bb8"}}/>
                        {" "}    
                        Restartuj pretragu
                    </Button>
                </Box>
            </form>
        </Box>
    );
}

export default SearchForm;