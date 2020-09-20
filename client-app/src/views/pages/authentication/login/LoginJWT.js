import React from "react";
import { Link } from "react-router-dom";
import { Redirect } from 'react-router-dom'
import auth from '../../../../core/services/userService/authService'

import {
  CardBody,
  FormGroup,
  Form,
  Input,
  Button,
  Label,
  Alert,
} from "reactstrap";
import Checkbox from "../../../../components/@vuexy/checkbox/CheckboxesVuexy";
import { Mail, Lock, Check, Phone } from "react-feather";
// import { loginWithJWT } from "../../../../redux/actions/auth/loginActions";
// import { connect } from "react-redux";
// import { history } from "../../../../history";
import { toast } from "react-toastify"

class LoginJWT extends React.Component {
  state = {
    phone: "",
    password: "",
    errors: [],
    errorMessage: "",
  };


  doSubmit = async (e) => {

    e.preventDefault();
    const errorMessage = "";
    const errors = [];
    this.setState({ errorMessage, errors })
    try {
      const { phone, password } = this.state;
      const res = await auth.login(phone, password);
      // console.log(this.props)
      const state = this.props?.location?.state;
      window.location = state ? state.from.pathName : "/";
      // this.props.history.push("/")
    } catch (ex) {
      console.log(ex)
      if (ex?.response?.status == 400) {
        const errors = ex?.response?.data?.errors;
        this.setState({ errors });
      } else if (ex?.response) {
        const errorMessage = ex?.response?.data?.message;
        this.setState({ errorMessage });
        toast.error(errorMessage, {
          autoClose: 10000
        })
      }

    }








    // e.preventDefault();
    // const errorMessage = "";
    // const errors = [];
    // this.setState({ errorMessage, errors });

    // alert(this.state.phone);
    // alert(this.state.password);
    // try {
    //   const state = this.state;
    //   // const countrycode = countryCode
    //   // const { data } = {state.phone, state.password} this.state;
    //   // await auth.login(data.email, data.password);

    //   const retutnUrl = localStorage.getItem("returnUrl");
    //   localStorage.removeItem("returnUrl");
    //   window.location = retutnUrl ? retutnUrl : "/";

    //   // window.location = state ? state.from.pathName : "/";
    //   // this.props.history.push("/")
    // } catch (ex) {
    //   if (ex?.response?.status == 400) {
    //     const errors = ex?.response?.data?.errors;
    //     this.setState({ errors });
    //   } else {
    //     const errorMessage = ex?.response?.data?.message;
    //     this.setState({ errorMessage });
    //     toast(errorMessage);
    //   }

    // if (
    //   (ex.response && ex.response.status === 403) ||
    //   ex.response.status === 400
    // ) {

    //   const errors = { ...this.state.errors };
    //   const message = ex.response.data.message.message[0].message;
    //   toast(message);
    //   this.setState({ errors });
    // }
  };

  render() {

   if (auth.getCurrentUser()) return <Redirect to="/" />

    const { errorMessage, errors } = this.state;
    return (
      <React.Fragment>
        <CardBody className="pt-1">


          {errors &&
            errors.map((err, index) => {

              return (
                <Alert key={index} color="danger"> {err}</Alert>
              )
            })
          }
          {/* // errors.map((err, index) => { */}
          {/* //   return (
          //   <Alert key={index} color="danger"> {err}</Alert>
          //   )
          // }) */}


          {/* < Alert color="danger">Password is Required</Alert>
          <Alert color="danger">
            The minimum Password length is 6 characters
          </Alert>
          <Alert color="danger">
            The minimum PhoneNumber length is 10 characters
          </Alert>
          <Alert color="danger">Country Code is Required</Alert> */}

          <Form action="/s" className="mt-3" onSubmit={this.doSubmit}>
            <FormGroup className="form-label-group position-relative has-icon-left">
              <Input
                type="number"
                placeholder="Phone"
                value={this.state.phone}
                onChange={(e) => this.setState({ phone: e.target.value })}
                required
                pattern="[0-9]*"
              />
              <div className="form-control-position">
                <Phone size={15} />
              </div>
              <Label>phoneNumber</Label>
            </FormGroup>
            <FormGroup className="form-label-group position-relative has-icon-left">
              <Input
                type="password"
                placeholder="Password"
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
              <Button.Ripple color="primary" type="submit">
                Login
              </Button.Ripple>
            </div>
          </Form>
        </CardBody>
      </React.Fragment >
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
