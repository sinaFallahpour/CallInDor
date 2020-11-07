import React from "react";
import {
  Button,
  FormGroup,
  Card,
  CardHeader,
  CardBody,
  CardTitle,
  Form as FormStrap,
  Alert,
  Spinner,
  Col,
} from "reactstrap";

// import { modalForm } from "./ModalSourceCode"
import { toast } from "react-toastify";
import ReactTagInput from "@pathofdev/react-tag-input";
import "@pathofdev/react-tag-input/build/index.css";
import Joi from "joi-browser";

import agent from "../../../core/services/agent";
import Form from "../../../components/common/form";

import ModalCustom from "./ModalCustom";
import { conutryPhoneCodes } from "../../../utility/phone-codes.data";
class Create extends Form {
  state = {
    data: {
      id: null,
      email: "",
      password: "",
      name: "",
      lastName: "",
      dial_code: "",
      phoneNumber: "",
      roleId: null,
    },
    roles: [],
    conutryPhoneCodes: conutryPhoneCodes,
    modal: false,

    errors: {},
    errorscustom: [],
    errorMessage: "",
    Loading: false,
  };

  schema = {
    id: Joi.string().allow(null),
    name: Joi.string().required().min(1).max(200).label("Name"),

    lastName: Joi.string().required().min(1).max(100).label("last Name"),

    email: Joi.string().email({ minDomainAtoms: 2 }).required().label("email"),

    password: Joi.string().required().min(6).max(20).label("passworrd"),

    phoneNumber: Joi.number().required().label("phone number"),

    dial_code: Joi.string().required().label("country code"),

    roleId: Joi.string().required().label("role"),

    roles: Joi.label("roles"),
  };

  async populatingRoles() {
    const { data } = await agent.Role.listActive();
    let roles = data.result.data;
    this.setState({ roles });
  }

  async componentDidMount() {
    this.populatingRoles();
  }

  componentDidUpdate(prevProps, prevState) {
    const { addNew, currentUser, addToUsers } = this.props;

    if (addNew != prevProps.addNew) {
      const data = {
        id: null,
        email: "",
        name: "",
        lastName: "",
        roleId: null,
      };
      this.setState({
        data,
      });
    }
    if (
      !addNew &&
      currentUser != null &&
      currentUser?.data?.id != prevProps?.currentUser?.data?.id
    ) {
      delete currentUser.data.phoneNumber;
      delete currentUser.data.countryCode;
      delete currentUser.data.password;
      delete currentUser.data.dial_code;
      delete currentUser.data.roleName;

      delete this.schema.phoneNumber;
      delete this.schema.countryCode;
      delete this.schema.password;
      delete this.schema.dial_code;
      delete this.schema.roleName;

      this.setState({
        data: currentUser.data,
        ...currentUser,
        // Specialities: [],
      });
    }
  }

  toggleModal = () => {
    this.setState((prevstate, prevProps) => {
      return { modal: !prevstate.modal };
    });
  };

  doSubmit = async (e) => {
    this.setState({ Loading: true });

    const errorMessage = "";
    const errorscustom = [];
    this.setState({ errorMessage, errorscustom });
    try {
      // const { isEnabled, isProfessional, Specialities } = this.state;
      const obj = {
        ...this.state.data,
        countryCode: this.state.data.dial_code,
      };

      //Add
      if (this.props.addNew) {
        const { data } = await agent.User.registerAdmin(obj);
        if (data.result.status) {
          obj.id = data.result.data.id;
          obj.roleName = data.result.data.roleName;
          this.props.addToUsers(obj);
          toast.success(data.result.message);
          setTimeout(() => {
            this.cleanData();
          }, 600);
        }
      }
      //Edit
      else {
        const { data } = await agent.User.update(obj);
        if (data.result.status) {
          obj.id = data.result.data.id;
          obj.roleName = data.result.data.roleName;
          this.props.editToUsers(obj);
          toast.success(data.result.message);
          // setInterval(() => {
          //   this.cleanData();
          // }, 600);
        }
      }
    } catch (ex) {
      console.log(ex);
      if (ex?.response?.status == 400) {
        const errorscustom = ex?.response?.data?.errors;
        this.setState({ errorscustom });
      } else if (ex?.response) {
        const errorMessage = ex?.response?.data?.Message;
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

  cleanData = () => {
    const data = {
      id: null,
      email: "",
      password: "",
      name: "",
      lastName: "",
      phoneNumber: "",
      roleId: null,
      dial_code: null,
    };

    this.setState({
      data,
    });
  };

  render() {
    const {
      errorscustom,
      errorMessage,
      roles,
      modal,
      conutryPhoneCodes,
    } = this.state;
    const { addNew } = this.props;

    return (
      <React.Fragment>
        <Col sm="13" className="mx-auto">
          <Card>
            <CardHeader>
              <CardTitle> {addNew ? "Create User" : "Edit User"} </CardTitle>
            </CardHeader>
            <CardBody>
              {errorscustom &&
                errorscustom.map((err, index) => {
                  return (
                    <Alert key={index} className="text-center" color="danger ">
                      {err}
                    </Alert>
                  );
                })}

              <form onSubmit={this.handleSubmit}>
                <FormGroup row>
                  <Col md="4">{this.renderInput("name", "Name")}</Col>
                  <Col md="4"> {this.renderInput("lastName", "Last Name")}</Col>
                  <Col md="4"> {this.renderInput("email", "Email")}</Col>

                  {addNew ? (
                    <>
                      <Col md="4">
                        {this.renderInput("password", "Password", "password")}
                      </Col>
                      <Col md="4">
                        {this.renderInput(
                          "phoneNumber",
                          "Phone Number(withOut Country Code)",
                          "number"
                        )}
                      </Col>
                      <Col md="4">
                        {this.renderReactSelect(
                          "dial_code",
                          "Country Code",
                          conutryPhoneCodes.map((item) => ({
                            value: item.dial_code,
                            label: item.name,
                          }))
                        )}
                      </Col>
                    </>
                  ) : null}
                  <Col md="4">
                    {this.renderReactSelect(
                      "roleId",
                      "Role",
                      roles.map((item) => ({
                        value: item.id,
                        label: item.name,
                      }))
                    )}
                  </Col>
                </FormGroup>

                {this.state.Loading ? (
                  <Button disabled={true} color="primary" className="mb-1">
                    <Spinner color="white" size="sm" type="grow" />
                    <span className="ml-50">Loading...</span>
                  </Button>
                ) : addNew ? (
                  this.renderButton("Add")
                ) : (
                  this.renderButton("Edit")
                )}
              </form>
            </CardBody>
          </Card>
        </Col>
        {modal && (
          <ModalCustom
            modal={modal}
            toggleModal={this.toggleModal}
            handleAddSpeciality={this.handleAddSpeciality}
          />
        )}
      </React.Fragment>
    );
  }
}
export default Create;
