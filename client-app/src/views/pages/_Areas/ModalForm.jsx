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
import ModalCustom from './ModalCustom';

class ModalForm extends Form {
  state = {
    data: {
      title: "",
      persianTitle: "",
      serviceId: null,
    },

    services: [],
    Specialities: [],
    isEnabled: true,
    isProfessional: true,

    modal: false,

    errors: {},
    errorscustom: [],
    errorMessage: "",
    Loading: false,
  };

  schema = {
    title: Joi.string()
      .required()
      .min(3)
      .max(100)
      .label("English Title"),

    persianTitle: Joi.string()
      .required()
      .min(3)
      .max(100)
      .label("Persian Title"),

    serviceId: Joi.number()
      .error(() => {
        return {
          message: 'service Is Required',
        };
      })
      .required()
      .label("service"),

    Specialities: Joi.label("speciality")

  };


  async populatingServiceTypes() {
    const { data } = await agent.ServiceTypes.list();
    let services = data.result.data;
    this.setState({ services });
  }


  async componentDidMount() {
    this.populatingServiceTypes();
  }


  // handleAddSpeciality = topic => {
  //   let prevSpecialities = this.state.speciality
  //   prevSpecialities.push(topic)
  //   this.setState({ speciality: prevSpecialities })
  // };

  handleAddSpeciality = (persianName, englishName) => {
    let prevSpecialities = this.state.Specialities
    const obj = { persianName, englishName }

    let thisSpeciality = [...prevSpecialities, obj]
    // prevSpecialities.push(obj)
    this.setState({ Specialities: thisSpeciality })
  }

  handleDeleteSpeciality = Specialities => {
    this.setState({ Specialities });
  };


  toggleModal = () => {
    this.setState((prevstate, prevProps) => {
      return { modal: !prevstate.modal }
    })
  }


  doSubmit = async (e) => {
    this.setState({ Loading: true });

    const errorMessage = "";
    const errorscustom = [];
    this.setState({ errorMessage, errorscustom });
    try {
      const { isEnabled, isProfessional, Specialities } = this.state
      const obj = { ...this.state.data, isEnabled, isProfessional, Specialities }

      const { data } = await agent.Areas.create(obj);
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

    const { errorscustom, errorMessage, services, modal } = this.state;
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

                  <Col md="6">
                    {
                      <CustomTagsInput
                        handleAddSpeciality={this.handleAddSpeciality}
                        handleDeleteTopic={this.handleDeleteSpeciality}
                        toggleModal={this.toggleModal}
                        speciality={this.state.Specialities}
                        isProfessional={this.state.isProfessional}
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










                <div className="form-group _customCheckbox">
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

                <div className="form-group _customCheckbox">
                  <label htmlFor="isProfessional">Is Professional</label>
                  <input
                    value={this.state.isProfessional}
                    checked={this.state.isProfessional}
                    onChange={(e) => this.setState({ isProfessional: !this.state.isProfessional })}
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

        {modal && (
          <ModalCustom modal={modal} toggleModal={this.toggleModal} handleAddSpeciality={this.handleAddSpeciality} />
        )}
      </React.Fragment>
    );
  }
}
export default ModalForm;
