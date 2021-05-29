import Axios from "axios";
import axios from "axios";
import { func } from "prop-types";
// import { history } from "../..";
import { toast } from "react-toastify";
export const baseUrl = "https://localhost:44377/";
// // // // // export const baseUrl = "https://api.callindoor.ir/";
axios.defaults.baseURL = "https://localhost:44377/api";
// // // // // axios.defaults.baseURL = "https://api.callindoor.ir/api";

// const token = window.localStorage.getItem("jwt");
// axios.config.headers.Authorization = `Bearer ${token}`;

const headerNames = {
  acceptLanguage: "Accept-Language"
}

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

  if (error?.response?.data) {
    // console.log(error.response);
    error.response.data.errors.map((item, index) => {
      toast.error(item);
    });
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
  getWithHeader: (url, headerValue) => axios.get(url, {
    headers: { "accept-language": headerValue },
  }),
  post: (url, body) => axios.post(url, body),
  postWithHeader: (url, body, headerValue) => axios.post(url, body, {
    headers: { "accept-language": headerValue },
  }),
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

  rejectProvideServicesInAdmin: (obj) =>
    requests.post("/ServiceType/RejectProvideServicesInAdmin", obj),

  acceptProvideServicesInAdmin: (serviceid) =>
    requests.get(
      `/ServiceType/AcceptProvideServicesInAdmin?serviceId=${serviceid}`
    ),

  getChatServiceDetailsInAdmin: (id) =>
    requests.get(`/ServiceType/GetChatServiceDetailsInAdmin?id=${id}`),
  getServiceServiceDetailsInAdmin: (id) =>
    requests.get(`/ServiceType/GetServiceServiceDetailsInAdmin?id=${id}`),

  getServiceCommentsForAdmin: (id) =>
    requests.get(`/ServiceType/GetServiceCommentsForAdmin?id=${id}`),
  confirmComment: (id) => requests.get(`/ServiceType/ConfirmComment?id=${id}`),
};


const UserWithdrawlRequest = {
  GetAllRequestForAdmin: (obj) => requests.post('/UserWithdrawlRequest/GetAllRequestForAdmin', obj),


  // GetAll: () => requests.get("/ServiceType/GetAllServiceForAdmin"),
  // details: (id) => requests.get(`/ServiceType/GetServiceByIdForAdmin?id=${id}`),
  // create: (service) => requests.post("/ServiceType/CreateForAdmin", service),
  // update: (activity) => requests.put("/ServiceType/UpdateServiceForAdmin", activity),
  // // delete: (id) => requests.del(`/activities/${id}`)

  // getAllProvideServicesInAdmin: (params) =>
  //   requests.get(`/ServiceType/GetAllProvideServicesInAdmin?${params}`),

  RejectUserWithdrawlRequestInAdmin: (obj) =>
    requests.post("/UserWithdrawlRequest/RejectUserWithdrawlRequestInAdmin", obj),

  AcceptRequestInAdmin: (requestId) =>
    requests.get(
      `/UserWithdrawlRequest/AcceptRequestInAdmin?requestId=${requestId}`
    ),

  // getChatServiceDetailsInAdmin: (id) =>
  //   requests.get(`/ServiceType/GetChatServiceDetailsInAdmin?id=${id}`),
  // getServiceServiceDetailsInAdmin: (id) =>
  //   requests.get(`/ServiceType/GetServiceServiceDetailsInAdmin?id=${id}`),

  // getServiceCommentsForAdmin: (id) =>
  //   requests.get(`/ServiceType/GetServiceCommentsForAdmin?id=${id}`),
  // confirmComment: (id) => requests.get(`/ServiceType/ConfirmComment?id=${id}`),
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

  // User: (username) => requests.post("/Account/LockedUser", { username }),
  // confirmCertificate: (id) =>
  //   requests.get(`/ServiceType/ConfirmComment?id=${id}`),

  confirmProfileForAdmin: (obj) =>
    requests.post("/Account/ConfirmProfileForAdmin", obj),
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

export const Tikets = {
  // GetAllTiketsInAdmin: (model) => requests.post("/ServiceType/GetAllProvideServicesInAdmin", model),

  GetAllTiketsInAdmin: (params) =>
    requests.get(`/Tiket/GetAllTiketsInAdmin?${params}`),
  GetTiketsDetails: (id) =>
    requests.get(`/Tiket/GetTiketsDetailsInAdmin?tiketId=${id}`),
  closeOrOpenTicket: (id) =>
    requests.get(`/Tiket/CloseOrOpenTicket?ticketId=${id}`),

  addFileToTiketInAdmin: (model) =>
    requests.post("/tiket/AddFileToTiketInAdmin", model),
  addChatMessageToTiketInAdmin: (model) =>
    requests.post("/tiket/AddChatMessageToTiketInAdmin", model),
};

const Transactions = {
  GetAllTransactionInAdmin: (model) => requests.post("/Transactoin/GetAllTransactionInAdmin", model),
  GetAllServiceTransactionInAdmin: (model) => requests.post("/Transactoin/GetAllServiceTransactionInAdmin", model),
  GetAllNormalTransactionInAdmin: (model) => requests.post("/Transactoin/GetAllNormalTransactionInAdmin", model),
  GetAllTopTenTransactionInAdmin: (model) => requests.post("/Transactoin/GetAllTopTenTransactionInAdmin", model),
  GetAllCommissionTransactionInAdmin: (model) => requests.post("/Transactoin/GetAllCommissionTransactionInAdmin", model)



  //   GetTiketsDetails: (id) =>
  //   requests.get(`/Tiket/GetTiketsDetailsInAdmin?tiketId=${id}`),
  // closeOrOpenTicket: (id) =>
  //   requests.get(`/Tiket/CloseOrOpenTicket?ticketId=${id}`),

  // addFileToTiketInAdmin: (model) =>
  //   requests.post("/tiket/AddFileToTiketInAdmin", model),
  // addChatMessageToTiketInAdmin: (model) =>
  //   requests.post("/tiket/AddChatMessageToTiketInAdmin", model),
};



const Resources = {
  dataAnotationsList: (language) => requests.get(`/Resource/GetErrorMessages?languageHeader=${language}`),
  editDataAnotationAndErrorMessages: (model) => requests.post("/Resource/EditErrorMessagess", model),


  StaticWordList: (language) => requests.get(`/Resource/GetStaticWord?languageHeader=${language}`),
  editStaticWordList: (model) => requests.post("/Resource/EditStaticWord", model)



  // // // {
  // // //   // setHeader(headerNames.acceptLanguage, acceptLanguageHeader)
  // // //   return requests.getWithHeader("/Resource/GetDataAnotationAndErrorMessages", acceptLanguageHeader)
  // // // },
  // listActive: () => requests.get("/Account/Role/GetAllActiveRolesInAdmin"),
  // details: (id) => requests.get(`/Account/Role/GetRoleByIdInAdmin?id=${id}`),
  // create: (role) => requests.post("/Account/Role/CreateRoleInAdmin", role),


  //   {
  //   // setHeader(headerNames.acceptLanguage, acceptLanguageHeader)
  //   return requests.postWithHeader("/Resource/EditDataAnotationAndErrorMessages", model, acceptLanguageHeader)
  // }
};


const Settings = {
  details: () => requests.get("/Settings/GetSettings"),
  update: (setting) => requests.put("/Settings/UpdateSettings", setting),
};

const Home = {
  details: () => requests.get("/Home/DashBoardForAdmin"),
};

export const Questions = {
  list: () => requests.get("/Question/GetAllQuestionsForAdmin"),
  create: (question) =>
    requests.post("/Question/CreateQuestionForAdmin", question),
  details: (id) => requests.get(`/Question/GetQuestionsByIdForAdmin?id=${id}`),
  update: (question) =>
    requests.put("/Question/UpdateQuestionsForAdmin", question),
};

export const CheckDiscount = {
  list: () => requests.get("/CheckDiscount/GetAllCheckDiscountInAdmin"),
  create: (discount) =>
    requests.post("/CheckDiscount/AddCheckDiscountInAdmin", discount),
  details: (id) =>
    requests.get(`/CheckDiscount/GetCheckDiscountByIdInAdmin?id=${id}`),
  update: (discount) =>
    requests.put("/CheckDiscount/UpdateCheckDiscountInAdmin", discount),
};

export default {
  User,
  Role,
  Permissions,
  Category,
  ServiceTypes,
  UserWithdrawlRequest,
  Test,
  Areas,
  Settings,
  Home,
  Tikets,
  Transactions,
  Questions,
  CheckDiscount,
  Resources
};




// "Accept-Language"
function setHeader(headerName, headerValue) {
  axios.interceptors.request.use(
    (config) => {
      // alert(headerName)
      console.log(headerValue)
      config.headers = { "accept-language": headerValue };
      return config;
    }
    // (error) => {
    //   return Promise.reject(error);
    // }
  );

}