import React from 'react'
import {Company as CompanyItem} from '../model/Company'
import { makeStyles } from '@material-ui/core/styles';

import Paper from '@material-ui/core/Paper';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TablePagination from '@material-ui/core/TablePagination';
import TableRow from '@material-ui/core/TableRow';
import { withStyles, Theme, createStyles} from '@material-ui/core/styles';
import { Contact } from '../model/Contact';
import { ContactType } from '../model/enum/ContactType';

const StyledTableCell = withStyles((theme: Theme) =>
  createStyles({
    head: {
      backgroundColor: theme.palette.common.black,
      color: theme.palette.common.white,
      fontSize:18,
      fontWeight:"bold"
    },
    body: {
      fontSize: 14,
    },
  }),
)(TableCell);

const StyledTableRow = withStyles((theme: Theme) =>
  createStyles({
    root: {
      '&:nth-of-type(odd)': {
        backgroundColor: '#727bb8'
      },
    },
  }),
)(TableRow);

const useStyles = makeStyles({
    table: {
      minWidth: 650,
    },
  });

interface Props{
    companies:CompanyItem[];
    selectedRowCompany:number|null;
    setSelectedRowCompany:(id:number|null)=>any;
    setVisibleCompanyForm:(visibleCompanyForm:boolean)=>any;
    scrolToForm:()=>any;
}

const getEmail = (contacts:Contact[])=>{
    const emails = contacts.filter(contact => contact.contactType===ContactType.Email);
    if(emails.length === 0){
      return null;
    }
    return emails[emails.length-1];
}

function Company(props:Props){
    const classes = useStyles();

    const setSelectedRow = (id: number) => {
      if (props.selectedRowCompany === id)
      {
         props.setSelectedRowCompany(null);
         props.setVisibleCompanyForm(false);
      }
      else 
      {
        props.setSelectedRowCompany(id);
        props.scrolToForm();
      }
    }


    return(
        <>
            
                <TableContainer component={Paper}>
                    <Table  style={{border:'7px solid grey'}}
                            className={classes.table} 
                            aria-label="simple table">
                        <TableHead>
                            <TableRow>
                                <StyledTableCell>
                                    Naziv kompanije:
                                </StyledTableCell>
                                <StyledTableCell>
                                    Grad kompanije:
                                </StyledTableCell>
                                <StyledTableCell>
                                    Ulica kompanije:
                                </StyledTableCell>
                                <StyledTableCell>
                                    Email kompanije:
                                </StyledTableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {props.companies.map((company)=>(
                                <StyledTableRow key={company.name} style={props.selectedRowCompany===company.id?{backgroundColor:"#E91E63"}:{}}
                                onClick={() => setSelectedRow(company.id)}>
                                <StyledTableCell>
                                   {company.name}
                                </StyledTableCell>
                                <StyledTableCell>
                                    {company.locations.length===0?"Kompanija nema lokaciju":company.locations[company.locations.length-1].city.name}
                                </StyledTableCell>
                                <StyledTableCell>
                                {company.locations.length===0?"Kompanija nema lokaciju":company.locations[company.locations.length-1].streetName}  
                                </StyledTableCell>
                                <StyledTableCell>
                                {
                                 getEmail(company.contacts)===null?"Kompanija nema mail":getEmail(company.contacts)?.content
                                }
                                </StyledTableCell>
                                </StyledTableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>                
           
        </>
    );
}

export default Company