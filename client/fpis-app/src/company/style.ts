import {makeStyles,Theme, createStyles } from '@material-ui/core/styles'


export default makeStyles((theme: Theme) =>createStyles({
    formContainer:{
        margin:'1rm',
        '& .MuiTextField-root':{
            margin:'1rem 0'
        },
    },
    option: {
      fontSize: 15,
      '& > span': {
        marginRight: 10,
        fontSize: 18,
      },
    },
            button: {
                display: 'block',
                marginTop: theme.spacing(2),
              },
              formControl: {
                margin: theme.spacing(1),
                minWidth: 120,
              },
              root: {
                display: 'flex',
                '& > * + *': {
                  marginLeft: theme.spacing(2),
                },
                justifyContent: 'center'
              },
}));