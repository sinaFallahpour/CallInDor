import React from "react";
import {
  Card,
  CardHeader,
  CardTitle,
  CardBody,
  Button,
  Alert,
  FormGroup,
  Col,
  Spinner as SpinnerButtton,
} from "reactstrap";

import Spinner from "../../../components/@vuexy/spinner/Loading-spinner";
import { PlusSquare, Check } from "react-feather";

// import ReactTagInput from "@pathofdev/react-tag-input";
import "@pathofdev/react-tag-input/build/index.css";
import { toast } from "react-toastify";

// import Joi, { validate } from "joi-browser";
// import Form from "../../../components/common/form";

// import LoadingOverlay from 'react-loading-overlay';
// import 'react-loading-overlay/lib/styles'

// import { Formik, Field, Form  } from "formik";
// import * as Yup from "yup";

import Checkbox from "../../../components/@vuexy/checkbox/CheckboxesVuexy";

import agent, { baseUrl } from "../../../core/services/agent";

import "react-block-ui/dist/style.css";
// import BlockUi from "react-block-ui";

// import ReactLoading from "react-loading";

class _DetailsUser extends React.Component {
  state = {
    data: {
      id: null,
      userName: "",
      email: "",
      name: "",
      lastName: "",
      imageAddress: "",
      isCompany: false,
      phoneNumberConfirmed: false,
      isEditableProfile: false,
      isLockOut: false,
      profileConfirmType: null,
      countryCode: null,
    },
    requiredFiles: [],
    fields: [],
    errors: {},
    errorscustom: [],
    errorMessage: "",
    Loading: false,
    pageLoading: false,
  };

  // schema = {
  //     id: Joi.number(),
  //     name: Joi.string(),
  //     persianName: Joi.string().required().label("Persian Name"),

  //     color: Joi.string().required().label("color"),

  //     minPriceForService: Joi.number()
  //         .required()
  //         .min(1)
  //         .max(10000000000000)
  //         .label("minimum Price"),

  //     minSessionTime: Joi.number()
  //         .required()
  //         .min(1)
  //         .max(10000000)
  //         .label("minimum Session Time"),

  //     acceptedMinPriceForNative: Joi.number()
  //         .required()
  //         .min(1)
  //         .max(10000000000000)
  //         .label("minimum Price For Native User"),

  //     acceptedMinPriceForNonNative: Joi.number()
  //         .required()
  //         .min(1)
  //         .max(10000000000000)
  //         .label("minimum Price For Non Native User"),

  //     roleId: Joi.string().required().label("role"),

  //     roles: Joi.label("roles"),
  // };

  async populatinUser() {
    this.setState({ pageLoading: true });
    const username = this.props.match.params.username;
    try {
      const { data } = await agent.User.UsersDetails({ username });
      let {
        id,
        userName,
        email,
        name,
        lastName,
        imageAddress,
        isCompany,
        phoneNumberConfirmed,
        isEditableProfile,
        isLockOut,
        profileConfirmType,
        countryCode,
        requiredFiles,
        fields,
      } = data.result.data;

      this.setState({
        data: {
          id,
          userName,
          email,
          name,
          lastName,
          imageAddress,
          isCompany,
          phoneNumberConfirmed,
          isEditableProfile,
          isLockOut,
          profileConfirmType,
          countryCode,
          requiredFiles,
        },
        requiredFiles,
        fields,
      });
    } catch (ex) {
      console.log(ex);
      if (ex?.response?.status == 404 || ex?.response?.status == 400) {
        return this.props.history.replace("/not-found");
      }
    }
    this.setState({ pageLoading: false });
  }

  async componentDidMount() {
    this.populatinUser();
  }

  doSubmit = async () => {
    this.setState({ Loading: true });
    const errorMessage = "";
    const errorscustom = [];
    this.setState({ errorMessage, errorscustom });
    try {
      const { isEnabled, tags, persinaTags, requiredFiles } = this.state;
      const obj = {
        ...this.state.data,
        isEnabled,
        tags: tags?.length == 0 ? null : tags.join(),
        persinaTags: persinaTags?.length == 0 ? null : persinaTags.join(),
        requiredFiles,
      };
      const { data } = await agent.ServiceTypes.update(obj);
      if (data.result.status) toast.success(data.result.message);
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
    }, 800);
  };

  render() {
    const {
      errorscustom,
      errorMessage,
      pageLoading,
      requiredFiles,
      fields,
      data,
    } = this.state;

    if (pageLoading) {
      return <Spinner />;
    }

    return (
      <Col sm="10" className="mx-auto">
        <Card>
          <CardHeader>
            <CardTitle> Edit Category </CardTitle>
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
                <Col md="6">
                  <label htmlFor={"name"}>Name</label>
                  <input
                    value={data.name}
                    readOnly
                    name={"name"}
                    id={"name"}
                    className={`form-control `}
                  />
                </Col>
                <Col md="6">
                  <label htmlFor={"LastName"}>Last Name</label>
                  <input
                    value={data.lastName}
                    readOnly
                    name={"LastName"}
                    id={"LastName"}
                    className={`form-control `}
                  />
                </Col>
              </FormGroup>

              <FormGroup row>
                <Col md="6">
                  <label htmlFor={"username"}>Username</label>
                  <input
                    value={data.userName}
                    readOnly
                    name={"username"}
                    id={"username"}
                    className={`form-control `}
                  />
                </Col>

                <Col md="6">
                  <label htmlFor={"email"}>Email</label>
                  <input
                    value={data.lastName}
                    readOnly
                    name={"email"}
                    id={"email"}
                    className={`form-control `}
                  />
                </Col>
              </FormGroup>

              <FormGroup row>
                <Col md="6">
                  <label htmlFor={"username"}>Username</label>
                  <input
                    value={data.username}
                    readOnly
                    name={"username"}
                    id={"username"}
                    className={`form-control `}
                  />
                </Col>

                <Col md="6">
                  <label htmlFor={"email"}>Email</label>
                  <input
                    value={data.email}
                    readOnly
                    name={"email"}
                    id={"email"}
                    className={`form-control `}
                  />
                </Col>
              </FormGroup>

              <FormGroup row>
                <Col md="6">
                  <label htmlFor={"bio"}>Bio</label>
                  <textarea
                    value={data.bio}
                    readOnly
                    name={"bio"}
                    id={"bio"}
                    className={`form-control `}
                  />
                </Col>
              </FormGroup>

              <FormGroup row>
                <Col md="3">
                  <Checkbox
                    disabled
                    color="primary"
                    icon={<Check className="vx-icon" size={16} />}
                    label="Profile editable  status"
                    defaultChecked={data.isEditableProfile}
                  />
                </Col>

                <Col md="3">
                  <Checkbox
                    disabled
                    color="primary"
                    icon={<Check className="vx-icon" size={16} />}
                    label="Is it a company?"
                    defaultChecked={data.isCompany}
                  />
                </Col>

                <Col md="3">
                  <Checkbox
                    disabled
                    color="primary"
                    icon={<Check className="vx-icon" size={16} />}
                    label="Lock status"
                    defaultChecked={data.isLockOut}
                  />
                </Col>
                <Col md="3">
                  <Checkbox
                    disabled
                    color="primary"
                    icon={<Check className="vx-icon" size={16} />}
                    label="Phone number confirmation"
                    defaultChecked={data.phoneNumberConfirmed}
                  />
                </Col>
              </FormGroup>

              <hr></hr>
              <h3>required file for user </h3>
              {this.state.requiredFiles?.map((requireFIle, index) => (
                <>
                  <hr></hr>
                  {requireFIle.serviceName}
                  <div key={index} className="row">
                    <input type="hidden" value={requireFIle?._id} />

                    <div className="form-group col-4  col-md-3">
                      <label></label>
                      <Button
                        type="button"
                        onClick={() => {
                          this.removeRequredFile(requireFIle?._id);
                        }}
                        className="form-control btn-danger"
                      >
                        <a
                          href={baseUrl + requireFIle.fileAddress}
                          target="_blank"
                        >
                          Download
                        </a>
                      </Button>
                      {/* {error && <div className="  alert alert-danger">{error}</div>} */}
                    </div>

                    <div className="form-group col-4  col-md-3">
                      <label></label>
                      <Button
                        type="button"
                        onClick={() => {
                          this.removeRequredFile(requireFIle?._id);
                        }}
                        className="form-control btn-danger"
                      >
                        remove
                      </Button>
                      {/* {error && <div className="  alert alert-danger">{error}</div>} */}
                    </div>
                    <div className="form-group col-4  col-md-3">
                      <label></label>
                      <Button
                        type="button"
                        onClick={() => {
                          this.removeRequredFile(requireFIle?._id);
                        }}
                        className="form-control btn-danger"
                      >
                        remove
                      </Button>
                      {/* {error && <div className="  alert alert-danger">{error}</div>} */}
                    </div>
                    <div className="form-group col-4  col-md-3">
                      <label></label>
                      <Button
                        type="button"
                        onClick={() => {
                          this.removeRequredFile(requireFIle?._id);
                        }}
                        className="form-control btn-danger"
                      >
                        remove
                      </Button>
                      {/* {error && <div className="  alert alert-danger">{error}</div>} */}
                    </div>
                    <div className="form-group col-4  col-md-3">
                      <label></label>
                      <Button
                        type="button"
                        onClick={() => {
                          this.removeRequredFile(requireFIle?._id);
                        }}
                        className="form-control btn-danger"
                      >
                        remove
                      </Button>
                      {/* {error && <div className="  alert alert-danger">{error}</div>} */}
                    </div>
                  </div>
                </>
              ))}

              {this.state.Loading ? (
                <Button disabled={true} color="primary" className="mb-1">
                  <SpinnerButtton color="white" size="sm" type="grow" />
                  <span className="ml-50">Loading...</span>
                </Button>
              ) : (
                <button className="btn btn-primary">reject or accept</button>
              )}
            </form>
          </CardBody>
        </Card>
      </Col>
    );
  }
}
export default _DetailsUser;
