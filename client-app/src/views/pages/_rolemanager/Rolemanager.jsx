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

import { Edit, Plus } from "react-feather";
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
      rolesPermission: [],
    },

    addNew: true,

    roles: [],
    permissions: [],
    loading: true,
    loadingSubmiting: false,

    errors: [],
    errorMessage: "",

    activeTab: "1",

    modal: false,
  };

  async populatingRoles() {
    const { data } = await agent.Role.list();
    let roles = data.result.data;
    this.setState({ roles });
  }

  async populatingPermissions() {
    const { data } = await agent.Permissions.list();
    let permissions = data.result.data;
    this.setState({ permissions });
  }

  async componentDidMount() {
    await this.populatingRoles();
    this.populatingPermissions();
    this.setState({ loading: false });
  }

  toggleModal = (role) => {
    if (!role) {
      this.setState({ modal: false });
      return;
    }
    this.setState((prevState) => ({
      modal: !prevState.modal,
      addNew: false,
      data: {
        id: role.id,
        name: role.name,
        isEnabled: role.isEnabled,
        rolesPermission: role.rolesPermission,
      },
    }));
  };

  addNewModal = async (modal) => {
    await this.setState((prev) => ({
      modal,
      addNew: true,
      data: { id: null, name: "", isEnabled: false, rolesPermission: [] },
    }));
  };

  mapToViewModel(role) {
    if (role) {
      return {
        id: role.id,
        name: role.name,
        isEnabled: role.isEnabled,
        rolesPermission: role.rolesPermission,
      };
    }
  }

  roleEnclude = (permissionId) => {
    if (this.state.data.rolesPermission.includes(permissionId)) {
      return true;
    } else {
      return false;
    }
  };

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
        // rolesPermission: this.state.data.rolesPermission
      };
      obj.premissions = this.state.data.rolesPermission;
      if (this.state.addNew) {
        const { data } = await agent.Role.create(obj);
        if (data.result.status) {
          toast.success(data.result.message);
          let roles = this.state.roles.concat({
            ...data.result.data,
            rolesPermission: obj.premissions,
          });
          this.setState({ roles });
        }
        setTimeout(() => {
          this.setState({
            modal: false,
            loadingSubmiting: false,
            data: { name: "", id: null, rolesPermission: [] },
            // name: "",
            // isEnabled: "",
            // id: null,
          });
          // this.toggleModal()
        }, 2000);
      } else {
        const { data } = await agent.Role.update(obj);
        if (data.result.status) {
          toast.success(data.result.message);
          this.setState({
            roles: this.state.roles.map((el) => {
              if (el.id == obj.id) {
                return {
                  ...obj,
                  rolesPermission: obj.premissions,
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
            data: { name: "", id: null, rolesPermission: [] },
            // name: "",
            // isEnabled: "",
            // id: null,
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
          <Button
            className="add-new-btn mb-2"
            color="primary"
            // onClick={this.toggleModal}
            onClick={() => {
              this.addNewModal(true);
            }}
            outline
          >
            <Plus size={15} />
            <span className="align-middle">Add New</span>
          </Button>
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
                {this.state.permissions?.map((item, index) => {
                  return (
                    <Col md="6" sm="12">
                      <Label>
                        <Checkbox
                          value={item.id}
                          color="primary"
                          icon={<Check className="vx-icon" size={16} />}
                          label=""
                          // defaultChecked={true}
                          // checked={this.state.data.rolesPermission.includes(item.id) ? true : false}
                          checked={this.roleEnclude(item.id)}
                          defaultChecked={this.roleEnclude(item.id)}
                          onChange={async (e) => {
                            if (
                              data.rolesPermission.indexOf(
                                parseInt(e.target.value)
                              ) !== -1
                            ) {
                              let rolesPermission = data.rolesPermission.filter(
                                (item) => {
                                  return item != parseInt(e.target.value);
                                }
                              );
                              this.setState(
                                { data: { ...data, rolesPermission } },
                                function () { }
                              );
                            } else {
                              let rolesPermission = data.rolesPermission;
                              rolesPermission.push(parseInt(e.target.value));
                              this.setState(
                                { data: { ...data, rolesPermission } },
                                function () { }
                              );
                            }
                          }}
                        />
                      </Label>
                      {item.id}
                      {item.title}
                    </Col>
                  );
                })}

                {/* <Col md="6" sm="12">
                  <Label >
                    <Checkbox
                      value={1}
                      color="primary"
                      icon={<Check className="vx-icon" size={16} />}
                      label="hoghoghy msool"
                      // defaultChecked={true}
                      checked={true}
                    />
                  </Label>

                </Col> */}

                {/* <Col md="6" sm="12">
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
                      value={1}
                      color="primary"
                      icon={<Check className="vx-icon" size={16} />}
                      label="hoghoghy msool"
                      // defaultChecked={true}
                      checked={true}
                    />
                  </Label>

                </Col>

                <Col md="6" sm="12">
                  <Label htmlFor="isEnabled">
                    <Checkbox
                      color="primary"
                      icon={<Check className="vx-icon" size={16} />}
                      label=""
                      // defaultChecked={true}
                      checked={false}

                    />
                  </Label>
                  veryfy Services
                </Col> */}
              </Row>
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
