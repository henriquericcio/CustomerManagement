import { createStore, applyMiddleware } from "redux";
import thunk from 'redux-thunk'
import reducer from "./reducers";
import { getAllRegions, getAllSellers} from './actions'

const store = createStore(reducer, applyMiddleware(thunk))

store.dispatch(getAllRegions())
store.dispatch(getAllSellers())
export default store