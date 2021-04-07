import React from "react";
import { Link } from "react-router-dom";
import { Redirect } from "react-router-dom";
import auth from "../../../../core/services/userService/authService";

import {
  CardBody,
  FormGroup,
  Form,
  Input,
  Button,
  Label,
  Alert,
  Spinner,
} from "reactstrap";
// import Checkbox from "../../../../components/@vuexy/checkbox/CheckboxesVuexy";
import { Lock, Phone, Code } from "react-feather";
import Select from "react-select";

// import { loginWithJWT } from "../../../../redux/actions/auth/loginActions";
// import { connect } from "react-redux";
// import { history } from "../../../../history";
import { toast } from "react-toastify";
// import Spinner from "../../../../components/@vuexy/spinner/Loading-spinner";
import { conutryPhoneCodes } from "../../../../utility/phone-codes.data";

class LoginJWT extends React.Component {
  state = {
    phone: "",
    password: "",
    countryCode: "",
    errors: [],
    errorMessage: "",
    loading: false,

    conutryPhoneCodes: conutryPhoneCodes,
  };

  doSubmit = async (e) => {
    e.preventDefault();
    let result = this.validate(this.state);
    if (!result) return;
    this.setState({ loading: true });
    // const errorMessage = "";
    // const errors = [];
    // this.setState({ errorMessage, errors });
    try {



      let { phone, password, countryCode } = this.state;
      phone = countryCode + phone;
      const res = await auth.login(phone, password);
      setTimeout(() => {
        const state = this.props?.location?.state;
        window.location = state ? state.from.pathName : "/";
        this.setState({ loading: false });
      }, 1000);


    } catch (ex) {

      //   console.log(ex);
      //   if (ex?.response?.status == 400) {
      //     const errors = ex?.response?.data?.errors;
      //     this.setState({ errors });
      //   } else if (ex?.response) {
      //     const errorMessage = ex?.response?.data?.message;
      //     this.setState({ errorMessage });
      //     toast.error(errorMessage, {
      //       autoClose: 10000,
      //     });
      //   }
      //   setTimeout(() => {
      //     this.setState({ loading: false });
      //   }, 1000);
    } finally {
      setTimeout(() => {
        this.setState({ loading: false });
      }, 1000);
    }
  };

  validate = (obj) => {
    let errors = [];
    let result = true;
    if (obj.phone.length != 10) {
      errors.push("The minimum PhoneNumber length is 10 characters");
      result = false;
    }
    if (!obj.countryCode) {
      errors.push("Country code is required");
      result = false;
    }
    this.setState({ errors });
    return result;
  };

  getValue = () => {
    var selectedCountry = conutryPhoneCodes.filter(
      (item) => item.dial_code === this.state.countryCode
    );
    var data = {
      value: selectedCountry[0]?.dial_code,
      label: selectedCountry[0]?.dial_code,
    };
    return data;
  };

  render() {
    // if (this.state.Loading) {
    //   return <Spinner />;
    // }
    //  if (auth.getCurrentUser()) return <Redirect to="/" />;
    const { errorMessage, errors } = this.state;
    return (
      <React.Fragment>
        <CardBody className="pt-4">
          {/* {errors &&
            errors.map((err, index) => {
              return (
                <Alert key={index} color="danger">
                  {err}
                </Alert>
              );
            })} */}

          <Form action="/s" className="mt-3" onSubmit={this.doSubmit}>
            <label htmlFor="select"> Country code</label>

            <FormGroup className="form-label-group position-relative has-icon-left">
              <Select
                className="React"
                classNamePrefix="select"
                id="select"
                // defaultValue={options[1]}
                value={this.getValue()}
                name="countryCode"
                // defaultValue={{ value: "", label: "" }}
                //value={value}
                isClearable={true}
                options={conutryPhoneCodes.map((item) => ({
                  value: item.dial_code,
                  label: item.name,
                }))}
                onChange={(e) => {
                  this.setState({ countryCode: e?.value });
                }}
              />
            </FormGroup>

            <label htmlFor="_phone"> Phone number(with out country Code)</label>
            <FormGroup className="form-label-group position-relative has-icon-left">
              <Input
                type="number"
                placeholder="example: 9117110586"
                id="_phone"
                value={this.state.phone}
                onChange={(e) => this.setState({ phone: e.target.value })}
                required
                pattern="[0-9]*"
              />
              <div className="form-control-position">
                <Phone size={15} />
              </div>
              <Label>phone number(with out country Code)</Label>
            </FormGroup>

            <label htmlFor="_Password"> Password</label>

            <FormGroup className="form-label-group position-relative has-icon-left">
              <Input
                type="password"
                placeholder="Password"
                id="_Password"
                value={this.state.password}
                onChange={(e) => this.setState({ password: e.target.value })}
                required
                minLength="6"
                maxLength="20"
              />
              <div className="form-control-position">
                <Lock size={15} />
              </div>
              <Label>Password</Label>
            </FormGroup>
            <FormGroup className="d-flex justify-content-between align-items-center">
              <div className="float-right">
                <Link to="/pages/forgot-password">Forgot Password?</Link>
              </div>
            </FormGroup>
            <div className="d-flex justify-content-between">
              {/* <Button.Ripple color="primary" type="submit" >
                Login
              </Button.Ripple> */}

              {this.state.loading ? (
                <Button disabled={true} color="primary" className="mb-1">
                  <Spinner color="white" size="sm" type="grow" />
                  <span className="ml-50">Loading...</span>
                </Button>
              ) : (
                <Button.Ripple color="primary" type="submit">
                  Login
                </Button.Ripple>
              )}
            </div>
          </Form>
        </CardBody>
      </React.Fragment>
    );
  }
}

export default LoginJWT;

// const mapStateToProps = state => {
//   return {
//     values: state.auth.login
//   }
// }
// export default connect(mapStateToProps, { loginWithJWT })(LoginJWT)
