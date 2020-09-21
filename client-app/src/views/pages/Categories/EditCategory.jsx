import React from "react";
import {
  Card,
  CardHeader,
  CardTitle,
  CardBody,
  FormGroup,
  Button,
  Label
} from "reactstrap";

import Joi from "joi-browser";
import Form from "../../../components/common/form";


// import { Formik, Field, Form  } from "formik";
// import * as Yup from "yup";

import agent from "../../../core/services/agent";
import { toast } from "react-toastify";


// const formSchema = Yup.object().shape({
//   // id,isEnabled,title,persianTitle,serviceName,parentName
//   title: Yup.string().required(" Required").min(1, "Too Short!").max(100, "Too Long!"),
//   persianTitle: Yup.string().required(" Required").min(1, "Too Short!").max(100, "Too Long!"),
//   isEnabled: Yup.boolean().required(" Required")
//   // required: Yup.string().required(" Required"),
//   // email: Yup.string().email("Invalid email").required("Required"),
//   // number: Yup.number().required("Required"),
//   // url: Yup.string().url().required("Required"),
//   // date: Yup.date().required("Required"),
//   // minlength: Yup.string().min(4, "Too Short!").required("Required"),
//   // maxlength: Yup.string().max(5, "Too Long!").required("Required")
// });

class EditCategory extends Form {

  state = {
    data: {
      id: "",
      title: "",
      persianTitle: "",
      isEnabled: true,
      serviceName: '',
      parentId: null,

      // serviceId: null,
      // parentId: null,
    },
    categories: [],
    services: [],

    errors: [],
    errorMessage: "",
    Loading: false,

  };

  schema = {
    // id: Joi.string(),
    title: Joi.string()
      .required()
      .label("Title"),

    persianTitle: Joi.string()
      .required()
      .label("PersianTitle"),

    parentId: Joi.string()
      .required()
      .label("parent"),

    isEnabled: Joi
      .required()
      .label("isEnabled"),

    serviceId: Joi.string()
      .required()
      .label("service"),

  };




  async populatingCategories() {
    const { data } = await agent.Category.list();
    let categories = data.result.data;
    this.setState({ categories });
  }

  async populatingServiceTypes() {
    const { data } = await agent.ServiceTypes.list();
    let services = data.result.data;
    this.setState({ services });
  }

  async populatinCategory() {


    const catId = this.props.match.params.id;
    try {
      const { data } = await agent.Category.details(catId);
      let { id, title, persianTitle, isEnabled, serviceId, parentId } = data.result.data;
      console.clear()
      console.log(title)
      this.setState({ data: { id, title: title, persianTitle, isEnabled, serviceId, parentId } });

      console.log(this.state)
    }
    catch (ex) {
      console.clear()
      console.log(ex)
      console.log(ex);
      if (ex?.response?.status == 404 || ex?.response?.status == 400) {
        return this.props.history.replace("/not-found");
      }
    }





  }

  async componentDidMount() {
    this.populatingCategories();
    this.populatingServiceTypes();
    this.populatinCategory();
  }

  doSubmit = async (e) => {

    this.setState({ Loading: true });
    e.preventDefault();
    const errorMessage = "";
    const errors = [];
    this.setState({ errorMessage, errors });
    try {
      const { title, persianTitle, isEnabled, serviceId, parentId, } = this.state;

      const obj = { title, persianTitle, isEnabled, serviceId, parentId };
      const { data } = await agent.Category.update(obj);
      if (data.result.status) {
        toast.success(data.result.message)
      }
      setTimeout(() => {
        this.setState({ Loading: false });
      }, 2000);
    } catch (ex) {
      console.log(ex);
      if (ex?.response?.status == 400) {
        const errors = ex?.response?.data?.errors;
        this.setState({ errors });
      } else if (ex?.response) {
        const errorMessage = ex?.response?.data?.message;
        this.setState({ errorMessage });
        toast.error(errorMessage, {
          autoClose: 10000,
        });
      }
      setTimeout(() => {
        this.setState({ Loading: false });
      }, 1000);
    }
  };


  render() {
    const { id, title, persianTitle, isEnabled, serviceId, parentId } = this.state


    return (
      <Card>
        <CardHeader>
          <CardTitle> Edit Category </CardTitle>
        </CardHeader>
        <CardBody>

          <form onSubmit={this.handleSubmit}>
            {this.renderInput("title", "Title")}
            {this.renderInput("persianTitle", "persianTitle")}
            {this.renderInput("isEnabled", "is Enabled", "checbox")}
            {this.renderSelect("parentId", "Parent", this.state.categories.map(item => ({ name: item.title })))}
            {this.renderSelect("serviceId", "Service", this.state.services)}

            {this.renderButton("Save")}

            {/* {this.renderSelect("genreId", "Genre", this.state.genres)} */}
            {/* {this.renderInput("numberInStock", "Number in Stock", "number")}
            {this.renderInput("dailyRentalRate", "Rate")}
            {this.renderButton("Save")} */}
          </form>

        </CardBody>
      </Card>
    );
  }
}
export default EditCategory;
