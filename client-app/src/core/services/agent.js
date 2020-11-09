import axios from "axios";
// import { history } from "../..";
import { toast } from "react-toastify";
export const baseUrl = "https://localhost:44377/";
axios.defaults.baseURL = "https://localhost:44377/api";
// axios.defaults.baseURL = "https://api.callindoor.ir/api";

// const token = window.localStorage.getItem("jwt");
// axios.config.headers.Authorization = `Bearer ${token}`;

axios.interceptors.request.use(
  (config) => {
    const token = window.localStorage.getItem("token");
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
  }
  // (error) => {
  //   return Promise.reject(error);
  // }
);

axios.interceptors.response.use(undefined, (error) => {
  if (error.message === "Network Error" && !error.response) {
    toast.error("Network error - make sure API is running!");
  }

  // const { status, data, config } = error?.response;
  //   if (status === 404) {
  //     history.push("/notfound");
  //   }
  // if (
  //   status === 400 &&
  //   config.method === "get" &&
  //   data.errors.hasOwnProperty("id")
  // ) {
  //   history.push("/notfound");
  // }
  if (error?.response?.status === 500) {
    toast.error("Server error - check the terminal for more info!");
  }
  //   throw error.response;
  return Promise.reject(error);
});

// const responseBody = (response) => response.data;

export const requests = {
  get: (url) => axios.get(url),
  post: (url, body) => axios.post(url, body),
  put: (url, body) => axios.put(url, body),
  del: (url) => axios.delete(url),
  postForm: (url, file) => {
    const formData = new FormData();
    formData.append("File", file);
    return axios.post(url, formData, {
      headers: { "Content-type": "multipart/form-data" },
    });
  },
};

const ServiceTypes = {
  list: () => requests.get("/ServiceType/GetAllActiveService"),
  GetAll: () => requests.get("/ServiceType/GetAllServiceForAdmin"),
  details: (id) => requests.get(`/ServiceType/GetServiceByIdForAdmin?id=${id}`),
  create: (service) => requests.post("/ServiceType/CreateForAdmin", service),
  update: (activity) =>
    requests.put("/ServiceType/UpdateServiceForAdmin", activity),
  // delete: (id) => requests.del(`/activities/${id}`)

  getAllProvideServicesInAdmin: (params) =>
    requests.get(`/ServiceType/GetAllProvideServicesInAdmin?${params}`),

  rejectProvideServicesInAdmin: (obj) => requests.post("/ServiceType/RejectProvideServicesInAdmin", obj),

};

const User = {
  // current: () => requests.get("/user"),
  list: () => requests.get("/Account/admin/GetAllAdminInAdmin"),
  UsersList: (params) => requests.get(`/Account/GetAllUsersList?${params}`),
  UsersListForVerification: (params) =>
    requests.get(`/Account/GetAllUsersListForVerification?${params}`),
  UsersDetails: (obj) => {
    return requests.post("/Account/admin/GetUserByUsernameInAdmin", obj);
  },
  details: (id) => requests.get(`/Account/admin/GetAdminByIdInAdmin?id=${id}`),
  login: (user) => requests.post("/user/login", user),
  registerAdmin: (user) =>
    requests.post("/Account/admin/RegisterAdminInAdmin", user),
  update: (user) => requests.put("/Account/admin/UpdateAdmin", user),
  changePassword: (obj) => requests.post("/Account/ChangePasswordInAdmin", obj),
  forgetPassword: (obj) => requests.post("/Account/ForgetPasswod", obj),
  lockedUser: (username) => requests.post("/Account/LockedUser", { username }),
};

const Role = {
  list: () => requests.get("/Account/Role/GetAllRolesInAdmin"),
  listActive: () => requests.get("/Account/Role/GetAllActiveRolesInAdmin"),
  details: (id) => requests.get(`/Account/Role/GetRoleByIdInAdmin?id=${id}`),
  create: (role) => requests.post("/Account/Role/CreateRoleInAdmin", role),
  update: (role) => requests.put("/Account/Role/UpdateRoleInAdmin", role),
};

const Permissions = {
  list: () => requests.get("/Account/Permission/GetAllPermissionInAdmin"),
  // listActive: () => requests.get("/Account/Role/GetAllActiveRolesInAdmin"),
  // details: (id) => requests.get(`/Account/Role/GetRoleByIdInAdmin?id=${id}`),
  // create: (role) => requests.post("/Account/Role/CreateRoleInAdmin", role),
  // update: (role) => requests.put("/Account/Role/UpdateRoleInAdmin", role),
};

const Category = {
  // list: () => requests.get("/category/getAll"),
  listParentCatgory: () =>
    requests.get("/Category/GetAllParentCateGoryForAdmin"),
  listCategory: () => requests.get("/Category/GetAllCateGoryForAdmin"),
  details: (id) => requests.get(`/Category/GetByIdForAdmin?id=${id}`),
  create: (category) => requests.post("/Category/Create", category),
  update: (category) => requests.put("/Category/Update", category),
  // delete: (id) => requests.del(`/activities/${id}`)
};

const Areas = {
  // list: () => requests.get("/category/getAll"),
  list: () => requests.get("/Area/GetAllAreaForAdmin"),
  details: (id) => requests.get(`/Area/GetAreaByIdForAdmin?id=${id}`),
  create: (area) => requests.post("/Area/CreateArea", area),
  update: (area) => requests.put("/Area/UpdateArea", area),
  // delete: (id) => requests.del(`/activities/${id}`)
};

const Test = {
  list: (params) => requests.get(`/Test/Index?${params}`),
  details: (id) => requests.get(`/Test/getById?${id}`),
  create: (test) => requests.post("/Test/Create", test),
  update: (test) => requests.put("/Test/Update", test),
  delete: (id) => requests.del("/Test/Index", id),
};

const Settings = {
  details: () => requests.get("/Settings/GetSettings"),
  update: (setting) => requests.put("/Settings/UpdateSettings", setting),
};

export default {
  User,
  Role,
  Permissions,
  Category,
  ServiceTypes,
  Test,
  Areas,
  Settings,
};
