import React, { useState } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { createSession } from '../actions'

export default () => {
    const [value, setValue] = useState({ email: '', password: ''})
    const currentSession = useSelector(state => state.session)
    const dispatch = useDispatch()

    const handleSubmit = e => {
        e.preventDefault()
        if (value.email === '' || value.password === '') return
        dispatch(createSession(value))
    }

    const pstyle = { color: "red" }
    const istyle=  { border:"1px solid red" }

    if (!currentSession.fail){
        pstyle.display="none"
        istyle.border=""
    }

    const handleChange = e => setValue({ ...value, [e.target.id]: e.target.value })

    return (
        < main className="layout" >
            <form onSubmit={handleSubmit}>
                <label className="label" htmlFor="Email">E-mail:</label>
                <input type="text" placeholder="E-mail" className="input" id="email" value={value.email} onChange={handleChange} style={istyle}/>
                <label className="label" htmlFor="password">Password:</label>
                <input type="password" placeholder="Password" className="input" id="password" value={value.password} onChange={handleChange} style={istyle}/>
                
                <p style={pstyle}>The email and/or password entered is invalid. Please try again.</p>

                <button className="button">Login</button>
            </form>
        </main>
    )
}
