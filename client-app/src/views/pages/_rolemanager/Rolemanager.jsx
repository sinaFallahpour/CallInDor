import React from "react";
import { Row, Col } from "reactstrap";
import Breadcrumbs from "../../../components/@vuexy/breadCrumbs/BreadCrumb";
import SweetAlert from "react-bootstrap-sweetalert";

// import SubscribersGained from "./SubscriberGained";
// import RevenueGenerated from "./RevenueGenerated";
// import QuaterlySales from "./QuaterlySales";
// import OrdersReceived from "./OrdersReceived;"

import SpinnerPage from "../../../components/@vuexy/spinner/Loading-spinner";

import RoleItem from "./RoleItem";
import agent from "../../../core/services/agent";

import { Edit } from "react-feather";
import Checkbox from "../../../components/@vuexy/checkbox/CheckboxesVuexy";
import { Check, Lock } from "react-feather";
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
  Form,
  Alert,
  Spinner,
} from "reactstrap";

// import {
//     siteTraffic,
//     siteTrafficSeries,
//     activeUsers,
//     activeUsersSeries,
//     newsLetter,
//     newsLetterSeries
// } from "./StatisticsData2";
import { toast } from "react-toastify";
import { ThemeProvider } from "styled-components";

class RoleManager extends React.Component {
  state = {
    data: {
      id: "",
      name: "",
      isEnabled: true,
    },

    addNew: true,

    roles: [],
    loading: true,
    loadingSubmiting: false,

    errors: [],
    errorMessage: "",
    Loading: false,
    activeTab: "1",

    modal: false,
  };

  // async componentDidMount() {
  //     const { data } = await agent.Role.list();
  //     const courses = data.result;
  //     this.setState({ courses });
  // }

  async componentDidMount() {
    const { data } = await agent.Role.list();
    if (data?.result) {
      this.setState({ roles: data.result.data, loading: false });
      return;
    }
    toast.error(data.message, {
      autoClose: 10000,
    });
  }

  toggleModal = (role) => {
    if (!role) {
      // alert("role")
      this.setState({ modal: false });
      return;
    }
    // alert("no Role")
    this.setState((prevState) => ({
      modal: !prevState.modal,
      addNew: false,
      data: { id: role.id, name: role.name, isEnabled: role.isEnabled },
    }));
  };

  addNewModal = async (modal) => {
    // alert(modal)
    await this.setState((prev) => ({
      modal,
      addNew: true,
      data: { id: null, name: "", isEnabled: false },
    }));

    // alert(this.stat.modal)
  };

  mapToViewModel(role) {
    if (role) {
      return {
        id: role.id,
        name: role.name,
        isEnabled: role.isEnabled,
      };
    }
  }

  // toggleModal1 = () => {
  //     this.setState((prevState) => ({
  //         modal: !prevState.modal,
  //     }));
  // };

  doSubmit = async (e) => {
    this.setState({ loadingSubmiting: true });
    e.preventDefault();
    const errorMessage = "";
    const errors = [];
    this.setState({ errorMessage, errors });
    try {
      const obj = {
        name: this.state.data.name,
        isEnabled: this.state.data.isEnabled,
        id: this.state.data.id,
      };

      if (this.state.addNew) {
        const { data } = await agent.Role.create(obj);
        if (data.result.status) {
          toast.success(data.result.message);
          let roles = this.state.roles.concat(data.result.data);
          this.setState({ roles });
        }
        setTimeout(() => {
          this.setState({
            modal: false,
            loadingSubmiting: false,
            name: "",
            isEnabled: "",
            id: null,
          });
          // this.toggleModal()
        }, 2000);
      } else {
        const { data } = await agent.Role.update(obj);
        if (data.result.status) {
          toast.success(data.result.message);

          // this.setState({
          //     data: this.state.roles.map((el) =>
          //         (el.id === obj.id ? { ...el, ...obj } : el))
          // });

          // this.state.roles.map((el) => {
          //     if (el.id === obj.id) {

          //         return {
          //             ...el, name: obj.name
          //         }
          //     }
          //     else {
          //         return el;
          //     }

          // })

          this.setState({
            roles: this.state.roles.map((el) => {
              if (el.id == obj.id) {
                return {
                  ...obj,
                };
              } else {
                return el;
              }
            }),
          });
        }
        setTimeout(() => {
          this.setState({
            modal: false,
            loadingSubmiting: false,
            name: "",
            isEnabled: "",
            id: null,
          });
          // this.toggleModal()
        }, 2000);
      }
    } catch (ex) {
      console.log(ex);
      if (ex?.response?.status == 400) {
        const errors = ex?.response?.data?.errors;
        this.setState({ errors });
      } else if (ex?.response) {
        const errorMessage = ex?.response?.data?.Message;
        this.setState({ errorMessage });
        toast.error(errorMessage, {
          autoClose: 10000,
        });
      }
    } finally {
      setTimeout(() => {
        this.setState({ loadingSubmiting: false });
      }, 1000);
    }
  };

  render() {
    const { errorMessage, errors, roles, loading, data } = this.state;

    if (loading) return <SpinnerPage />;

    return (
      <React.Fragment>
        <Breadcrumbs
          breadCrumbTitle="Manage Roles"
          breadCrumbParent="Card"
          breadCrumbActive="Statistics Cards"
        />
        <CardHeader style={{ backgroundColor: "unset", borderBottom: "unset" }}>
          <Button.Ripple
            color="primary"
            onClick={() => {
              this.addNewModal(true);
            }}
          >
            Create New Service
          </Button.Ripple>
        </CardHeader>
        {/* <div onClick={() => this.handleAlert("defaultAlert", true)}>asdas</div> */}

        <Row>
          {roles.length == 0 ? (
            <h3 class="w-100 text-center">There is no role to display </h3>
          ) : (
            roles.map((role) => {
              return (
                <Col key={role.id} lg="6" sm="6">
                  <RoleItem
                    hideChart
                    iconRight
                    iconBg="success"
                    icon={<Edit className="warning" size={22} />}
                    stat={role.name}
                    isEnabled={role.isEnabled}
                    toggleModal={this.toggleModal}
                    role={role}
                  />
                </Col>
              );
            })
          )}

          {/* <Col xl="2" lg="4" sm="6">
                        <StatisticsCard
                            hideChart
                            iconBg="warning"
                            icon={<Truck className="warning" size={22} />}
                            stat="2"
                            statTitle="Returns"
                        />
                    </Col> */}
        </Row>

        <Modal
          isOpen={this.state.modal}
          toggle={() => {
            this.addNewModal(false);
          }}
          className="modal-dialog-centered"
        >
          <ModalHeader
            toggle={() => {
              this.addNewModal(false);
            }}
          >
            {this.state.addNew ? "Add Role " : "Edit Role"}
          </ModalHeader>
          <ModalBody>
            {errors &&
              errors.map((err, index) => {
                return (
                  <Alert key={index} className="text-center" color="danger ">
                    {err}
                  </Alert>
                );
              })}
            <Form action="/s" className="mt-3" onSubmit={this.doSubmit}>
              <FormGroup>
                <h5 for="email">name:</h5>
                <Input
                  type="text"
                  id="name"
                  value={this.state.data.name}
                  onChange={(e) => {
                    this.setState({
                      data: { ...this.state.data, name: e.target.value },
                    });
                  }}
                  placeholder="name"
                  required
                  minLength="1"
                  maxLength="100"
                />
              </FormGroup>
              {/* {this.state.data.isEnabled ? "غیر فعال" : " فعال "} */}
              {/* <div className="form-group">
                                                <label htmlFor="isEnabled">Is Enabled</label>
                                                <input
                                                    value={this.state.data.isEnabled}
                                                    checked={this.state.data.isEnabled}
                                                    onChange={(e) => {
                                                        console.log("caneg")
                                                        this.setState({
                                                            ...this.state.data,
                                                            isEnabled: !this.state.data.isEnabled
                                                        })
                                                    }}
                                                    name="isEnabled"
                                                    id="isEnabled"
                                                    type="checkbox"
                                                    className="ml-1" />
                                            </div> */}
              <FormGroup className="ml-1 _customCheckbox">
                <Label htmlFor="isEnabled">
                  <Input
                    type="checkbox"
                    // value={this.state.data.isEnabled}
                    checked={this.state.data.isEnabled}
                    onChange={(e) => {
                      this.setState({
                        data: {
                          ...this.state.data,
                          isEnabled: !this.state.data.isEnabled,
                        },
                      });
                    }}
                    name="isEnabled"
                    id="isEnabled"
                    defaultChecked={this.state.data.isEnabled}
                  />
                  Is this role ctive?
                </Label>
              </FormGroup>

              <Row className="mb-2">
                <Col md="6" sm="12">
                  <Label htmlFor="isEnabled">
                    <Checkbox
                      color="primary"
                      icon={<Check className="vx-icon" size={16} />}
                      label=""
                      defaultChecked={true}
                    />
                  </Label>
                  bamir sd dige
                </Col>

                <Col md="6" sm="12">
                  <Label htmlFor="isEnabled">
                    <Checkbox
                      color="primary"
                      icon={<Check className="vx-icon" size={16} />}
                      label=""
                      defaultChecked={true}
                    />
                  </Label>
                  darvazweba bi orze
                </Col>

                <Col md="6" sm="12">
                  <Label htmlFor="isEnabled">
                    <Checkbox
                      color="primary"
                      icon={<Check className="vx-icon" size={16} />}
                      label=""
                      defaultChecked={true}
                    />
                  </Label>
                  hoghoghy msool
                </Col>

                <Col md="6" sm="12">
                  <Label htmlFor="isEnabled">
                    <Checkbox
                      color="primary"
                      icon={<Check className="vx-icon" size={16} />}
                      label=""
                      defaultChecked={true}
                    />
                  </Label>
                  veryfy Services
                </Col>
              </Row>

              {/* <FormGroup className="ml-1 _customCheckbox">
                <Label htmlFor="isEnabled">
                  <Checkbox
                    color="primary"
                    icon={<Check className="vx-icon" size={16} />}
                    label=""
                    defaultChecked={true}
                  />
                </Label>
                get all user in asdmin pannekl
                <Label htmlFor="isEnabled">
                  <Checkbox
                    color="primary"
                    icon={<Check className="vx-icon" size={16} />}
                    label=""
                    defaultChecked={true}
                  />
                </Label>
                get all user in asdmin pannekl
              </FormGroup> */}

              {/* <Checkbox
                color="primary"
                icon={<Check className="vx-icon" size={16} />}
                label=""
                defaultChecked={true}
              />
              <Checkbox
                color="primary"
                icon={<Check className="vx-icon" size={16} />}
                label=""
                defaultChecked={true}
              /> */}
              {this.state.loadingSubmiting ? (
                <Button disabled={true} color="primary" className="mb-1">
                  <Spinner color="white" size="sm" type="grow" />
                  <span className="ml-50">Loading...</span>
                </Button>
              ) : (
                <Button color="primary">submit</Button>
              )}
            </Form>
          </ModalBody>
        </Modal>
      </React.Fragment>
    );
  }
}

export default RoleManager;
