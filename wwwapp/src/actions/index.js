import { createSessionSuccess, createSessionFail, deleteSessionSuccess, getAllRegionsSuccess, getAllCustomersSuccess, getAllSellersSuccess} from './internal'
import axios from 'axios'
import {navigate} from "hookrouter";

const isProduction = process.env.NODE_ENV === 'production'

const API_BASE = !isProduction?'http://localhost:5000':''

const CUSTOMERS_API_URL = API_BASE + '/customers'
const REGIONS_API_URL = API_BASE + '/regions'
const SESSION_API_URL = API_BASE + '/sessions'
const SELLERS_API_URL = API_BASE + '/sellers'

/* side effects */
export const createSession = session =>
    dispatch => axios.post(SESSION_API_URL, session)
        .then(response => dispatch(createSessionSuccess(response.data)))
        .then(response => navigate('/customers'))
        .catch(error => { 
            if(error.response.status === 401)
                dispatch(createSessionFail())
            else
                { throw (error) }
        })

export const deleteSession =  
    deleteSessionSuccess
    

export const getAllRegions = () =>
        dispatch => axios.get(REGIONS_API_URL)
            .then(response => dispatch(getAllRegionsSuccess(response.data)))
            .catch(error => { throw (error) })
    
export const getAllCustomers = (sessionKey, params) =>
    dispatch => axios.get(CUSTOMERS_API_URL, { 
        headers: { sessionKey },
        params
    })
    .then(response => dispatch(getAllCustomersSuccess(response.data)))
    .catch(error => { throw (error) })
    
export const getAllSellers = () =>
    dispatch => axios.get(SELLERS_API_URL)
    .then(response => dispatch(getAllSellersSuccess(response.data)))
    .catch(error => { throw (error) })
    