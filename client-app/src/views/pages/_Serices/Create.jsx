import React from "react";
import {
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Label,
  FormGroup,
  Input,
  Card,
  CardHeader,
  CardBody,
  CardTitle,
  TabContent,
  TabPane,
  Nav,
  NavItem,
  NavLink,
  Form as FormStrap,
  Alert,
  Spinner,
  Col
} from "reactstrap";


// import { modalForm } from "./ModalSourceCode"
import { toast } from "react-toastify";
import agent from "../../../core/services/agent";

import Joi from "joi-browser";
import Form from "../../../components/common/form";

class ModalForm extends Form {
  state = {
    data: {
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
    name: Joi.string(),

    persianName: Joi.string()
      .required()
      .label("Persian Name"),

    color: Joi.string()
      .required()
      .label("color"),
  };




  async componentDidMount() {

  }


  doSubmit = async (e) => {
    this.setState({ Loading: true });

    const errorMessage = "";
    const errorscustom = [];
    this.setState({ errorMessage, errorscustom });
    try {
      const obj =    { ...this.state.data, isEnabled: this.state.isEnabled }
      // const obj = { name, persianName, color, isEnabled };
      const { data } = await agent.ServiceTypes.create(obj);
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
    }, 200);
  };




  render() {
    const { errorscustom, errorMessage } = this.state;
    return (
      <React.Fragment>

        <Col sm="10" className="mx-auto">
          <Card >
            <CardHeader>
              <CardTitle> Create Service </CardTitle>
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
                {this.renderInput("name", "Name" )}
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
      </React.Fragment>
    );
  }
}
export default ModalForm;
