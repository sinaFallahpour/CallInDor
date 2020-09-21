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
} from "reactstrap";
import classnames from "classnames";
import { Eye, Code } from "react-feather";
// import { modalForm } from "./ModalSourceCode"
import { toast } from "react-toastify";
import agent from "../../../core/services/agent";

class ModalForm extends React.Component {
  state = {
    title: "",
    persianTitle: "",
    color: "",
    isEnabled: false,
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
    const { data } = await agent.Category.list();
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

    console.log(this.state.services);
    console.log(this.state.categories);
    // const { data } = await agent.Category.list();

    // if (data?.result) {
    //   setTimeout(() => {
    //     this.setState({ rowData: data.result.data, loading: false });
    //   }, 2000);
    //   return;
    // }
    // toast.error(data.message, {
    //   autoClose: 10000,
    // });
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
        serviceId,
        parentId,
      } = this.state;
      const obj = { title, persianTitle, isEnabled, serviceId, parentId };
      const res = await agent.Category.create(obj);

      setTimeout(() => {
        this.setState({ Loading: false });
      }, 1000);
    } catch (ex) {
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
      setTimeout(() => {
        this.setState({ Loading: false });
      }, 1000);
    }
  };

  render() {
    const { errorMessage, errors } = this.state;
    return (
      <React.Fragment>
        <Card>
          <CardBody>
            <TabContent activeTab={this.state.activeTab}>
              <TabPane tabId="1">
                <Button.Ripple
                  color="success"
                  outline
                  onClick={this.toggleModal}
                >
                  Create New Category
                </Button.Ripple>
                <Modal
                  isOpen={this.state.modal}
                  toggle={this.toggleModal}
                  className="modal-dialog-centered"
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
                        />
                      </FormGroup>

                      <FormGroup>
                        <h5 className="text-bold-600">service types</h5>
                        <Input
                          type="select"
                          name="serviceId"
                          onChange={(e) =>
                            this.setState({ serviceId: e.target.value })
                          }
                        >
                          <option key={-1} value={null}>
                            select service
                          </option>
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
                          <option key={-1} value={null}>
                            select parent
                          </option>
                          {this.state.categories.map((option) => (
                            <option key={option.id} value={option.id}>
                              {option.title}
                            </option>
                          ))}

                          {/* <option>Pulp Fiction</option>
                          <option>Nightcrawler</option>
                          <option>Donnie Darko</option> */}
                        </Input>
                      </FormGroup>

                      <FormGroup className="ml-1">
                        <Label check>
                          <Input type="checkbox" name="asa" defaultChecked />
                          Active
                        </Label>
                      </FormGroup>
                      <Button color="primary">submit</Button>
                    </Form>
                  </ModalBody>
                </Modal>
              </TabPane>
              {/* <TabPane className="component-code" tabId="2">
                {modalForm}
              </TabPane> */}
            </TabContent>
          </CardBody>
        </Card>
      </React.Fragment>
    );
  }
}
export default ModalForm;
