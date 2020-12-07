import {GET_ALL_REGIONS, GET_ALL_CUSTOMERS, GET_ALL_SELLERS, CREATE_SESSION, DELETE_SESSION} from '../actions/types'

const isProduction = process.env.NODE_ENV === 'production'

const initialState = {
    customers: [],
    regions: [],
    sellers: [],
    session: {}

}

export default (state = initialState, action) => {
    if (!isProduction) {
        console.log(state)
        console.log(action)
    }
    switch (action.type) {
        case CREATE_SESSION:
            return ({...state, customers: [], session: action.payload})
        case DELETE_SESSION:
            return ({...state, session: {}})
        case GET_ALL_CUSTOMERS:
            return ({...state, customers: action.payload})
        case GET_ALL_REGIONS:
            return ({...state, regions: action.payload})
        case GET_ALL_SELLERS:
            return ({...state, sellers: action.payload})
        default:
            return state
    }
}

