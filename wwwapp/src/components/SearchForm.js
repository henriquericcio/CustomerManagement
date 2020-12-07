import React, {useState} from 'react'
import {useSelector, useDispatch} from 'react-redux'
import {getAllCustomers} from '../actions'

export default () => {
    const [value, setValue] = useState({name: '', region: ''})
    const session = useSelector(state => state.session)
    const regions = useSelector(state => state.regions)
    const sellers = useSelector(state => state.sellers)
    const [currentRegion, setCurrentRegion] = useState()

    const dispatch = useDispatch()

    const handleSubmit = e => {
        e.preventDefault()
        dispatch(getAllCustomers(session.id, {...value}))
    }

    const handleGeneralChanges = e => {
        setValue({...value, [e.target.id]: e.target.value})
    }
    const handleRegionChange = e => {
        setCurrentRegion(regions.find(r => r.id === e.target.value))
        setValue({...value, region: e.target.value, city: ''})
    }

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label className="label" htmlFor="Name">Name:</label>
                <input type="text" placeholder="Name" className="input" id="name" value={value.name}
                       onChange={handleGeneralChanges}/>
            </div>
            <div>
                <label htmlFor="classification">Choose a classification:</label>
                <select name="classification" id="classification" onChange={handleGeneralChanges}>
                    <option value="">All</option>
                    <option value="0">Vip</option>
                    <option value="1">Regular</option>
                    <option value="2">Sporadic</option>
                </select>
            </div>
            <div>
                <label htmlFor="gender">Choose a gender:</label>
                <select name="gender" id="gender" onChange={handleGeneralChanges}>
                    <option value="">All</option>
                    <option value="0">Male</option>
                    <option value="1">Female</option>
                </select>
            </div>
            <div>
                <label htmlFor="region">Choose a region:</label>
                <select name="region" id="region" onChange={handleRegionChange}>
                    <option value="">All</option>
                    {
                        regions.map(o => {
                            return (
                                <option value={o.id}>{o.name}</option>
                            )
                        })
                    }
                </select>
            </div>
            <div>
                <label htmlFor="city">Choose a city:</label>
                <select name="city" id="city" onChange={handleGeneralChanges}>
                    <option value="">All</option>
                    {
                        currentRegion?.cities?.map(o => {
                            return (
                                <option value={o.id}>{o.name}</option>
                            )
                        })
                    }
                </select>
            </div>
            {(session.role !== "Seller") && (
                <div>
                    <label htmlFor="seller">Choose a seller:</label>
                    <select name="seller" id="seller" onChange={handleGeneralChanges}>
                        <option value="">All</option>
                        {
                            sellers.map(o => {
                                return (
                                    <option value={o.id}>{o.email}</option>
                                )
                            })
                        }
                    </select>
                </div>
            )}

            <button className="button">Search</button>
        </form>
    )
}
