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
  Form as FormStrap,
  Alert,
  Spinner,
  Col
} from "reactstrap";

// import { modalForm } from "./ModalSourceCode"
import { toast } from "react-toastify";
import ReactTagInput from "@pathofdev/react-tag-input";
import "@pathofdev/react-tag-input/build/index.css";
import Joi from "joi-browser";

import agent from "../../../core/services/agent";
import Form from "../../../components/common/form";
import CustomTagsInput from "./CustomTagsInput";

class ModalForm extends Form {
  state = {
    data: {
      title: "",
      persianTitle: "",
      serviceId: null,
    },

    services: [],
    speciality: [],
    isEnabled: true,
    IsProfessional: true,
    errors: {},
    errorscustom: [],
    errorMessage: "",
    Loading: false,
  };


  schema = {
    title: Joi.string()
      .label("English Title"),

    persianTitle: Joi.string()
      .required()
      .label("Persian Title"),

    serviceId: Joi.number()
      .error(() => {
        return {
          message: 'service Is Required',
        };
      })
      .required()
      .label("service"),

    speciality: Joi.label("speciality")

  };





  async populatingServiceTypes() {
    const { data } = await agent.ServiceTypes.list();
    let services = data.result.data;
    this.setState({ services });
  }


  async componentDidMount() {
    this.populatingServiceTypes();
  }



  handleAddSpeciality = topic => {
    let prevSpecialities = this.state.speciality
    prevSpecialities.push(topic)
    this.setState({ speciality: prevSpecialities })
  };

  handleDeleteSpeciality = speciality => {
    this.setState({ speciality });
  };




  doSubmit = async (e) => {
    this.setState({ Loading: true });

    const errorMessage = "";
    const errorscustom = [];
    this.setState({ errorMessage, errorscustom });
    try {
      const { isEnabled, tags, persinaTags } = this.state
      const obj = { ...this.state.data, isEnabled, tags: tags.join(), persinaTags: persinaTags.join() }

      const { data } = await agent.ServiceTypes.create(obj);
      if (data.result.status)
        toast.success(data.result.message)
    } catch (ex) {
      console.log(ex);
      if (ex?.response?.status == 400) {
        const errorscustom = ex?.response?.data?.errors;
        this.setState({ errorscustom });
      } else if (ex?.response) {
        const errorMessage = ex?.response?.data?.message;
        this.setState({ errorMessage });
        toast.error(errorMessage, {
          autoClose: 10000,
        });
      }
    }
    setTimeout(() => {
      this.setState({ Loading: false });
    }, 200);
  };


  render() {
    const { errorscustom, errorMessage, services } = this.state;
    return (
      <React.Fragment>

        <Col sm="10" className="mx-auto">
          <Card >
            <CardHeader>
              <CardTitle> Create Area </CardTitle>
            </CardHeader>
            <CardBody>
              {errorscustom &&
                errorscustom.map((err, index) => {
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

              <form onSubmit={this.handleSubmit}>
                <FormGroup row>
                  <Col md="4">
                    {this.renderInput("title", "English Title")}
                  </Col>
                  <Col md="4">
                    {this.renderInput("persianTitle", "Persian Title")}
                  </Col>
                  <Col md="4">
                    {this.renderReactSelect("serviceId", "Service", services.map(item => ({ value: item.id, label: item.name })))}
                  </Col>

                  <Col md="4">
                    {
                      <CustomTagsInput
                        handleAddTopic={this.handleAddSpeciality}
                        handleDeleteTopic={this.handleDeleteSpeciality}
                        topics={this.state.speciality}
                      />
                    }

                  </Col>
                </FormGroup>




                {/* <Col lg="6" className="mb-4">
                  {
                    <CustomTagsInput
                      handleAddTopic={this.handleAddTopic}
                      handleDeleteTopic={this.handleDeleteTopic}
                      topics={this.state.speciality}
                    />
                  }
                </Col> */}











                {/* {this.renderInput("title", "English Title")} */}
                {/* {this.renderInput("persianTitle", "Persian Title")} */}

                <div className="form-group">
                  <label htmlFor="isEnabled">Is Enabled</label>
                  <input
                    value={this.state.isEnabled}
                    checked={this.state.isEnabled}
                    onChange={(e) => this.setState({ isEnabled: !this.state.isEnabled })}
                    name="isEnabled"
                    id="isEnabled"
                    type="checkbox"
                    className="ml-1" />
                </div>

                <div className="form-group">
                  <label htmlFor="isProfessional">Is Professional</label>
                  <input
                    value={this.state.IsProfessional}
                    checked={this.state.IsProfessional}
                    onChange={(e) => this.setState({ IsProfessional: !this.state.IsProfessional })}
                    name="isProfessional"
                    id="isProfessional"
                    type="checkbox"
                    className="ml-1" />
                </div>



                {this.state.Loading ?
                  <Button disabled={true} color="primary" className="mb-1">
                    <Spinner color="white" size="sm" type="grow" />
                    <span className="ml-50">Loading...</span>
                  </Button>
                  :
                  this.renderButton("Save")
                }

              </form>

            </CardBody>
          </Card>
        </Col>
      </React.Fragment>
    );
  }
}
export default ModalForm;
