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
  Col,
} from "reactstrap";
import classnames from "classnames";
import { Eye, Code, Plus, Check } from "react-feather";


// import { modalForm } from "./ModalSourceCode"
import Checkbox from "../../../components/@vuexy/checkbox/CheckboxesVuexy";

import { toast } from "react-toastify";
import agent from "../../../core/services/agent";

class ChatDetailsModal extends React.Component {
  state = {

    id: null,
    packageType: null,
    beTranslate: false,
    freeMessageCount: null,
    isServiceReverse: false,
    priceForNativeCustomer: null,
    priceForNonNativeCustomer: null,
    rejectReason: "",
    createDate: null,
    serviceName: '',
    serviceType: null,
    catId: null,
    subCatId: null,
    userName: "",
    confirmedServiceType: null,
    isDeleted: false,
    isEditableService: false,
    // roleId: null,



    // categories: [],
    // services: [],

    errors: [],
    errorMessage: "",
    Loading: false,

    activeTab: "1",
    modal: false,
  };


  componentDidMount() {

  }


  componentDidUpdate(prevProps, prevState) {
    if (this.props.currenChatServiceData !== null) {
      if (this.props.currenChatServiceData.id != prevState.id) {
        const {
          id,
          packageType,
          beTranslate,
          freeMessageCount,
          isServiceReverse,
          priceForNativeCustomer,
          priceForNonNativeCustomer,
          rejectReason,
          createDate,
          serviceName,
          serviceType,
          catId,
          subCatId,
          userName,
          confirmedServiceType,
          IsDeleted,
          isEditableService,
          roleId,
        } = this.props.currenChatServiceData


        this.setState({
          id,
          packageType,
          beTranslate,
          freeMessageCount,
          isServiceReverse,
          priceForNativeCustomer,
          priceForNonNativeCustomer,
          rejectReason,
          createDate,
          serviceName,
          serviceType,
          catId,
          subCatId,
          userName,
          confirmedServiceType,
          IsDeleted,
          isEditableService,
          roleId,
          modal: this.props.modal

        });
      }
    }
    if (this.props.currenChatServiceData === null && prevProps.currenChatServiceData !== null) {
      this.setState({
        id: null,
        packageType: null,
        beTranslate: false,
        freeMessageCount: null,
        isServiceReverse: false,
        priceForNativeCustomer: null,
        priceForNonNativeCustomer: null,
        rejectReason: "",
        createDate: null,

        serviceName: '',
        serviceType: null,
        catId: null,
        subCatId: null,
        userName: "",
        confirmedServiceType: null,
        IsDeleted: false,
        isEditableService: false,
        roleId: null,
        modal: false
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

  returnConfirmService = (confirmType) => {
    if (confirmType == 0) {
      return "ACCEPTED"
    }
    if (confirmType == 1) {
      return "REJECTED "
    }
    if (confirmType == 2) {
      return "PENDING"
    }
  }



  returnServiceType = (serviceType) => {
    if (serviceType == 0)
      return "Chat-Voice"
    if (serviceType == 1)
      return "Video-Call "
    if (serviceType == 2)
      return "Voice-Call"
    if (serviceType == 3)
      return "Service"
    if (serviceType == 4)
      return "Course"

  }




  returnPackageType = (packageType) => {
    if (packageType == 0) {
      return "FREE"
    }
    if (packageType == 1) {
      return "PRIODED"
    }
    if (packageType == 2) {
      return "SESSION"
    }
  }

  render() {
    const { errorMessage, errors } = this.state;
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
            Service Details
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



            {/* id: null,
        packageType: null, =====================
        beTranslate: false,     
        freeMessageCount: null, ============================
        isServiceReverse: false,    ==========
        priceForNativeCustomer: null, ============================
        priceForNonNativeCustomer: null, ============================
        rejectReason: "",         ===================
        createDate: null,      

        serviceName: '',   ============================
        serviceType: null, 
        catId: null,
        subCatId: null,
        userName: "",  ============================
        confirmedServiceType: null,
        IsDeleted: false,    ============================
        isEditableService: false,   ============================
      
        modal: false */}

            <Form action="/s" className="mt-3" >
              <FormGroup row>
                <Col md="6">
                  <h5 for="userName">userName:</h5>
                  <Input
                    type="text"
                    id="userName"
                    value={this.state.userName}
                    readOnly
                    // onChange={(e) => {
                    //   this.setState({ title: e.target.value });
                    // }}
                    placeholder="userName"
                    required
                    minLength="1"
                    maxLength="100"
                  />
                </Col>

                <Col md="6">
                  <h5 for="serviceName">serviceName:</h5>
                  <Input
                    type="text"
                    id="serviceName"
                    value={this.state.serviceName}
                    readOnly
                  // onChange={(e) => {
                  //   this.setState({ title: e.target.value });
                  // }}
                  />
                </Col>
              </FormGroup>

              <FormGroup row>
                <Col md="6">
                  <h5 for="free Message Count">free Message Count:</h5>
                  <Input
                    type="text"
                    id="free Message Count"
                    value={this.state.freeMessageCount}
                    readOnly
                  // onChange={(e) => {
                  //   this.setState({ title: e.target.value });
                  // }}

                  />
                </Col>
                <Col md="6">
                  <h5 for="confirmedServiceType">confirmed service status:</h5>
                  <Input
                    type="text"
                    id="confirmedServiceType"
                    value={this.returnConfirmService(this.state.confirmedServiceType)}
                    readOnly
                  // onChange={(e) => {
                  //   this.setState({ title: e.target.value });
                  // }}

                  />

                </Col>

              </FormGroup>


              <FormGroup row>
                <Col md="6">
                  <h5 for="email">price for native customer:</h5>
                  <Input
                    type="text"
                    id="title"
                    value={this.state.priceForNativeCustomer}
                    readOnly

                  />
                </Col>

                <Col md="6">
                  <h5 for="email">price for non native customer:</h5>
                  <Input
                    type="text"
                    id="title"
                    value={this.state.priceForNonNativeCustomer}
                    readOnly

                  />
                </Col>
              </FormGroup>



              <FormGroup row>
                <Col md="6">
                  <h5 for="package type">package type:</h5>
                  <Input
                    type="text"
                    id="package type"
                    value={this.returnPackageType(this.state.packageType)}
                    readOnly
                  />
                </Col>

                <Col md="6">
                  <h5 for="rejectReason"> reason for reject:</h5>
                  <Input
                    type="text"
                    id="rejectReason"
                    value={this.state.rejectReason}
                    readOnly

                  />
                </Col>
              </FormGroup>




              <FormGroup row>
                <Col md="6">
                  <h5 for="Service Type">Service Type:</h5>
                  <Input
                    type="text"
                    id="Service Type"
                    value={this.returnServiceType(this.state.serviceType)}
                    readOnly
                  />
                </Col>

              </FormGroup>

              <FormGroup>
                <Col md="6">
                  <Checkbox
                    disabled
                    color="primary"
                    icon={<Check className="vx-icon" size={16} />}
                    label="Ediable status for user?"
                    defaultChecked={this.state.isEditableService}
                  />
                </Col>

                <Col md="6">
                  <Checkbox
                    disabled
                    color="primary"
                    icon={<Check className="vx-icon" size={16} />}
                    label="Is it service reverse?"
                    defaultChecked={this.state.isServiceReverse}
                  />
                </Col>
              </FormGroup>

            </Form>
          </ModalBody>
        </Modal>


      </React.Fragment >
    );
  }
}
export default ChatDetailsModal;
