import React from "react";
import cookie from "react-cookies";
import { Outlet, Navigate } from "react-router-dom";
export enum COOKIE_NAME {
  AUTH = "MEDU.AUTH",
  USER = "MEDU.USER",
}
export const AuthRoute = () => {
  // const authData: any = cookie.load(COOKIE_NAME.AUTH);

  // return authData ? <Outlet /> : <Navigate to='/login' />;
  return <Outlet />;

}