import React from 'react'
import { useRoutes } from 'hookrouter'
import { useDispatch } from 'react-redux'
import CustomerList from './components/CustomerList'
import LoginForm from './components/LoginForm'
import SearchForm from './components/SearchForm'
import Layout from './components/Layout'
import './App.css'
import { deleteSession } from './actions'



export default () => {
  const dispatch = useDispatch()
  return useRoutes({
    '/': () => <LoginForm />,
    '/customers': () => <Layout> <SearchForm /> <CustomerList /></Layout>,
    '/login': () => <LoginForm />,
    '/logout': () => {
      dispatch(deleteSession())
      return <LoginForm/>
    }
  })
}

