import FormPage from '@/components/FormPage'
import LoginForm from '@/components/LoginForm'
import RegisterForm from '@/components/RegisterForm'
import React from 'react'

const registerPage = () => {
  return (
    <div>
        <FormPage>
            <RegisterForm></RegisterForm>
        </FormPage>
    </div>
  )
}

export default registerPage