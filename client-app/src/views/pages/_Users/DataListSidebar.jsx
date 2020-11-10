import React, { Component } from "react";
import {
  Label,
  Alert,
  Input,
  FormGroup,
  Button,
  Spinner,
  Form,
} from "reactstrap";
import { X } from "react-feather";
import PerfectScrollbar from "react-perfect-scrollbar";
import classnames from "classnames";
import { toast } from "react-toastify";

import agent from "../../../core/services/agent";

class DataListSidebar extends Component {
  state = {
    userName: "",
    newPassword: "",
    confirmPassword: "",

    loadingSubmit: false,

    errors: [],
    errorMessage: "",
  };

  addNew = false;

  componentDidUpdate(prevProps, prevState) {
    if (this.props.data !== null && prevProps.data === null) {
      if (this.props.data.userName !== prevState.userName) {
        this.setState({ userName: this.props.data.userName });
      }
    }
    if (this.props.data === null && prevProps.data !== null) {
      this.setState({
        newPassword: "",
        confirmPassword: "",
        userName: "",
      });
    }
    if (this.addNew) {
      this.setState({
        newPassword: "",
        confirmPassword: "",
        userName: "",
      });
    }
    this.addNew = false;
  }

  handleValidate = () => {
    const { newPassword, confirmPassword } = this.state;
    if (newPassword !== confirmPassword) {
      this.setState({ errors: ["Incorrect Confirm Passwrod "] });
      return false;
    }
    return true;
  };

  doSubmit = async (e) => {
    e.preventDefault();

    var res = this.handleValidate();
    if (!res) return;
    this.setState({ loadingSubmit: true });
    const errorMessage = "";
    const errors = [];
    this.setState({ errorMessage, errors });
    try {
      const { data } = await agent.User.changePassword(this.state);
      toast.success(data.result.message);
    } catch (ex) {
      this.handleCatch(ex);
    } finally {
      setTimeout(() => {
        this.setState({ loadingSubmit: false });
        this.props.handleSidebar(false, true);
      }, 2000);
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

  // handleSubmit = (obj) => {
  //   this.setState({ loadingSubmit: true });
  //   if (this.props.data !== null) {
  //     this.props.updateData(obj);
  //   } else {
  //     this.addNew = true;
  //     this.props.addData(obj);
  //   }
  //   // let params = Object.keys(this.props.dataParams).length
  //   //   ? this.props.dataParams
  //   //   : { page: 1, perPage: 4 };
  //   this.props.handleSidebar(false, true);
  //   // this.props.getData(params);
  // };

  render() {
    let { show, handleSidebar, data } = this.props;
    let { newPassword, confirmPassword, loadingSubmit, errors } = this.state;
    return (
      <div
        className={classnames("data-list-sidebar", {
          show: show,
        })}
      >
        <div className="data-list-sidebar-header mt-2 px-2 d-flex justify-content-between">
          {/* <h4>{data !== null ? "UPDATE DATA" : "ADD NEW DATA"}</h4> */}
          <h4> CHANGE PASSWORD</h4>

          <X size={20} onClick={() => handleSidebar(false, true)} />
        </div>
        <Form action="/s" onSubmit={this.doSubmit}>
          <PerfectScrollbar
            className="data-list-fields px-2 mt-3"
            options={{ wheelPropagation: false }}
          >
            {errors &&
              errors.map((err, index) => {
                return (
                  <Alert key={index} className="text-center" color="danger ">
                    {err}
                  </Alert>
                );
              })}
            <FormGroup>
              <Label for="data-newPass">New Password</Label>
              <Input
                type="text"
                value={newPassword}
                placeholder="New PassWord"
                onChange={(e) => this.setState({ newPassword: e.target.value })}
                id="data-newPass"
                required
                minLength={6}
                maxLength={20}
              />
            </FormGroup>

            <FormGroup>
              <Label for="data-confirm">Confirm Password</Label>
              <Input
                type="text"
                value={confirmPassword}
                placeholder="Confirm Password"
                onChange={(e) =>
                  this.setState({ confirmPassword: e.target.value })
                }
                id="data-confirm"
                required
                minLength={6}
                maxLength={20}
              />
            </FormGroup>
          </PerfectScrollbar>

          <div className="data-list-sidebar-footer px-2 d-flex justify-content-start align-items-center mt-1">
            {loadingSubmit ? (
              <Button disabled={true} color="primary" className="mb-1">
                <Spinner color="white" size="sm" type="grow" />
                <span className="ml-50">Loading...</span>
              </Button>
            ) : (
                <Button
                  color="primary"
                // onClick={() => this.handleSubmit(this.state)}
                >
                  {data !== null ? "Update" : "Submit"}
                </Button>
              )}

            <Button
              className="ml-1"
              color="danger"
              outline
              onClick={() => handleSidebar(false, true)}
            >
              Cancel
            </Button>
          </div>
        </Form>
      </div>
    );
  }
}
export default DataListSidebar;
