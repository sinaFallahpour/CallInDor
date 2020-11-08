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
import { PlusSquare, Check, Edit2, Settings, Menu, X } from "react-feather";


// import coverImg from "../../../assets/img/profile/user-uploads/cover.jpg"
// import profileImg from "../../../assets/img/profile/user-uploads/user-13.jpg"

import profile from "../../../assets/img/_custom/user-profile.png"

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

      this.setState({
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


  returnDegreeType = (degreeType) => {

    // [Description("کارشناسی")]
    // Bachelor,
    //   [Description("کارشناسی ارشد")]
    // Masters,
    //   //[Description("کارشناسی ارشد")]
    //   //SeniorMaster,
    //   [Description("دکترا")]
    // Doctorate,
    //   [Description("فوق دکترا")]
    // SeniorDoctorate,
    //   [Description("فوق دیپلم")]
    // Assosiate,
    //   [Description("دیپلم")]
    // Diploma,
    //   [Description("زیر دیپلم")]
    // UnderDiploma,


    if (degreeType == 0) {
      return "under-diploma"
    }
    if (degreeType == 1) {
      return "diploma"
    } if (degreeType == 2) {
      return "assosiate"
    } if (degreeType == 3) {
      return "bachelor"
    } if (degreeType == 4) {
      return "masters"
    } if (degreeType == 5) {
      return "doctorate"
    }

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
      <>

        <Col sm="10" className="mx-auto">
          <Card>
            <CardHeader>
              <CardTitle class="w-100 text-center"> User Information </CardTitle>
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
                  <Col md="6" style={{ height: "252px" }} >
                    <img
                      style={{ height: "100%", borderRadius: "12px !important" }}
                      src={data.imageAddress ? profile : baseUrl + data.imageAddress}
                      alt="CoverImg"
                      className="img-fluid bg-cover w-100 rounded-0"
                    />
                  </Col>
                  <Col md="6">
                    {data.videoAddress ?
                      <iframe
                        className="embed-responsive-item w-100 height-250 mb-1"
                        src={baseUrl + data.videoAddress}
                        allowFullScreen
                        title="post"
                        frameBorder="0"
                      />
                      :
                      <h3> there is no video for User</h3>
                    }
                  </Col>
                </FormGroup>

                <FormGroup row>
                  <Col md="6">
                    <Input name="name" value={data.name} label={"Name"} />
                  </Col>
                  <Col md="6">
                    <Input name="LastName" value={data.lastName} label={"Last Name"} />
                  </Col>
                </FormGroup>

                <FormGroup row>
                  <Col md="6">
                    <Input name="username" value={data.userName} label={"Username"} />
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
                {this.state?.fields?.length === 0 ?
                  <h3 className="text-center"> there no filed for user  </h3>
                  :
                  <FormGroup row>
                    {this.state.fields?.map((field, index) => (
                      <>
                        <Col key={index} md="3" className="mt-2">
                          <button type="button" key={index} className="text-center text-white form-control btn-warning">
                            {field.title}({this.returnDegreeType(field.degreeType)})  </button>
                        </Col>

                      </>
                    ))}
                  </FormGroup>
                }

                <hr></hr>
                <h3>required file for user </h3>
                <hr></hr>

                {this.state?.requiredFiles?.length == 0
                  ?
                  <h3 className="text-center"> there no file for user </h3>
                  :
                  (
                    <FormGroup row>
                      { this.state.requiredFiles?.map((requireFIle, index) => (
                        <>
                          <Col key={index} md="3" className="mt-1" >
                            <input type="hidden" value={requireFIle?._id} />
                            <label></label>
                            <a
                              type="button"
                              href={baseUrl + requireFIle.fileAddress}
                              target="_blank"
                              className="text-center text-white form-control btn-warning"
                            >
                              {requireFIle.serviceName}
                            </a>
                          </Col>
                        </>
                      ))}
                    </FormGroup>

                  )}


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
  )

}