import React from 'react'
import { useSelector } from 'react-redux'
import { navigate } from 'hookrouter'
import { A } from 'hookrouter'

export default ({ children }) => {
    const currentSession = useSelector(state => state.session)

    //guard
    if (Object.entries(currentSession).length === 0)
        navigate('/login')
        

    return (
        < main className="layout" >
            <nav className="menu">
                <A href='/logout' className="button">Logout</A>
            </nav>
            <section>
                {children}
            </section>
        </main >
    )
}