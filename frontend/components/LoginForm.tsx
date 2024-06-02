'use client'
import React, { createContext, useContext, useEffect } from 'react';
import { Formik, Field, Form, FormikHelpers } from 'formik';
import GoogleLoginButton from './GoogleLoginButton';
import AccountService from "@/utils/AccountService";
import doesCookieExist from "@/utils/CookieHelper";
import { useRouter } from "next/navigation";
import { AppContext } from '@/context/StateComponent';

interface Values {
  email: string;
  password: '';
}
const LoginForm = () => {
    const { userInfo, setUserInfo } = useContext(AppContext);
    const router = useRouter();

    useEffect(() => {
        if (doesCookieExist("jwtTimer")) {
            console.log("here")
            router.push("/");
        }
      }, [userInfo]);

    const login = async (email: string, password: string) => {
        const res = await AccountService.login(email, password)
        if (await res.status < 400) {
            setUserInfo({
                email: email,
                jwt: res.jwt,
                refreshToken: res.refreshToken
            })
            router.push("/");
        }
        console.log(await res.status)
    }

  return (
    <div>
      <h1>Signup</h1>
      <Formik
        initialValues={{
          email: '',
          password: '',
        }}
        onSubmit={async (
          values: Values,
          { setSubmitting }: FormikHelpers<Values>
        ) => {
            login(values.email, values.password)
            // const res = await AccountService.login(values.email, values.password)
            // if (await res.status > 300) {
            //     setUserInfo({
            //         email: values.email,
            //         jwt: res.jwt,
            //         refreshToken: res.refreshToken
            //     })
            // }
            // console.log(await res.status)
        }}
      >
        <Form>
          <label htmlFor="password">Password</label>
          <Field suggested="current-password" type="password" id="password" name="password" placeholder="" />

          <label htmlFor="email">Email</label>
          <Field
            id="email"
            name="email"
            placeholder="john@acme.com"
            type="email"
          />

          <button type="submit">Submit</button>
        </Form>
      </Formik>
      {/* <GoogleLoginButton></GoogleLoginButton> */}
      <button onClick={() => login("admin@eesti.ee", "Kala.maja1")}>Login easy</button>
    </div>
  )
}

export default LoginForm