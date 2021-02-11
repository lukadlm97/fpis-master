import React, { useState } from 'react'
import {Contact as ContactItem} from '../model/Contact'
import {ContactType} from '../model/enum/ContactType'
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import ExpansionPanel from '@material-ui/core/ExpansionPanel';
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails';
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary';
import Typography from '@material-ui/core/Typography';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import FacebookIcon from '@material-ui/icons/Facebook';
import TwitterIcon from '@material-ui/icons/Twitter';
import EmailIcon from '@material-ui/icons/Email';
import InstagramIcon from '@material-ui/icons/Instagram';
import PrintIcon from '@material-ui/icons/Print';
import PhoneIphoneIcon from '@material-ui/icons/PhoneIphone';
import PhoneIcon from '@material-ui/icons/Phone';
import CircularProgress from '@material-ui/core/CircularProgress';


const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      width: '100%',
    },
    root1:{
        display: 'flex',
        '& > * + *': {
          marginLeft: theme.spacing(2),
        },
        justifyContent: 'center'
    },
    heading: {
      fontSize: theme.typography.pxToRem(15),
      flexBasis: '33.33%',
      flexShrink: 0,
    },
    secondaryHeading: {
      fontSize: theme.typography.pxToRem(15),
      color: theme.palette.text.secondary,
    },
  }),
);



interface Props{
    contacts:ContactItem[]
}


function TableContacts(props:Props){
    const classes = useStyles();
        const [expanded, setExpanded] = React.useState<string | false>(false);

        const handleChange = (panel: string) => (event: React.ChangeEvent<{}>, isExpanded: boolean) => {
            setExpanded(isExpanded ? panel : false);
        };
   

    return(
        <>
            <div className={classes.root}>
            <ExpansionPanel expanded={expanded === 'panel1'} onChange={handleChange('panel1')}>
                <ExpansionPanelSummary
                expandIcon={<ExpandMoreIcon />}
                aria-controls="panel1bh-content"
                id="panel1bh-header"
                >
                <Typography className={classes.heading}>Emailovi:</Typography>
                <Typography className={classes.secondaryHeading}><EmailIcon style={{color : '#727bb8'}}/></Typography>
                </ExpansionPanelSummary>
                <ExpansionPanelDetails>
                {props.contacts.length===0?
                <div className={classes.root1}>
                    <CircularProgress/>
                </div>
                :
                <Typography>
                {
                   props.contacts.map(cont=>{
                    if(cont.contactType===ContactType.Email)
                        return(<h4>{cont.content}</h4>)
                    })
                }
                </Typography>
                }
                </ExpansionPanelDetails>
            </ExpansionPanel>
            <ExpansionPanel expanded={expanded === 'panel2'} onChange={handleChange('panel2')}>
                <ExpansionPanelSummary
                expandIcon={<ExpandMoreIcon />}
                aria-controls="panel2bh-content"
                id="panel2bh-header"
                >
                <Typography className={classes.heading}>Facebook:</Typography>
                <Typography className={classes.secondaryHeading}><FacebookIcon style={{color : '#727bb8'}}/> </Typography>
                </ExpansionPanelSummary>
                <ExpansionPanelDetails>
                {props.contacts.length===0?
                <div className={classes.root1}>
                    <CircularProgress/>
                </div>
                :
                <Typography>
                    {props.contacts.map(cont=>{
                    if(cont.contactType===ContactType.Facebook)
                        return(<h4>{cont.content}</h4>)
                    })}
                </Typography>
                }
                </ExpansionPanelDetails>
            </ExpansionPanel>

            <ExpansionPanel expanded={expanded === 'panel3'} onChange={handleChange('panel3')}>
                <ExpansionPanelSummary
                expandIcon={<ExpandMoreIcon />}
                aria-controls="panel3bh-content"
                id="panel3bh-header"
                >
                <Typography className={classes.heading}>Instagram:</Typography>
                <Typography className={classes.secondaryHeading}><InstagramIcon style={{color : '#727bb8'}}/></Typography>
                </ExpansionPanelSummary>
                <ExpansionPanelDetails>
                {props.contacts.length===0?
                <div className={classes.root1}>
                    <CircularProgress/>
                </div>
                :
                <Typography>
                    {props.contacts.map(cont=>{
                    if(cont.contactType===ContactType.Instagram)
                        return(<h4>{cont.content}</h4>)
                    })}
                </Typography>
                }
                </ExpansionPanelDetails>
            </ExpansionPanel>
            <ExpansionPanel expanded={expanded === 'panel4'} onChange={handleChange('panel4')}>
                <ExpansionPanelSummary
                expandIcon={<ExpandMoreIcon />}
                aria-controls="panel4bh-content"
                id="panel4bh-header"
                >
                <Typography className={classes.heading}>Twitter:</Typography>
                <Typography className={classes.secondaryHeading}><TwitterIcon style={{color : '#727bb8'}}/></Typography>
                </ExpansionPanelSummary>
                <ExpansionPanelDetails>
                {props.contacts.length===0?
                <div className={classes.root1}>
                    <CircularProgress/>
                </div>
                :
                <Typography>
                    {props.contacts.map(cont=>{
                    if(cont.contactType===ContactType.Twitter)
                        return(<h4>{cont.content}</h4>)
                    })}
                </Typography>
                }
                </ExpansionPanelDetails>
            </ExpansionPanel>
            <ExpansionPanel expanded={expanded === 'panel5'} onChange={handleChange('panel5')}>
                <ExpansionPanelSummary
                expandIcon={<ExpandMoreIcon />}
                aria-controls="panel5bh-content"
                id="panel5bh-header"
                >
                <Typography className={classes.heading}>Fax:</Typography>
                <Typography className={classes.secondaryHeading}><PrintIcon style={{color : '#727bb8'}}/></Typography>
                </ExpansionPanelSummary>
                <ExpansionPanelDetails>
                {props.contacts.length===0?
                <div className={classes.root1}>
                    <CircularProgress/>
                </div>
                :
                <Typography>
                    {props.contacts.map(cont=>{
                    if(cont.contactType===ContactType.Fax)
                        return(<h4>{cont.content}</h4>)
                    })}
                </Typography>
                }
                </ExpansionPanelDetails>
            </ExpansionPanel>
            <ExpansionPanel expanded={expanded === 'panel6'} onChange={handleChange('panel6')}>
                <ExpansionPanelSummary
                expandIcon={<ExpandMoreIcon />}
                aria-controls="panel6bh-content"
                id="panel6bh-header"
                >
                <Typography className={classes.heading}>Mobilni telefon:</Typography>
                <Typography className={classes.secondaryHeading}><PhoneIphoneIcon style={{color : '#727bb8'}}/></Typography>
                </ExpansionPanelSummary>
                <ExpansionPanelDetails>
                {props.contacts.length===0?
                <div className={classes.root1}>
                    <CircularProgress/>
                </div>
                :
                <Typography>
                    {props.contacts.map(cont=>{
                    if(cont.contactType===ContactType.MobilePhone)
                        return(<h4>{cont.content}</h4>)
                    })}
                </Typography>
                }
                </ExpansionPanelDetails>
            </ExpansionPanel>
            <ExpansionPanel expanded={expanded === 'panel7'} onChange={handleChange('panel7')}>
                <ExpansionPanelSummary
                expandIcon={<ExpandMoreIcon />}
                aria-controls="panel7bh-content"
                id="panel7bh-header"
                >
                <Typography className={classes.heading}>Fiksni telefon:</Typography>
                <Typography className={classes.secondaryHeading}><PhoneIcon style={{color : '#727bb8'}}/></Typography>
                </ExpansionPanelSummary>
                <ExpansionPanelDetails>
                {props.contacts.length===0?
                <div className={classes.root1}>
                    <CircularProgress/>
                </div>
                :
                <Typography>
                    {props.contacts.map(cont=>{
                    if(cont.contactType===ContactType.Phone)
                        return(<h4>{cont.content}</h4>)
                    })}
                </Typography>
                }
                </ExpansionPanelDetails>
            </ExpansionPanel>
            </div>
          
        </>
    );
}

export default TableContacts;