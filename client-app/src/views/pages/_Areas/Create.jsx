import React from "react";
import {
  Button,
  FormGroup,
  Card,
  CardHeader,
  CardBody,
  CardTitle,
  Form as FormStrap,
  Alert,
  Spinner,
  Col,
} from "reactstrap";

// import { modalForm } from "./ModalSourceCode"
import { toast } from "react-toastify";
import ReactTagInput from "@pathofdev/react-tag-input";
import "@pathofdev/react-tag-input/build/index.css";
import Joi from "joi-browser";

import agent from "../../../core/services/agent";
import Form from "../../../components/common/form";
import CustomTagsInput from "./CustomTagsInput";
import ModalCustom from "./ModalCustom";

class Create extends Form {
  state = {
    data: {
      id: null,
      title: "",
      persianTitle: "",
      serviceId: null,
    },
    services: [],
    Specialities: [],
    isEnabled: false,
    isProfessional: false,

    modal: false,

    errors: {},
    errorscustom: [],
    errorMessage: "",
    Loading: false,
  };

  schema = {
    id: Joi.number(),
    title: Joi.string().required().min(3).max(100).label("English Title"),

    persianTitle: Joi.string()
      .required()
      .min(3)
      .max(100)
      .label("Persian Title"),

    serviceId: Joi.number()
      .error(() => {
        return {
          message: "service Is Required",
        };
      })
      .required()
      .label("service"),

    Specialities: Joi.label("speciality"),
  };

  async populatingServiceTypes() {
    const { data } = await agent.ServiceTypes.list();
    let services = data.result.data;
    this.setState({ services });
  }

  async componentDidMount() {
    this.populatingServiceTypes();
  }

  componentDidUpdate(prevProps, prevState) {
    const { addNew, currentArea, addToAreas } = this.props;
    // console.log(currentArea);
    // console.log(this.props.currentArea);
    // console.log("seperate");
    // console.log(prevProps.currentArea);
    if (addNew != prevProps.addNew) {
      const data = { id: null, title: "", persianTitle: "", serviceId: null };
      this.setState({
        data,
        Specialities: [],
        isEnabled: false,
        isProfessional: false,
      });
    }
    if (
      !addNew &&
      currentArea != null &&
      currentArea?.data?.id != prevProps?.currentArea?.data?.id
    ) {
      console.log(currentArea);

      this.setState({
        data: currentArea.data,
        ...currentArea,
        // Specialities: [],
      });
    }
  }

  // // // // // // // componentDidUpdate(prevProps, prevState) {
  // // // // // //   const { addNew, currentArea, addToAreas } = this.props;
  // // // // // //   console.log(this.props.currentArea);
  // // // // // //   console.log("seperate");
  // // // // // //   console.log(prevProps.currentArea);
  // // // // // //   if (
  // // // // // //     !addNew &&
  // // // // // //     currentArea != null &&
  // // // // // //     (prevProps.currentArea == null ||
  // // // // // //       currentArea?.data?.id != prevProps?.currentArea?.data?.id)
  // // // // // //   ) {
  // // // // // //     const { id, title, persianTitle, serviceId } = currentArea?.data;
  // // // // // //     const { isEnabled, isProfessional } = currentArea;
  // // // // // //     if (id != prevState.data.id) {
  // // // // // //       alert("changeId");
  // // // // // //       this.setState({ data: { ...prevState.data, id } });
  // // // // // //     }
  // // // // // //     if (title != prevState.data.title) {
  // // // // // //       alert("change title");
  // // // // // //       this.setState({ data: { ...prevState.data, title } });
  // // // // // //     }
  // // // // // //     if (persianTitle != prevState.data.persianTitle) {
  // // // // // //       alert("change persianTitle");
  // // // // // //       this.setState({ data: { ...prevState.data, persianTitle } });
  // // // // // //     }
  // // // // // //     if (serviceId != prevState.data.serviceId) {
  // // // // // //       alert("change serviceId ");
  // // // // // //       this.setState({ data: { ...prevState.data, serviceId } });
  // // // // // //     }
  // // // // // //     if (isEnabled != prevState.isEnabled) {
  // // // // // //       alert("change isEnabled ");
  // // // // // //       this.setState({ isEnabled });
  // // // // // //     }
  // // // // // //     if (isProfessional != prevState.isProfessional) {
  // // // // // //       alert("change isProfessional");
  // // // // // //       this.setState({ isProfessional });
  // // // // // //     }
  // // // // // //     // if(dont chec speciality)

  // // // // // //     // this.setState({
  // // // // // //     //   data: currentArea.data,
  // // // // // //     //   ...currentArea,
  // // // // // //     // });
  // // // // // //   }

  // // // // // //   // data: { id, title, persianTitle, serviceId },
  // // // // // //   // isEnabled,
  // // // // // //   // isProfessional,
  // // // // // //   // Specialities,
  // // // // // // }

  // handleAddSpeciality = topic => {
  //   let prevSpecialities = this.state.speciality
  //   prevSpecialities.push(topic)
  //   this.setState({ speciality: prevSpecialities })
  // };

  handleAddSpeciality = (persianName, englishName) => {
    let prevSpecialities = this.state.Specialities;
    const obj = { persianName, englishName };

    let thisSpeciality = [...prevSpecialities, obj];
    // prevSpecialities.push(obj)
    this.setState({ Specialities: thisSpeciality });
  };

  handleDeleteSpeciality = (Specialities) => {
    this.setState({ Specialities });
  };

  toggleModal = () => {
    this.setState((prevstate, prevProps) => {
      return { modal: !prevstate.modal };
    });
  };

  doSubmit = async (e) => {
    this.setState({ Loading: true });

    const errorMessage = "";
    const errorscustom = [];
    this.setState({ errorMessage, errorscustom });
    try {
      const { isEnabled, isProfessional, Specialities } = this.state;
      const obj = {
        ...this.state.data,
        isEnabled,
        isProfessional,
        Specialities,
      };
      //Add
      if (this.props.addNew) {
        const { data } = await agent.Areas.create(obj);
        if (data.result.status) {
          obj.id = data.result.data.id;
          obj.serviceName = data.result.data.serviceName;
          this.props.addToAreas(obj);
          toast.success(data.result.message);
          setInterval(() => {
            this.cleanData();
          }, 600);
        }
      }
      //Edit
      else {
        const { data } = await agent.Areas.update(obj);
        if (data.result.status) {
          obj.id = data.result.data.id;
          obj.serviceName = data.result.data.serviceName;
          this.props.editToAreas(obj);
          toast.success(data.result.message);
          // setInterval(() => {
          //   this.cleanData();
          // }, 600);
        }
      }
    } catch (ex) {
      console.log(ex);
      if (ex?.response?.status == 400) {
        const errorscustom = ex?.response?.data?.errors;
        this.setState({ errorscustom });
      } else if (ex?.response) {
        const errorMessage = ex?.response?.data?.Message;
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

  cleanData = () => {
    const data = {
      title: "",
      persianTitle: "",
      serviceId: null,
    };
    this.setState({
      data,
      Specialities: [],
      isEnabled: false,
      isProfessional: false,
    });
  };

  render() {
    const { errorscustom, errorMessage, services, modal } = this.state;
    const { addNew } = this.props;

    return (
      <React.Fragment>
        <Col sm="13" className="mx-auto">
          <Card>
            <CardHeader>
              <CardTitle> {addNew ? "Create Area" : "Edit Area"} </CardTitle>
            </CardHeader>
            <CardBody>
              {errorscustom &&
                errorscustom.map((err, index) => {
                  return (
                    <Alert key={index} className="text-center" color="danger ">
                      {err}
                    </Alert>
                  );
                })}

              <form onSubmit={this.handleSubmit}>
                <FormGroup row>
                  <Col md="4">{this.renderInput("title", "English Title")}</Col>
                  <Col md="4">
                    {this.renderInput("persianTitle", "Persian Title")}
                  </Col>
                  <Col md="4">
                    {this.renderReactSelect(
                      "serviceId",
                      "Service",
                      services.map((item) => ({
                        value: item.id,
                        label: item.name,
                      }))
                    )}
                  </Col>

                  <Col md="6">
                    {
                      <CustomTagsInput
                        handleAddSpeciality={this.handleAddSpeciality}
                        handleDeleteTopic={this.handleDeleteSpeciality}
                        toggleModal={this.toggleModal}
                        Specialities={this.state.Specialities}
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
                    onChange={(e) =>
                      this.setState({ isEnabled: !this.state.isEnabled })
                    }
                    name="isEnabled"
                    id="isEnabled"
                    type="checkbox"
                    className="ml-1"
                  />
                </div>

                <div className="form-group _customCheckbox">
                  <label htmlFor="isProfessional">Is Professional</label>
                  <input
                    value={this.state.isProfessional}
                    checked={this.state.isProfessional}
                    onChange={(e) =>
                      this.setState({
                        isProfessional: !this.state.isProfessional,
                      })
                    }
                    name="isProfessional"
                    id="isProfessional"
                    type="checkbox"
                    className="ml-1"
                  />
                </div>

                {this.state.Loading ? (
                  <Button disabled={true} color="primary" className="mb-1">
                    <Spinner color="white" size="sm" type="grow" />
                    <span className="ml-50">Loading...</span>
                  </Button>
                ) : addNew ? (
                  this.renderButton("Add")
                ) : (
                      this.renderButton("Edit")
                    )}
              </form>
            </CardBody>
          </Card>
        </Col>
        {modal && (
          <ModalCustom
            modal={modal}
            toggleModal={this.toggleModal}
            handleAddSpeciality={this.handleAddSpeciality}
          />
        )}
      </React.Fragment>
    );
  }
}
export default Create;
