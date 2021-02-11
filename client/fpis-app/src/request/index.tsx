import React,{useState,useRef} from 'react'
import { RequestForCooperation as RequestForCooperationItem, RequestForCooperation} from '../model/RequestForCooperation';
import {Company} from '../model/Company'
import {Employee} from '../model/Employee'
import CircularProgress from '@material-ui/core/CircularProgress';
import useStyles from '../company/style'

import RequestTable from './tableForRequests'
import RequestEntryForm from './requestForm'

interface Props{
    requests:RequestForCooperationItem[];
    companies:Company[];
    employees:Employee[];
    selectedRowRequest:number|null;
    loading:boolean;
    setSelectedRowRequest:(id:number|null)=>any;
    onAddRequest:(request:RequestForCooperationItem)=>Promise<any>;
    onRemoveRequest:()=>Promise<any>;
    onUpdateRequest:(request:RequestForCooperationItem,id:number)=>Promise<any>;
    onAddMoreDescription:(description:string)=>Promise<any>;
}

const scrollToRef = (ref:any)=>window.scrollTo(0,ref.current.offsetTop);


function RequestController(props:Props){
    const [visibleRequestForm,setVisibleRequestForm]= useState(false)
    const classes = useStyles();
    const executeScroll = () =>scrollToRef(myRef)
    const myRef = useRef(null);

    return(
        <>
        <RequestTable requests={props.requests}
                        selectedRowRequest={props.selectedRowRequest}
                        setSelectedRowRequest={props.setSelectedRowRequest}
                        setVisibleRequestForm={setVisibleRequestForm}
                        visibleRequestForm={visibleRequestForm}
                        scrollToForm={executeScroll}/>
                        
        <div className={classes.root}>
            {props.requests.length===0?<CircularProgress/>
               :null}
        </div>
        {visibleRequestForm||props.selectedRowRequest!=null?<RequestEntryForm employees={props.employees}
                            companies={props.companies}
                            selectedRowRequest={props.selectedRowRequest}
                            onAdd={props.onAddRequest}
                            onUpdate={props.onUpdateRequest}
                            onRemove={props.onRemoveRequest}
                            requests={props.requests}
                            onAddMoreDescription={props.onAddMoreDescription}
                            setVisibleRequestForm = {setVisibleRequestForm}/>:null}
        <div ref={myRef}>
        </div>
        </>
    );
}


export default RequestController;