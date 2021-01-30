import React from "react";
import {
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  FormGroup,
  Input,
  Form,
  Alert,
  Spinner,
  Col,
} from "reactstrap";
// import classnames from "classnames";
// import { Eye, Code, Plus, Check } from "react-feather";

import Select from "react-select";

// import { modalForm } from "./ModalSourceCode"
// import Checkbox from "../../../components/@vuexy/checkbox/CheckboxesVuexy";

import { toast } from "react-toastify";
import agent from "../../../core/services/agent";

class FormModal extends React.Component {
  state = {
    id: null,
    text: "",
    englishText: "",
    // isEnabled: false,

    answer1: "",
    answerEnglish1: "",
    // isEnabled1: "",

    answer2: "",
    answerEnglish2: "",
    // isEnabled2: false,

    answer3: "",
    answerEnglish3: "",
    // isEnabled3: false,

    serviceId: null,

    // categories: [],
    services: [],

    errors: [],
    errorMessage: "",
    errorscustom: [],

    Loading: false,

    activeTab: "1",
    modal: false,
  };

  componentDidMount() {}

  componentDidUpdate(prevProps, prevState) {
    //این دیتا جدیده نسبت به قبل ایدی ها تغییر کرد
    if (this.props.currentData !== null) {
      if (this.props.currentData.id != prevState.id) {
        const {
          id,
          text,
          englishText,
          isEnabled,

          answer1,
          answerEnglish1,
          isEnabled1,
          key1,

          answer2,
          answerEnglish2,
          isEnabled2,
          key2,

          answer3,
          answerEnglish3,
          isEnabled3,
          key3,

          // serviceId,
          serviceName,
        } = this.props.currentData;

        this.setState({
          id,
          text,
          englishText,
          isEnabled,

          answer1,
          answerEnglish1,
          isEnabled1,
          key1,

          answer2,
          answerEnglish2,
          isEnabled2,
          key2,

          answer3,
          answerEnglish3,
          isEnabled3,
          key3,
          // serviceId,
          serviceName,

          errorMessage: "",
          errorscustom: [],

          modal: this.props.modal,
          // services: this.props.services.
          // services: this.props.services?.map((item) => {
          //   return {
          //     value: item.id,
          //     label: item.name,
          //   }
          // })
        });
      }
    }

    //افزودن جدید و قبلیش یک ادیت بود
    if (this.props.currentData === null && prevProps.currentData !== null) {
      this.setState({
        id: null,
        text: "",
        englishText: "",
        isEnabled: false,

        answer1: "",
        answerEnglish1: "",
        isEnabled1: false,
        key1: "",

        answer2: "",
        answerEnglish2: "",
        isEnabled2: false,
        key2: "",

        answer3: "",
        answerEnglish3: "",
        isEnabled3: false,
        key3: "",

        // serviceId,
        serviceName: "",

        modal: false,

        errorMessage: "",
        errorscustom: [],
      });
    }

    // this.addNew = false;

    // if (this.props.data !== null && prevProps.data === null) {
    //   if (this.props.data.userName !== prevState.userName) {
    //     this.setState({ userName: this.props.data.userName });
    //   }
    // }
    // if (this.props.data === null && prevProps.data !== null) {
    //   this.setState({
    //     newPassword: "",
    //     confirmPassword: "",
    //     userName: "",
    //   });
    // }
    // if (this.addNew) {
    //   this.setState({
    //     newPassword: "",
    //     confirmPassword: "",
    //     userName: "",
    //   });
    // }
    // this.addNew = false;
  }

  toggleTab = (tab) => {
    if (this.state.activeTab !== tab) {
      this.setState({ activeTab: tab });
    }
  };

  // toggleModal = () => {
  //   this.setState((prevState) => ({
  //     modal: !prevState.modal,
  //   }));
  // };

  doSubmit = async (e) => {
    e.preventDefault();

    this.setState({ Loading: true });

    const errorMessage = "";
    const errorscustom = [];
    this.setState({ errorMessage, errorscustom });
    try {
      let obj = this.populatinginData();
      console.log(obj);
      //Add
      if (this.props.addNew) {
        const { data } = await agent.Questions.create(obj);
        if (data.result.status) {
          obj.id = data.result.data.id;
          obj.serviceName =
            data.result.data.serviceName +
            `(${data.result.data.persianServiceName})`;

          this.props.addToQuestions(obj);
          toast.success(data.result.message);
          this.cleanData();
        }
      }

      //Edit
      else {
        const { data } = await agent.Questions.update(obj);
        if (data.result.status) {
          obj.id = data.result.data.id;
          obj.serviceName =
            data.result.data.serviceName +
            `(${data.result.data.persianServiceName})`;

          this.props.editToQuestions(obj);
          toast.success(data.result.message);
        }
      }

      this.props.toggleModal();
      this.props.handleSidebar(false, true, false);
    } catch (ex) {
      console.log(ex);
      if (ex?.response?.status == 400) {
        const errorscustom = ex?.response?.data?.errors;
        this.setState({ errorscustom });
      } else if (ex?.response) {
        const errorMessage = ex?.response?.data?.message;
        console.log(errorMessage);
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

  populatinginData = () => {
    const {
      id,
      text,
      englishText,
      isEnabled,

      answer1,
      answerEnglish1,
      isEnabled1,
      key1,

      answer2,
      answerEnglish2,
      isEnabled2,
      key2,

      answer3,
      answerEnglish3,
      isEnabled3,
      key3,

      serviceId,
    } = this.state;

    return {
      id,
      text,
      englishText,
      isEnabled,

      answer1,
      answerEnglish1,
      isEnabled1,
      key1,

      answer2,
      answerEnglish2,
      isEnabled2,
      key2,

      answer3,
      answerEnglish3,
      isEnabled3,
      key3,

      serviceId,
    };
  };

  render() {
    const { errorMessage, errors, errorscustom } = this.state;
    return (
      <React.Fragment>
        {/* <Button.Ripple color="primary" onClick={this.toggleModal}>
                  Create New Category
                </Button.Ripple> */}

        {/* <Button
          className="add-new-btn mb-2"
          color="primary"
          onClick={this.props.toggleModal}
          outline
        >
          <Plus size={15} />
          <span className="align-middle">Add New</span>
        </Button> */}
        <Modal
          isOpen={this.props.modal}
          toggle={this.props.toggleModal}
          className="modal-dialog-centered modal-lg"
        >
          <ModalHeader toggle={this.props.toggleModal}>
            Question Form
          </ModalHeader>
          <ModalBody>
            {/* 
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
              })} */}

            <Form className="mt-3" onSubmit={this.doSubmit}>
              {errorscustom &&
                errorscustom.map((err, index) => {
                  return (
                    <Alert key={index} className="text-center" color="danger ">
                      {err}
                    </Alert>
                  );
                })}

              <FormGroup row>
                <Col md="6">
                  <h5>question (english text):</h5>
                  <Input
                    type="text"
                    id="english text"
                    value={this.state.englishText}
                    onChange={(e) => {
                      this.setState({ englishText: e.target.value });
                    }}
                    placeholder="question (english Text)"
                    required
                    minLength={1}
                    maxLength={100}
                  />
                </Col>

                <Col md="6">
                  <h5>question (persian text):</h5>
                  <Input
                    className="text-right"
                    type="text"
                    id="persian text"
                    value={this.state.text}
                    placeholder="question (persian text)"
                    onChange={(e) => {
                      this.setState({ text: e.target.value });
                    }}
                    required
                    minLength={1}
                    maxLength={100}
                  />
                </Col>

                {/* <Col md="6">
                  <Checkbox
                    color="primary"
                    icon={<Check className="vx-icon" size={16} />}
                    label="Is it enable question?"
                    defaultChecked={this.state.isEnabled}
                    onChange={() => {
                      this.setState({
                        isEnabled: !this.state.isEnabled
                      })
                    }}
                  />
                </Col> */}
              </FormGroup>

              <FormGroup row>
                <Col md="6">
                  <h5>answer1 (english text):</h5>
                  <Input
                    type="text"
                    id="english text"
                    value={this.state.answerEnglish1}
                    onChange={(e) => {
                      this.setState({ answerEnglish1: e.target.value });
                    }}
                    placeholder="answer1 (english Text)"
                    required
                    minLength="1"
                    maxLength="100"
                  />
                </Col>

                <Col md="6">
                  <h5>answer1 (persian text):</h5>
                  <Input
                    className="text-right"
                    type="text"
                    id="persian text"
                    value={this.state.answer1}
                    placeholder="answer1 (persian text)"
                    onChange={(e) => {
                      this.setState({ answer1: e.target.value });
                    }}
                    required
                    minLength={1}
                    maxLength={100}
                  />
                </Col>

                {/* <Col md="6">
                  <Checkbox
                    color="primary"
                    icon={<Check className="vx-icon" size={16} />}
                    label="Is it enable answer1?"
                    defaultChecked={this.state.isEnabled1}
                    onChange={() => {
                      this.setState({
                        isEnabled1: !this.state.isEnabled1
                      })
                    }}
                  />
                </Col> */}
              </FormGroup>

              <FormGroup row>
                <Col md="6">
                  <h5>answer2 (english text):</h5>
                  <Input
                    type="text"
                    id="english text"
                    value={this.state.answerEnglish2}
                    onChange={(e) => {
                      this.setState({ answerEnglish2: e.target.value });
                    }}
                    placeholder="answer2 (english Text)"
                    required
                    minLength="1"
                    maxLength="100"
                  />
                </Col>

                <Col md="6">
                  <h5>answer2 (persian text):</h5>
                  <Input
                    className="text-right"
                    type="text"
                    id="persian text"
                    value={this.state.answer2}
                    placeholder="answer2 (persian text)"
                    onChange={(e) => {
                      this.setState({ answer2: e.target.value });
                    }}
                    required
                    minLength={1}
                    maxLength={100}
                  />
                </Col>

                {/* <Col md="6">
                  <Checkbox
                    color="primary"
                    icon={<Check className="vx-icon" size={16} />}
                    label="Is it enable answer2?"
                    defaultChecked={this.state.isEnabled2}
                    onChange={() => {
                      this.setState({
                        isEnabled2: !this.state.isEnabled2
                      })
                    }}
                  />
                </Col> */}
              </FormGroup>

              <FormGroup row>
                <Col md="6">
                  <h5>answer3 (english text):</h5>
                  <Input
                    type="text"
                    id="english text"
                    value={this.state.answerEnglish3}
                    onChange={(e) => {
                      this.setState({ answerEnglish3: e.target.value });
                    }}
                    placeholder="answer3 (english Text)"
                    required
                    minLength="1"
                    maxLength="100"
                  />
                </Col>

                <Col md="6">
                  <h5>answer3 (persian text):</h5>
                  <Input
                    className="text-right"
                    type="text"
                    id="persian text"
                    value={this.state.answer3}
                    placeholder="answer3 (persian text)"
                    onChange={(e) => {
                      this.setState({ answer3: e.target.value });
                    }}
                    required
                    minLength={1}
                    maxLength={100}
                  />
                </Col>

                {/* <Col md="6">
                  <Checkbox
                    color="primary"
                    icon={<Check className="vx-icon" size={16} />}
                    label="Is it enable answer3?"
                    defaultChecked={this.state.isEnabled3}
                    onChange={() => {
                      this.setState({
                        isEnabled3: !this.state.isEnabled3
                      })
                    }}
                  />
                </Col> */}
              </FormGroup>

              {this.props.addNew ? (
                <FormGroup>
                  <Col md="6">
                    <h5>Service:</h5>

                    <Select
                      className="React"
                      classNamePrefix="select"
                      // defaultValue={options[1]}
                      // value={{
                      //   value: 1,
                      //   label: "assasasa",
                      // }}
                      // value={this.props.services?.filter((option) => option.id === this.state.serviceId)}
                      name="servicess"
                      defaultValue={{ value: "", label: "" }}
                      //   value={value}
                      // isClearable={true}

                      // options={this.props.services}
                      options={this.props.services?.map((item) => {
                        return {
                          value: item.id,
                          label: item.name,
                        };
                      })}
                      onChange={(e) => {
                        this.setState({ serviceId: e.value });
                      }}
                    />
                  </Col>
                </FormGroup>
              ) : null}

              {this.state.Loading ? (
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
export default FormModal;
