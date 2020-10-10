import { requests } from "../agent";

import jwtDecode from "jwt-decode";
import { functions } from "firebase";

const tokenKey = "token";

export async function login(phoneNumber, password) {
  const { data } = await requests.post("/Account/AdminLogin", {
    phoneNumber,
    password
  });
  console.log(data);
  const jwt = data.result.data.token;
  localStorage.setItem(tokenKey, jwt);
}

export async function loginWithJwt(jwt) {
  localStorage.setItem(tokenKey, jwt);
}

export async function logout() {
  localStorage.removeItem("token");
}

export function getCurrentUser() {
  try {
    const jwt = localStorage.getItem(tokenKey);
    return jwtDecode(jwt);
  } catch (error) {
    return null;
  }
}

export async function isAdminLoggedIn() {
  const token = getJwt();
  if (!token)
    return false;
  try {
    await requests.get("/Account/IsAdminLoggedIn");
    return true;
  } catch (error) {
    return false;
  }
}

// export function isInRoleAdmin() {
//   var user = getCurrentUser();
// if(!user || )
// }

export function getJwt() {
  return localStorage.getItem(tokenKey);
}

export default {
  login,
  logout,
  isAdminLoggedIn,
  getCurrentUser,
  loginWithJwt,
  getJwt
};
