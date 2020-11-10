import React from "react";
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
import classnames from "classnames";
import { Eye, Code, Plus } from "react-feather";
// import { modalForm } from "./ModalSourceCode"
import { toast } from "react-toastify";
import agent from "../../../core/services/agent";

class Create extends React.Component {
  state = {
    title: "",
    persianTitle: "",
    isEnabled: true,
    isForCourse: true,
    isSubCategory: true,
    serviceId: null,
    parentId: null,

    categories: [],
    services: [],

    errors: [],
    errorMessage: "",
    Loading: false,

    activeTab: "1",
    modal: false,
  };

  async populatingCategories() {
    const { data } = await agent.Category.listParentCatgory();
    let categories = data.result.data;
    this.setState({ categories });
  }

  async populatingServiceTypes() {
    const { data } = await agent.ServiceTypes.list();
    let services = data.result.data;
    this.setState({ services });
  }

  async componentDidMount() {
    this.populatingCategories();
    this.populatingServiceTypes();
  }

  toggleTab = (tab) => {
    if (this.state.activeTab !== tab) {
      this.setState({ activeTab: tab });
    }
  };

  toggleModal = () => {
    this.setState((prevState) => ({
      modal: !prevState.modal,
    }));
  };

  doSubmit = async (e) => {
    this.setState({ Loading: true });
    e.preventDefault();
    const errorMessage = "";
    const errors = [];
    this.setState({ errorMessage, errors });
    try {
      const {
        title,
        persianTitle,
        isEnabled,
        isForCourse,
        isSubCategory,
        serviceId,
        parentId,
      } = this.state;

      const serviceName = this.state.services.find((c) => c.id == serviceId)
        .name;
      const parentName = this.state.categories.find((c) => c.id == parentId)
        ?.persianTitle;

      const obj = {
        title,
        persianTitle,
        isEnabled,
        isForCourse,
        isSubCategory,
        serviceId,
        parentId,
        serviceName,
        parentName,
      };
      const { data } = await agent.Category.create(obj);
      if (data.result.status) {
        toast.success(data.result.message);

        obj.id = data.result.data.id;
        this.props.GetAllCategory(obj);
      }
      setTimeout(() => {
        this.setState({
          modal: false,
          Loading: false,
          title: "",
          persianTitle: "",
          serviceId: null,
          parentId: null,
        });
        // this.toggleModal()
      }, 2000);
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
      setTimeout(() => {
        this.setState({ Loading: false });
      }, 1000);
    }
  };

  render() {
    const { errorMessage, errors } = this.state;
    return (
      <React.Fragment>

        {/* <Button.Ripple color="primary" onClick={this.toggleModal}>
                  Create New Category
                </Button.Ripple> */}

        <Button
          className="add-new-btn mb-2"
          color="primary"
          onClick={this.toggleModal}
          outline
        >
          <Plus size={15} />
          <span className="align-middle">Add New</span>
        </Button>
        <Modal
          isOpen={this.state.modal}
          toggle={this.toggleModal}
          className="modal-dialog-centered "
        >
          <ModalHeader toggle={this.toggleModal}>
            New Category
          </ModalHeader>
          <ModalBody>
            {errors &&
              errors.map((err, index) => {
                return (
                  <Alert
                    key={index}
                    className="text-center"
                    color="danger "
                  >
                    {err}
                  </Alert>
                );
              })}
            <Form action="/s" className="mt-3" onSubmit={this.doSubmit}>
              <FormGroup>
                <h5 for="email">Title:</h5>
                <Input
                  type="text"
                  id="title"
                  value={this.state.title}
                  onChange={(e) => {
                    this.setState({ title: e.target.value });
                  }}
                  placeholder="Title"
                  required
                  minLength="1"
                  maxLength="100"
                />
              </FormGroup>
              <FormGroup>
                <h5 for="persianTitle">PersianTitle:</h5>
                <Input
                  type="text"
                  id="persianTitle"
                  value={this.state.persianTitle}
                  onChange={(e) =>
                    this.setState({ persianTitle: e.target.value })
                  }
                  placeholder="PersianTitle"
                  required
                  minLength="1"
                  maxLength="100"
                  className="text-right"
                />
              </FormGroup>

              <FormGroup>
                <h5 className="text-bold-600">Service types</h5>
                <Input
                  required
                  type="select"
                  name="serviceId"
                  onChange={(e) =>
                    this.setState({ serviceId: e.target.value })
                  }
                >
                  <option key={-1} value={null}></option>
                  {this.state.services.map((option) => (
                    <option key={option.id} value={option.id}>
                      {option.name}
                    </option>
                  ))}
                </Input>
              </FormGroup>

              <FormGroup>
                <h5 className="text-bold-600">Parent Category</h5>
                <Input
                  type="select"
                  name="parentId"
                  onChange={(e) =>
                    this.setState({ parentId: e.target.value })
                  }
                >
                  <option key={-1} value={null}></option>
                  {this.state.categories.map((option) => (
                    <option key={option.id} value={option.id}>
                      {option.title}
                    </option>
                  ))}
                </Input>
              </FormGroup>

              <FormGroup className="ml-1 _customCheckbox">
                <Label check>
                  <Input
                    type="checkbox"
                    value={this.state.isEnabled}
                    onChange={(e) =>
                      // alert(e.target.value)
                      this.setState({
                        isEnabled:
                          e.target.value == "on" ? true : false,
                      })
                    }
                    name="isEnabled"
                    defaultChecked
                  />
                          Is Active?
                          {/* {this.state.isEnabled ? "active" : "not active"} */}
                </Label>
              </FormGroup>

              <FormGroup className="ml-1 _customCheckbox">
                <Label check>
                  <Input
                    type="checkbox"
                    value={this.state.isForCourse}
                    onChange={(e) =>
                      // alert(e.target.value)
                      this.setState({
                        isForCourse:
                          e.target.value == "on" ? true : false,
                      })
                    }
                    name="isForCourse"
                    defaultChecked
                  />
                          Is this category for Course?
                          {/* {this.state.isEnabled ? "active" : "not active"} */}
                </Label>
              </FormGroup>

              <FormGroup className="ml-1 _customCheckbox">
                <Label check>
                  <Input
                    type="checkbox"
                    value={this.state.isSubCategory}
                    onChange={(e) =>
                      // alert(e.target.value)
                      this.setState({
                        isSubCategory:
                          e.target.value == "on" ? true : false,
                      })
                    }
                    name="isEnabled"
                    defaultChecked
                  />
                          Is this a sub category ?
                          {/* {this.state.isEnabled ? "active" : "not active"} */}
                </Label>
              </FormGroup>

              {this.state.Loading ? (
                <Button
                  disabled={true}
                  color="primary"
                  className="mb-1"
                >
                  <Spinner color="white" size="sm" type="grow" />
                  <span className="ml-50">Loading...</span>
                </Button>
              ) : (
                  <Button color="primary">submit</Button>
                )}
            </Form>
          </ModalBody>
        </Modal>
        {/* <TabPane className="component-code" tabId="2">
                {modalForm}
              </TabPane> */}

      </React.Fragment>
    );
  }
}
export default Create;
