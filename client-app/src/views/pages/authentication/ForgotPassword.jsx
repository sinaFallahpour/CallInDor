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
} from "reactstrap";
import fgImg from "../../../assets/img/pages/forgot-password.png";
import { history } from "../../../history";
import "../../../assets/scss/pages/authentication.scss";

// import authService from "../../../core/services/userService/authService";
import auth from "../../../core/services/userService/authService";
import agent from "../../../core/services/agent";
import { toast } from "react-toastify";

class ForgotPassword extends React.Component {
  state = {
    phoneNumber: "",
    countryCode: "",
    loading: false,

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
    this.setState({ loading: true });
    e.preventDefault();
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

  render() {
    const { errors } = this.state;
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
                    {errors &&
                      errors.map((err, index) => {
                        return (
                          <Alert key={index} color="danger">
                            {err}
                          </Alert>
                        );
                      })}
                    <Form onSubmit={this.doSubmit}>
                      <FormGroup className="form-label-group">
                        <Input
                          type="number"
                          placeholder="Phone number"
                          required
                        />
                        <Label>Phone number</Label>
                      </FormGroup>
                      <div className=" d-block mb-1">
                        <Button.Ripple
                          color="primary"
                          type="submit"
                          className="px-75 btn-block"
                        >
                          Recover Password
                        </Button.Ripple>
                      </div>

                      <div className="float-md-left d-block mb-1">
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
