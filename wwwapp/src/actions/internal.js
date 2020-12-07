import { CREATE_SESSION, DELETE_SESSION, GET_ALL_REGIONS, GET_ALL_SELLERS, GET_ALL_CUSTOMERS } from './types'

/* pure functions */
export const createSessionSuccess = session => ({
    type: CREATE_SESSION,
    payload: session
})

export const createSessionFail = () => ({
    type: CREATE_SESSION,
    payload: {fail:true}
})

export const deleteSessionSuccess = () => ({
    type: DELETE_SESSION,
    payload: { }
})

export const getAllRegionsSuccess = regions => ({
    type: GET_ALL_REGIONS,
    payload: regions
})

export const getAllCustomersSuccess = customers => ({
    type: GET_ALL_CUSTOMERS,
    payload: customers
})

export const getAllSellersSuccess = sellers => ({
    type: GET_ALL_SELLERS,
    payload: sellers
})


