import React ,{useState} from 'react';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import {Switch,Route,Redirect,Link} from 'react-router-dom'
import Container from '@material-ui/core/Container';
import BusinessIcon from '@material-ui/icons/Business';
import DescriptionIcon from '@material-ui/icons/Description';
import CreateIcon from '@material-ui/icons/Create';
import Box from '@material-ui/core/Box'
import ContactMailIcon from '@material-ui/icons/ContactMail';



import Home from '../home'
import Company from '../company'
import Request from '../request/tableForRequests'
import useStyles from '../style'
import HomeIcon from './homeicon'
import Contact from '../contact'

const NavBar:React.FC = () =>{

    const [isOpen,setOpen] = useState(false)

    const classes = useStyles();

    return(
        <>
        <div className={classes.root}>
            <AppBar style={{background : '#727bb8'}} position="static" >
                <div>
                    <Box justifyContent="left"
                        display="flex"
                        alignItems="center"
                        css={{ height: 60 }}
                        p={0.7}
                        m={0.7}
                        mx="auto"
                        >
                    <Button component={Link} to="/">
                        <HomeIcon color="action" 
                        style={{ fontSize: 40,color:"#161824" }}/> 
                        Pocetna
                        </Button>
                    <Button component={Link} to="/company">
                        <BusinessIcon color="action"
                        style={{fontSize:40,color:"#161824"}}/>
                        Kompanije
                    </Button>
                    <Button component={Link} to="/request">
                        <DescriptionIcon color="action"
                        style={{fontSize:40,color:"#161824"}}/>
                        Zahtevi
                    </Button>
                    <Button component={Link} to="/contacts">
                        <ContactMailIcon color="action"
                        style={{fontSize:40,color:"#161824"}}/>
                        Kontakti
                    </Button>
                    </Box>
                </div>
            </AppBar>
            <div>
                <Container maxWidth="sm">
                    <Switch>
                        <Route exact path="/" component={Home}/>
                        <Route exact path="/company" component={Company} />
                        <Route exact path="/request" component={Request} />
                        <Route exact path="/contact" component={Contact}/>
                        <Redirect to="/"/>
                    </Switch>
                </Container>
            </div>
        </div>
        </>
    );
}

export default NavBar