import React from 'react'
import {useSelector} from 'react-redux'
import {navigate} from "hookrouter";

export default () => {
    const customers = useSelector(state => state.customers)
    const formatDate = date => new Date(date).toISOString().slice(0, 10)

    //guard
    const currentSession = useSelector(state => state.session)
    if (Object.entries(currentSession).length === 0)
        navigate('/login')

    return (
        <div className="customer-list">
            <table>
                <thead>
                <tr>
                    <th>Classification</th>
                    <th>Name</th>
                    <th>Phone</th>
                    <th>Gender</th>
                    <th>City</th>
                    <th>Region</th>
                    <th>Last Purchase</th>
                    <th>Seller</th>
                </tr>
                </thead>
                <tbody>
                {
                    customers.map(o => {
                        return (
                            <tr key={o.id}>
                                <td>{o.classification}</td>
                                <td>{o.name}</td>
                                <td>{o.phone}</td>
                                <td>{o.gender}</td>
                                <td>{o.city}</td>
                                <td>{o.region}</td>
                                <td>{formatDate(o.lastPurchase)}</td>
                                <td>{o.seller}</td>
                            </tr>
                        )
                    })
                }
                </tbody>
            </table>
        </div>
    )
}