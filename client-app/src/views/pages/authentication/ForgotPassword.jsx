import React from "react";
import {
  Card,
  CardHeader,
  CardTitle,
  CardBody,
  Row,
  Col,
  FormGroup,
  Form,
  Input,
  Button,
  Label,
  Alert,
  Spinner
} from "reactstrap";
import fgImg from "../../../assets/img/pages/forgot-password.png";
import { history } from "../../../history";
import "../../../assets/scss/pages/authentication.scss";
import Select from "react-select";

// import authService from "../../../core/services/userService/authService";
import auth from "../../../core/services/userService/authService";
import agent from "../../../core/services/agent";
import { toast } from "react-toastify";
import { conutryPhoneCodes } from "../../../utility/phone-codes.data";

class ForgotPassword extends React.Component {
  state = {
    phoneNumber: "",
    countryCode: "",
    loading: false,

    conutryPhoneCodes: conutryPhoneCodes,

    errors: [],
    errorMessage: "",
  };

  async componentDidMount() {
    const isloggedIn = await auth.checkTokenIsValid();
    if (isloggedIn) {
      history.push("/");
    }
    this.setState({ notload: false });
  }

  doSubmit = async (e) => {
    e.preventDefault();
    let result = this.validate(this.state)
    if (!result)
      return
    this.setState({ loading: true });
    const errorMessage = "";
    const errors = [];
    this.setState({ errorMessage, errors });
    try {
      const { data } = await agent.User.forgetPassword(this.state);
      if (data.result.status) toast.success("We sent you the new password");
    } catch (ex) {
      this.handleCatch(ex);
    } finally {
      setTimeout(() => {
        this.setState({ loading: false });
      }, 1000);
    }
  };

  handleCatch = (ex) => {
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
  };

  // sdsds = async () => {
  //   let data = await this.state.conutryPhoneCodes.filter((item) => item.dial_code === this.state.countryCode)
  //   console.log(data)
  // }


  validate = (obj) => {
    let errors = [];
    let result = true
    if (obj.phoneNumber.length != 10) {
      errors.push("The minimum PhoneNumber length is 10 characters")
      result = false
    } if (!obj.countryCode) {
      errors.push("Country code is required")
      result = false
    }
    this.setState({ errors })
    return result
  }


  getValue = () => {
    console.clear();
    var selectedCountry = conutryPhoneCodes.filter((item) => item.dial_code === this.state.countryCode)
    console.log(selectedCountry)
    var data = { value: selectedCountry[0]?.dial_code, label: selectedCountry[0]?.dial_code }
    console.log(data)
    return data;
  }



  render() {
    const { errors, loading, conutryPhoneCodes, countryCode, phoneNumber } = this.state;
    return (
      <Row className="m-0 justify-content-center">
        <Col
          sm="8"
          xl="7"
          lg="10"
          md="8"
          className="d-flex justify-content-center"
        >
          <Card className="bg-authentication rounded-0 mb-0 w-100">
            <Row className="m-0">
              <Col
                lg="6"
                className="d-lg-block d-none text-center align-self-center"
              >
                <img src={fgImg} alt="fgImg" />
              </Col>
              <Col lg="6" md="12" className="p-0">
                <Card className="rounded-0 mb-0 px-2 py-1">
                  <CardHeader className="pb-1">
                    <CardTitle>
                      <h4 className="mb-0">Recover your password</h4>
                    </CardTitle>
                  </CardHeader>
                  <p className="px-2 auth-title">
                    Please enter your phone number and we'll send you new
                    password .
                  </p>
                  <CardBody className="pt-1 pb-0">
                    <div className="mb-4">

                      {errors &&
                        errors.map((err, index) => {
                          return (
                            <Alert key={index} color="danger">
                              {err}
                            </Alert>

                          );
                        })}
                    </div>
                    <Form onSubmit={this.doSubmit} className="row">
                      <FormGroup className="form-label-group col-5">

                        <Select
                          className="React"
                          classNamePrefix="select"
                          // defaultValue={options[1]}
                          value={this.getValue()}
                          name="countryCode"
                          // defaultValue={{ value: "", label: "" }}
                          //value={value}
                          isClearable={true}
                          options={conutryPhoneCodes.map((item) => ({
                            value: item.dial_code,
                            label: item.dial_code,
                          })
                          )}
                          onChange={(e) => {
                            this.setState({ countryCode: e?.value })
                          }}

                        // onChange={(e) => {
                        //   onChange({ currentTarget: { name: name, value: e?.value } });
                        // }}
                        />
                        <Label>Country code</Label>
                      </FormGroup>
                      <FormGroup className="form-label-group col-7 p-0">
                        <Input
                          type="number"
                          placeholder="Phone number"
                          onChange={(e) => this.setState({ phoneNumber: e.target.value })}
                          value={phoneNumber}
                          required
                        />
                        <Label>Phone number</Label>
                      </FormGroup>





                      {loading ? (
                        <div className=" d-block mb-1 col-12">

                          <Button disabled={true} color="primary" className="mb-1">
                            <Spinner color="white" size="sm" type="grow" />
                            <span className="ml-50">Loading...</span>
                          </Button>
                        </div>

                      ) : (

                          <div className=" d-block mb-1 col-12">
                            <Button.Ripple
                              color="primary"
                              type="submit"
                              className="px-75 btn-block"
                            >
                              Recover Password
                        </Button.Ripple>
                          </div>
                        )}

                      {/* 
                      <div className=" d-block mb-1">
                        <Button.Ripple
                          color="primary"
                          type="submit"
                          className="px-75 btn-block"
                        >
                          Recover Password
                        </Button.Ripple>
                      </div> */}

                      <div className=" d-block mb-1 col-12">
                        <Button.Ripple
                          color="primary"
                          outline
                          className="px-75 btn-block"
                          onClick={() => history.push("/pages/login")}
                        >
                          Back to Login
                        </Button.Ripple>
                      </div>
                    </Form>
                  </CardBody>
                </Card>
              </Col>
            </Row>
          </Card>
        </Col>
      </Row>
    );
  }
}
export default ForgotPassword;
