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

import Select from "react-select";

import { toast } from "react-toastify";
import agent from "../../../core/services/agent";

// Id;
// PersianTitle;
// EnglishTitle;
// Code;
// Percent;
// DayCount;
// HourCount;

class FormModal extends React.Component {
  state = {
    id: null,
    persianTitle: "",
    englishTitle: "",
    code: "",
    percent: "",
    dayCount: "",
    hourCount: "",

    serviceId: null,
    services: [],

    errors: [],
    errorMessage: "",
    errorscustom: [],

    Loading: false,

    activeTab: "1",
    modal: false,
  };

  componentDidMount() { }

  componentDidUpdate(prevProps, prevState) {
    //این دیتا جدیده نسبت به قبل ایدی ها تغییر کرد
    if (this.props.currentData !== null) {
      if (this.props.currentData.id != prevState.id) {
        const {
          id,
          persianTitle,
          englishTitle,
          code,
          percent,
          dayCount,
          hourCount,

          serviceId,
          serviceName,
        } = this.props.currentData;

        this.setState({
          id,
          persianTitle,
          englishTitle,
          code,
          percent,
          dayCount,
          hourCount,

          serviceId,
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
        persianTitle: "",
        englishTitle: "",
        code: "",
        percent: "",
        dayCount: "",
        hourCount: "",

        serviceId: "",
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
        const { data } = await agent.CheckDiscount.create(obj);
        if (data.result.status) {
          obj.id = data.result.data.id;
          obj.serviceName =
            data.result.data.serviceName +
            `(${data.result.data.persianServiceName})`;
          obj.expireTime = data.result.data.expiretime

          this.props.addToDisCount(obj);
          toast.success(data.result.message);
          this.cleanData();
        }
      }

      //Edit
      else {
        const { data } = await agent.CheckDiscount.update(obj);
        if (data.result.status) {
          // obj.id = data.result.data.id;
          obj.serviceName =
            data.result.data.serviceName +
            `(${data.result.data.persianServiceName})`;
          obj.expireTime = data.result.data.expiretime

          this.props.editToDisCount(obj);

          toast.success(data.result.message);
          this.cleanData();
        }
      }

      this.props.toggleModal();
      this.props.handleSidebar(false, true, false);
    } catch (ex) {

      // console.log(ex);
      // if (ex?.response?.status == 400) {
      //   const errorscustom = ex?.response?.data?.errors;
      //   this.setState({ errorscustom });
      // } else if (ex?.response) {

      //   const errorMessage = ex?.response?.data?.message;
      //   console.log(errorMessage);
      //   this.setState({ errorMessage });
      //   toast.error(errorMessage, {
      //     autoClose: 10000,
      //   });
      // }
    } finally {
      setTimeout(() => {
        this.setState({ Loading: false });
      }, 200);
    }

  };

  populatinginData = () => {
    const {
      id,
      persianTitle,
      englishTitle,
      code,
      percent,
      dayCount,
      hourCount,

      serviceId,
    } = this.state;

    return {
      id,
      persianTitle,
      englishTitle,
      code,
      percent,
      dayCount,
      hourCount,
      serviceId,
    };
  };




  cleanData = () => {

    this.setState({
      persianTitle: "",
      englishTitle: "",
      code: "",
      percent: "",
      dayCount: "",
      hourCount: "",
      serviceId: null,

      // services: [],
    });
  };


  render() {
    const { errorMessage, errors, errorscustom } = this.state;

    console.log(this.props?.currentData?.serviceId, "aasasa");
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
            Discount Form
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
                  <h5>discount (english title):</h5>
                  <Input
                    type="text"
                    id="english title"
                    value={this.state.englishTitle}
                    onChange={(e) => {
                      this.setState({ englishTitle: e.target.value });
                    }}
                    placeholder="discount (english title)"
                    required
                    minLength={1}
                    maxLength={100}
                  />
                </Col>

                <Col md="6">
                  <h5>discount (persian title):</h5>
                  <Input
                    className="text-right"
                    type="text"
                    id="persian title"
                    value={this.state.persianTitle}
                    placeholder="discount (persian title)"
                    onChange={(e) => {
                      this.setState({ persianTitle: e.target.value });
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
                  <h5>code :</h5>
                  <Input
                    type="text"
                    id="code "
                    value={this.state.code}
                    onChange={(e) => {
                      this.setState({ code: e.target.value });
                    }}
                    placeholder="code"
                    required
                    minLength="5"
                    maxLength="100"
                  />
                </Col>

                <Col md="6">
                  <h5>percent :</h5>
                  <input
                    className=" form-control"
                    type="number"
                    id="percentt"
                    value={this.state.percent}
                    placeholder="percent"
                    onChange={(e) => {
                      this.setState({ percent: e.target.value });
                    }}
                    min={0}
                    max={100}
                    required
                  />
                </Col>
              </FormGroup>

              <FormGroup row>
                <Col md="6">
                  <h5>day Count:</h5>
                  <input
                    className="form-control"
                    type="number"
                    id="dayCount"
                    value={this.state.dayCount}
                    onChange={(e) => {
                      this.setState({ dayCount: e.target.value });
                    }}
                    placeholder="example 2 day"
                    min={1}
                    max={365}
                  // required
                  // minLength="1"
                  // maxLength="100"
                  />
                </Col>

                <Col md="6">
                  <h5>hour Count:</h5>
                  <Input
                    className=""
                    type="number"
                    id="hourCounr"
                    value={this.state.hourCount}
                    placeholder="example 4 hour"
                    onChange={(e) => {
                      this.setState({ hourCount: e.target.value });
                    }}
                    min={1}
                    max={24}
                  // required
                  // minLength={1}
                  // maxLength={100}
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

                    // value={this.props?.services.filter((option) => option.id === this.props?.currentData?.serviceId)}
                    // value={this.props.services?.filter((option) => option.id === this.state.serviceId)}
                    name="serviceId"

                    // defaultValue={{ value: "", label: "" }}


                    value={this.props.services?.map((item) => {
                      if (item.id === this.state?.serviceId) {
                        return {
                          value: item.id,
                          label: item.name,
                        };
                      }
                    })}
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
