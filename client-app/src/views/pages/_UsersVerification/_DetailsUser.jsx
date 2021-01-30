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
  Progress,
  Badge
} from "reactstrap";

import Spinner from "../../../components/@vuexy/spinner/Loading-spinner";
import { PlusSquare, Check, Edit2, Settings, Menu, X } from "react-feather";


import Swal from "sweetalert2";

// import coverImg from "../../../assets/img/profile/user-uploads/cover.jpg"
// import profileImg from "../../../assets/img/profile/user-uploads/user-13.jpg"

import profile from "../../../assets/img/_custom/user-profile.png";

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
      videoAddress: "",
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
        videoAddress,
        isCompany,
        phoneNumberConfirmed,
        isEditableProfile,
        isLockOut,
        profileConfirmType,
        countryCode,
        requiredFiles,
        fields,
      } = data.result.data;

      await this.setState({
        data: {
          id,
          userName,
          email,
          name,
          lastName,
          imageAddress,
          videoAddress,
          isCompany,
          phoneNumberConfirmed,
          isEditableProfile,
          isLockOut,
          profileConfirmType,
          countryCode,
          // requiredFiles,
        },
        requiredFiles,
        groupingRequiredFile: this.groupBy(requiredFiles, "serviceId"),
        fields,
      });

      // console.log(this.state.groupingRequiredFile);
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


  returnProfileConfirmType = (profileConfirmType) => {

    if (profileConfirmType == 0)
      return (< Badge className="ml-1 badge-glow" color="success" > accepted</Badge >)
    if (profileConfirmType == 1)
      return (<Badge className="ml-1 badge-glow" color="danger">rejected</Badge>)
    if (profileConfirmType == 2)
      return (<Badge className="ml-1 badge-glow" color="info">pending</Badge>)
  }

  returnDegreeType = (degreeType) => {
    if (degreeType == 0) {
      return "under-diploma";
    }
    if (degreeType == 1) {
      return "diploma";
    }
    if (degreeType == 2) {
      return "assosiate";
    }
    if (degreeType == 3) {
      return "bachelor";
    }
    if (degreeType == 4) {
      return "masters";
    }
    if (degreeType == 5) {
      return "doctorate";
    }
  };

  groupBy = (array, key) => {
    return array.reduce((result, obj) => {
      (result[obj[key]] = result[obj[key]] || []).push(obj);
      return result;
    }, {});
  };

  // handleAcceptService

  handleReject = async (id) => {
    Swal.fire({
      title: "reason for rejecting ",
      input: "text",
      inputAttributes: {
        autocapitalize: "off",
      },
      showCancelButton: true,
      confirmButtonText: "Reject",
      showLoaderOnConfirm: true,
      preConfirm: async (text) => {
        try {
          const obj = { serviceId: id, resonForReject: text, userName: this.state.data.userName, isConfirmed: false };
          const {
            data,
          } = await agent.User.confirmProfileForAdmin(obj);
          console.log(data);
          if (data.result.status) {
            await this.setState({ requiredFiles: this.state.requiredFiles.map(el => (el.serviceId === id ? Object.assign({}, el, { profileConfirmType: 1 }) : el)) });
            Swal.fire("Successful accepting", "profile successfully rejected", "success");
          }
        } catch (ex) {
          let res = this.handleCatch2(ex);
          Swal.showValidationMessage(`${res}`);
        }
      },
      allowOutsideClick: () => !Swal.isLoading(),
    }).then((result) => {
      // if (result.isConfirmed) {
      //   Swal.fire({
      //     title: `${result.value.login}'s avatar`,
      //     imageUrl: result.value.avatar_url
      //   })
      // }
    });
  };

  handleAccept = async (id) => {
    // this.setState({ loadngDelete: true });
    Swal.fire({
      title: "Are you sure?",
      text: "confirm profile for this service ",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes, consfirm it!",
    }).then(async (result) => {
      if (result.isConfirmed) {
        Swal.fire("loading ...");
        try {
          const obj = { serviceId: id, userName: this.state.data.userName, isConfirmed: true };
          const {
            data,
          } = await agent.User.confirmProfileForAdmin(obj);

          if (data.result.status) {
            await this.setState({ requiredFiles: this.state.requiredFiles.map(el => (el.serviceId === id ? Object.assign({}, el, { profileConfirmType: 0 }) : el)) });
            Swal.fire("Successful accepting", "profile successfully accepted", "success");
          }
        } catch (ex) {
          let res = this.handleCatch2(ex);
          Swal.fire(`Error!`, `${res}`, "warning");
        }

      }
    });


  };


  handleCatch2 = (ex) => {
    console.log(ex);
    if (ex?.response?.status == 400) {
      const errors = ex?.response?.data?.errors;
      // this.setState({ errors });
      return errors[0];
    } else if (ex?.response) {
      const errorMessage = ex?.response?.data?.message;
      return errorMessage;
    }
  };





  returnConfirmType = (confirmType) => {
    if (confirmType == 2) {
      return false
    }
    else if (confirmType == 0) {
      return false;
    }
    return true
  };



  returnRejectType = (rejectType) => {
    if (rejectType == 2)
      return false
    else if (rejectType == 1)
      return false;
    return true
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
      <>
        {console.log(this.state.groupingRequiredFile)}
        <Col sm="10" className="mx-auto">
          <Card>
            <CardHeader>
              <CardTitle className="w-100 text-center">
                {" "}
                User Information{" "}
              </CardTitle>
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
              <form>
                <FormGroup row>
                  <Col md="6" style={{ height: "252px" }}>
                    <img
                      style={{
                        height: "100%",
                        borderRadius: "12px !important",
                      }}
                      src={
                        data.imageAddress != null
                          ? baseUrl + data.imageAddress
                          : profile
                      }
                      alt="CoverImg"
                      className="img-fluid bg-cover w-100 rounded-0"
                    />

                    {/* {data.imageAddress != null ?
                      <img
                        style={{ height: "100%", borderRadius: "12px !important" }}
                        src={baseUrl + data.imageAddress}
                        alt="CoverImg"
                        className="img-fluid bg-cover w-100 rounded-0"
                      />
                      :
                      <img
                        style={{ height: "100%", borderRadius: "12px !important" }}
                        src={profile}
                        alt="CoverImg"
                        className="img-fluid bg-cover w-100 rounded-0"
                      />

                    } */}
                  </Col>
                  <Col md="6">
                    {data.videoAddress != null ? (
                      <iframe
                        className="embed-responsive-item w-100 height-250 mb-1"
                        src={baseUrl + data.videoAddress}
                        allowFullScreen
                        title="post"
                        frameBorder="0"
                      />
                    ) : (
                        <h3> there is no video for User</h3>
                      )}
                  </Col>
                </FormGroup>

                <FormGroup row>
                  <Col md="6">
                    <Input name="name" value={data.name} label={"Name"} />
                  </Col>
                  <Col md="6">
                    <Input
                      name="LastName"
                      value={data.lastName}
                      label={"Last Name"}
                    />
                  </Col>
                </FormGroup>

                <FormGroup row>
                  <Col md="6">
                    <Input
                      name="username"
                      value={data.userName}
                      label={"Username"}
                    />
                  </Col>

                  <Col md="6">
                    <Input name="email" value={data.email} label={"Email"} />
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
                <h3> fileds </h3>
                {this.state?.fields?.length === 0 ? (
                  <h3 className="text-center"> there no filed for user </h3>
                ) : (
                    <FormGroup row>
                      {this.state.fields?.map((field, index) => (
                        <>
                          <Col key={index} md="5" className="mt-2">
                            <button
                              type="button"
                              key={index}
                              className="text-center text-white form-control btn-warning"
                            >
                              {`${field.title}(${this.returnDegreeType(field.degreeType)})`}
                              {/* {field.title}( {this.returnDegreeType(field.degreeType)}) */}
                            </button>
                          </Col>
                        </>
                      ))}
                    </FormGroup>
                  )}

                <hr></hr>
                <h3>required file for user </h3>
                <hr></hr>
                {requiredFiles?.length == 0 ? (
                  <h3 className="text-center"> there no file for user </h3>
                ) : (
                    <FormGroup row>
                      {requiredFiles?.map((requireFIle, index) => (
                        <>
                          <Col key={index} md="12" className="mt-4">
                            <hr />
                            <p> {requireFIle.serviceName}
                              {this.returnProfileConfirmType(requireFIle.profileConfirmType)}
                            </p>
                            {requireFIle?.files?.map((item) => {
                              return (
                                <>
                                  <div className="mb-1">
                                    <input type="hidden" value={item?._id} />
                                    <label></label>
                                    <a
                                      type="button"
                                      href={baseUrl + item.fileAddress}
                                      target="_blank"
                                      className="text-center text-white form-control btn-success"
                                    >
                                      {item.serviceName}
                                    </a>

                                  </div>
                                </>
                              );
                            })}


                            <button
                              type="button"
                              // disabled={this.returnConfirmType(requireFIle.profileConfirmType)}
                              disabled={requireFIle.profileConfirmType == 0 ? true : false}
                              className="mr-1 text-center btn btn-primary text-center "
                              onClick={() => {
                                this.handleAccept(requireFIle.serviceId)
                              }}
                            >
                              confirm
                            </button>

                            <button
                              type="button"
                              // disabled={this.returnRejectType(requireFIle.profileConfirmType)}
                              disabled={requireFIle.profileConfirmType == 1 ? true : false}

                              // disabled={!(requireFIle.profileConfirmType == 2 && requireFIle.profileConfirmType == 1)}
                              className=" text-center btn btn-danger text-center "
                              onClick={() => {
                                this.handleReject(requireFIle.serviceId)
                              }}
                            >
                              reject
                            </button>
                          </Col>
                        </>
                      ))}
                    </FormGroup>
                  )}

              </form>
            </CardBody>
          </Card>
        </Col>
      </>
    );
  }
}
export default _DetailsUser;

const Input = ({ name, value, label }) => {
  return (
    <>
      <label htmlFor={name}> {label}</label>
      <input
        value={value}
        readOnly
        name={name}
        id={name}
        className={`form-control `}
      />
    </>
  );
};
