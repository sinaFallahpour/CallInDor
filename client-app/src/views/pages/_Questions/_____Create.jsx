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
  Spinner as ReactStrapSpinner,
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
import Spinner from "../../../components/@vuexy/spinner/Loading-spinner";

class Create extends Form {
  state = {
    data: {
      id: 0,
      englishText: "",
      text: "",
      serviceId: null,
      // roleId: null,
    },


    services: [],
    answersTBLs: [],
    // roles: [],

    isEnabled: false,
    // isProfessional: false,

    modal: false,

    errors: {},
    errorscustom: [],
    errorMessage: "",
    Loading: true,
  };

  schema = {
    id: Joi.number().allow(null),
    englishText: Joi
      .string()
      .required()
      .min(3)
      .max(100)
      .label("English Text"),

    text: Joi.string()
      .required()
      .min(3)
      .max(100)
      .label("Persian Text"),

    serviceId: Joi.number()
      .error(() => {
        return {
          message: "service Is required",
        };
      })
      .required()
      .label("service"),

    answersTBLs: Joi.label("answersTBL"),

    // roleId: Joi.string().required().label("role"),
    // roles: Joi.label("roles"),
  };

  async populatingServiceTypes() {
    const { data } = await agent.ServiceTypes.list();
    let services = data.result.data;
    this.setState({ services });
  }


  async componentDidMount() {
    this.populatingServiceTypes();
    this.setState({ Loading: false });
  }

  componentDidUpdate(prevProps, prevState) {
    const { addNew, currentQuestion, addToQuestions } = this.props;

    if (addNew != prevProps.addNew) {
      const data = { id: null, englishText: "", text: "", serviceId: null };
      this.setState({
        data,
        answersTBLs: [],
        isEnabled: false,
        // isProfessional: false,
      });
    }
    if (
      !addNew &&
      currentQuestion != null &&
      currentQuestion?.data?.id != prevProps?.currentQuestion?.data?.id
    ) {
      console.log(currentQuestion);

      this.setState({
        data: currentQuestion.data,
        ...currentQuestion,
        // answersTBLs: [],
      });
    }
  }

  // handleAddQuestion = topic => {
  //   let prevanswersTBLs = this.state.speciality
  //   prevanswersTBLs.push(topic)
  //   this.setState({ speciality: prevanswersTBLs })
  // };
  // handleAddQuestion
  handleAddQuestion = (persianName, englishName) => {
    let prevanswersTBLs = this.state.answersTBLs;
    const obj = { persianName, englishName };

    let answersTBLs = [...prevanswersTBLs, obj];
    // prevanswersTBLs.push(obj)
    this.setState({ answersTBLs });
  };

  handleDeleteQuestion = (answersTBLs) => {
    this.setState({ answersTBLs });
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
      const { isEnabled, answersTBLs } = this.state;
      const obj = {
        ...this.state.data,
        isEnabled,
        answersTBLs,
      };
      //Add
      if (this.props.addNew) {
        const { data } = await agent.Questions.create(obj);
        if (data.result.status) {
          obj.id = data.result.data.id;
          obj.serviceName = data.result.data.serviceName;
          this.props.addToQuestions(obj);
          toast.success(data.result.message);
          setTimeout(() => {
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
          this.props.editToQuestions(obj);
          toast.success(data.result.message);
          // setTimeout(() => {
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
      englishText: "",
      text: "",
      serviceId: null,
      // roleId: null,
    };

    this.setState({
      data,
      answersTBLs: [],
      isEnabled: false,
      // isProfessional: false,
    });
  };

  render() {
    const { errorscustom, errorMessage, services, modal, Loading } = this.state;
    const { addNew } = this.props;

    return (
      <React.Fragment>
        {Loading && (
          <>
            <Spinner />
          </>
        )}
        <Col sm="13" className="mx-auto">
          <Card>
            <CardHeader>
              <CardTitle> {addNew ? "Create Question" : "Edit Question"} </CardTitle>
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
                  <Col md="4">{this.renderInput("englishText", "English Text")}</Col>
                  <Col md="4">
                    {this.renderInput("text", "Persian Title")}
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
                        handleAddQuestion={this.handleAddQuestion}
                        handleDeleteTopic={this.handleDeleteQuestion}
                        toggleModal={this.toggleModal}
                        answersTBLs={this.state.answersTBLs}
                      // isProfessional={this.state.isProfessional}
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

                {/* <div className="form-group _customCheckbox">
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
                </div> */}

                {this.state.Loading ? (
                  <Button disabled={true} color="primary" className="mb-1">
                    <ReactStrapSpinner color="white" size="sm" type="grow" />
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
            handleAddQuestion={this.handleAddQuestion}
          />
        )}
      </React.Fragment>
    );
  }
}
export default Create;
