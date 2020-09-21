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
import { Formik, Field, Form } from "formik";
import * as Yup from "yup";

import agent from "../../../core/services/agent";
import { toast } from "react-toastify";


const formSchema = Yup.object().shape({
  // id,isEnabled,title,persianTitle,serviceName,parentName
  title: Yup.string().required(" Required").min(1, "Too Short!").max(100, "Too Long!"),
  persianTitle: Yup.string().required(" Required").min(1, "Too Short!").max(100, "Too Long!"),
  isEnabled: Yup.boolean().required(" Required")
  // required: Yup.string().required(" Required"),
  // email: Yup.string().email("Invalid email").required("Required"),
  // number: Yup.number().required("Required"),
  // url: Yup.string().url().required("Required"),
  // date: Yup.date().required("Required"),
  // minlength: Yup.string().min(4, "Too Short!").required("Required"),
  // maxlength: Yup.string().max(5, "Too Long!").required("Required")
});

class FormValidation extends React.Component {

  state = {
    id: "",
    title: "",
    persianTitle: "",
    isEnabled: true,
    serviceId: null,
    parentId: null,

    categories: [],
    services: [],

    errors: [],
    errorMessage: "",
    Loading: false,

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
    const { data } = await agent.Category.details();
    let { id, title, persianTitle, isEnabled, serviceId, parentId } = data.result.data;
    this.setState({ id, title, persianTitle, isEnabled, serviceId, parentId });
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
          <CardTitle> Validation</CardTitle>
        </CardHeader>
        <CardBody>  </CardBody>
      </Card>
    );
  }
}
export default FormValidation;
