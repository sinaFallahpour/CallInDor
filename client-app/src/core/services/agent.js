import axios from "axios";
// import { history } from "../..";
import { toast } from "react-toastify";

axios.defaults.baseURL = "https://localhost:44377/api";

// const token = window.localStorage.getItem("jwt");
// axios.config.headers.Authorization = `Bearer ${token}`;

axios.interceptors.request.use(
  (config) => {
    const token = window.localStorage.getItem("token");
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
  },
  // (error) => {
  //   return Promise.reject(error);
  // }
);

axios.interceptors.response.use(undefined, (error) => {
  if (error.message === "Network Error" && !error.response) {
    toast.error("Network error - make sure API is running!");
  }

  const { status, data, config } = error?.response;
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
  if (status === 500) {
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
    return axios
      .post(url, formData, {
        headers: { "Content-type": "multipart/form-data" }
      });
  }
};

const ServiceTypes = {
  list: () => requests.get("/Service/GetAllActive"),
  // details: (id) => requests.get(`/activities/${id}`),
  // create: (activity) => requests.post("/activities", activity),
  // update: (activity) => requests.put(`/activities/${activity.id}`, activity),
  // delete: (id) => requests.del(`/activities/${id}`)
};

const User = {
  // current: () => requests.get("/user"),
  login: (user) => requests.post("/user/login", user),
  // register: (user) => requests.post("/user/register", user)
};

const Category = {
  // list: () => requests.get("/category/getAll"),
  list: () => requests.get("/Category/GetAllCateGoryForAdmin"),
  details: (id) => requests.get(`/Category/GetByIdForAdmin/${id}`),
  create: (category) => requests.post("/Category/Create", category),
  update: (category) => requests.put("/Category/Update", category)
  // delete: (id) => requests.del(`/activities/${id}`)
};

export default {
  User,
  Category,
  ServiceTypes
};
