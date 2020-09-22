import React from "react";
import {
  Card,
  CardHeader,
  CardTitle,
  CardBody,
  FormGroup,
  Button,
  Label,
  Alert,
  Col,
  Spinner
} from "reactstrap";

import Joi, { validate } from "joi-browser";
import Form from "../../../components/common/form";


// import { Formik, Field, Form  } from "formik";
// import * as Yup from "yup";

import agent from "../../../core/services/agent";
import { toast } from "react-toastify";


class EditCategory extends Form {

  state = {
    data: {
      id: null,
      name: "",
      persianName: "",
      color: ""
    },

    isEnabled: true,
    errors: {},
    errorscustom: [],
    errorMessage: "",
    Loading: false,
  };

  schema = {
    id: Joi.number(),
    name: Joi.string(),
    persianName: Joi.string()
      .required()
      .label("Persian Name"),

    color: Joi.string()
      .required()
      .label("color"),
  };


  async populatinService() {
    const serviceId = this.props.match.params.id;
    try {
      const { data } = await agent.ServiceTypes.details(serviceId);
      let { id, name, persianName, color, isEnabled } = data.result.data;
      this.setState({ data: { id, name, persianName, color }, isEnabled });
    }
    catch (ex) {
      console.log(ex);
      if (ex?.response?.status == 404 || ex?.response?.status == 400) {
        alert(1)
        return this.props.history.replace("/not-found");
      }
    }
  }


  async componentDidMount() {
    this.populatinService();
  }



  // doSubmit = async () => {
  //   await saveMovie(this.state.data);
  //   this.props.history.push("/movies");
  // };





  doSubmit = async () => {
    this.setState({ Loading: true });
    const errorMessage = "";
    const errorscustom = [];
    this.setState({ errorMessage, errorscustom });
    try {
      const obj = { ...this.state.data, isEnabled: this.state.isEnabled };
      const { data } = await agent.ServiceTypes.update(obj);
      if (data.result.status)
        toast.success(data.result.message)
    } catch (ex) {
      console.log(ex);
      if (ex?.response?.status == 400) {
        const errorscustom = ex?.response?.data?.errors;
        this.setState({ errorscustom });
      } else if (ex?.response) {
        const errorMessage = ex?.response?.data?.message;
        this.setState({ errorMessage });
        toast.error(errorMessage, {
          autoClose: 10000,
        });
      }
    }
    setTimeout(() => {
      this.setState({ Loading: false });
    }, 800);
  };
 




  render() {
    const { errorscustom, errorMessage } = this.state


    return (
      <Col sm="10" className="mx-auto">
        <Card >
          <CardHeader>
            <CardTitle> Edit Category </CardTitle>
          </CardHeader>
          <CardBody>
            {errorscustom &&
              errorscustom.map((err, index) => {
                return (
                  <Alert
                    key={index}
                    className="text-center"
                    color="danger "
                  >
                    {err}
                  </Alert>
                );
              })}

            <form onSubmit={this.handleSubmit}>
              {this.renderInput("name", "Name")}
              {this.renderInput("persianName", "PersianName")}
              {this.renderInput("color", "Color")}

              <div className="form-group">
                <label htmlFor="isEnabled">Is Enabled</label>
                <input
                  value={this.state.isEnabled}
                  checked={this.state.isEnabled}
                  onChange={(e) => this.setState({ isEnabled: !this.state.isEnabled })}
                  name="isEnabled"
                  id="isEnabled"
                  type="checkbox"
                  className="ml-1" />
              </div>


              {this.state.Loading ?
                <Button disabled={true} color="primary" className="mb-1">
                  <Spinner color="white" size="sm" type="grow" />
                  <span className="ml-50">Loading...</span>
                </Button>
                :
                this.renderButton("Save")
              }

            </form>

          </CardBody>
        </Card>
      </Col>
    );
  }
}
export default EditCategory;
