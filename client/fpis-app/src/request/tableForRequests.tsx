import React,{useRef} from 'react'
import { RequestForCooperation as RequestForCooperationItem} from '../model/RequestForCooperation';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import TitleIcon from '@material-ui/icons/Title';
import CalendarTodayIcon from '@material-ui/icons/CalendarToday';
import BusinessCenterIcon from '@material-ui/icons/BusinessCenter';


import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import Loading from '../universal_components/loading'
import Box from '@material-ui/core/Box'
import Moment from 'react-moment';
import moment from 'moment';

interface Props{
    requests:RequestForCooperationItem[];
    selectedRowRequest:number|null;
    setSelectedRowRequest:(id:number|null)=>any;
    setVisibleRequestForm:(visibleRequestForm:boolean)=>any;
    visibleRequestForm:boolean;
    scrollToForm:()=>any;
}

const useStyles = makeStyles({
    root: {
      minWidth: 275,
    },
    bullet: {
      display: 'inline-block',
      margin: '0 2px',
      transform: 'scale(0.8)',
    },
    title: {
      fontSize: 14,
    },
    pos: {
      marginBottom: 12,
    },
  });



function RequestTable(props:Props) {

    const classes = useStyles();
    const bull = <span className={classes.bullet}>•</span>;
 
    

    const setSelectedRow = (id: number) => {
        if (props.selectedRowRequest === id) {
            props.setSelectedRowRequest(null);
        }
        else{
            props.setVisibleRequestForm(true);
            props.setSelectedRowRequest(id);
            props.scrollToForm()
        }
    }

    const onAddRequest = ()=>{
        props.setVisibleRequestForm(!props.visibleRequestForm)
    }



    return(
        <>
            <h1>Stranica za zahteve za saradnju</h1>
           
           {props.requests.map((request)=>(
               <Box p={0.5} m={0.5}>

                   <Card className={classes.root} style={{border:'3px solid grey'}}  key={request.id}>
                       <CardContent style={props.selectedRowRequest===request.id?{backgroundColor:"#E91E63"}:{}}>
                           <Typography className={classes.title} color="textSecondary" gutterBottom>
                               <Box mr={1} pr={2}>
                                   <CalendarTodayIcon 
                                   style={{fontSize:15,color:"#727bb8"}}
                                   />
                                     {" "}
                                     <div>
                                         {moment(request.date).format('DD-MM-YYYY')}
                                     </div>
                               </Box>
                           </Typography>
                           <Typography variant="h5" component="h2">
                               <Box  mr={10} pr={2}>
                                   <TitleIcon 
                                   style={{fontSize:20,color:"#727bb8"}}
                                   />
                                   {" "}
                               {request.title}
                               </Box>
                           </Typography>
                           <Typography className={classes.pos} color="textSecondary">
                               <Box  mr={10} pr={2}>
                                   <BusinessCenterIcon 
                                   style={{fontSize:15,color:"#727bb8"}}
                                   />
                                     {" "}
                               {request.company?request.company.name:""}
                               </Box>
                           </Typography>
                           <Typography variant="body2" component="p">
                               {request.descriptionOfProposal}
                           </Typography>
                       </CardContent>
                           <CardActions>
                               <Button size="small" onClick={()=>setSelectedRow(request.id)}>Detaljnije</Button>
                           </CardActions>
               </Card>
            </Box>
            ))}

            <Box display="flex" justifyContent="flex-end">
                    <Button  color="primary" variant="contained" onClick={onAddRequest} disabled={props.selectedRowRequest!==null}>Dodaj zahtev</Button>
            </Box>
            
        </>
    );
}

export default RequestTable