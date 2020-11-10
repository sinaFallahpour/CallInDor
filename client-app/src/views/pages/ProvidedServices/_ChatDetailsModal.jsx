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
import { Eye, Code, Plus } from "react-feather";
// import { modalForm } from "./ModalSourceCode"
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
    IsDeleted: false,
    isEditableService: false,
    roleId: null,



    // categories: [],
    // services: [],

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
      alert("exist")
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

        <Button
          className="add-new-btn mb-2"
          color="primary"
          onClick={this.props.toggleModal}
          outline
        >
          <Plus size={15} />
          <span className="align-middle">Add New</span>
        </Button>
        <Modal
          isOpen={this.props.modal}
          toggle={this.props.toggleModal}
          className="modal-dialog-centered modal-lg"
        >
          <ModalHeader toggle={this.props.toggleModal}>
            service Details
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
        isServiceReverse: false,
        priceForNativeCustomer: null, ============================
        priceForNonNativeCustomer: null, ============================
        rejectReason: "",
        createDate: null,

        serviceName: '',   ============================
        serviceType: null,
        catId: null,
        subCatId: null,
        userName: "",  ============================
        confirmedServiceType: null,
        IsDeleted: false,    ============================
        isEditableService: false,   ============================
        roleId: null,
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
                    placeholder="Title"
                    required
                    minLength="1"
                    maxLength="100"
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
                    placeholder="free Message Count"
                    required
                    minLength="1"
                    maxLength="100"
                  />
                </Col>
                <Col md="6">
                  <h5 for="confirmedServiceType">confirmedServiceType:</h5>
                  <Input
                    type="text"
                    id="confirmedServiceType"
                    value={this.returnConfirmService(this.state.confirmedServiceType)}
                    readOnly
                    // onChange={(e) => {
                    //   this.setState({ title: e.target.value });
                    // }}
                    placeholder="confirmedServiceType"
                    required
                    minLength="1"
                    maxLength="100"
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
                    // onChange={(e) => {
                    //   this.setState({ title: e.target.value });
                    // }}
                    placeholder="price For Native Customer"
                    required
                    minLength="1"
                    maxLength="100"
                  />
                </Col>

                <Col md="6">
                  <h5 for="email">price for non native customer:</h5>
                  <Input
                    type="text"
                    id="title"
                    value={this.state.priceForNonNativeCustomer}
                    readOnly
                    // onChange={(e) => {
                    //   this.setState({ title: e.target.value });
                    // }}
                    placeholder="price for non native customer"
                    required
                    minLength="1"
                    maxLength="100"
                  />
                </Col>
              </FormGroup>



              <FormGroup row>
                <Col md="6">
                  <h5 for="package type">package type:</h5>
                  <Input
                    type="text"
                    id="package type"
                    value={this.returnPackageType(this.state.PackageType)}
                    readOnly
                    // onChange={(e) => {
                    //   this.setState({ title: e.target.value });
                    // }}
                    placeholder="package type"
                    required
                    minLength="1"
                    maxLength="100"
                  />
                </Col>

                <Col md="6">
                  <h5 for="rejectReason"> reason for reject:</h5>
                  <Input
                    type="text"
                    id="rejectReason"
                    value={this.state.rejectReason}
                    readOnly
                    // onChange={(e) => {
                    //   this.setState({ title: e.target.value });
                    // }}
                    placeholder="rejectReason"
                    required
                    minLength="1"
                    maxLength="100"
                  />
                </Col>
              </FormGroup>








              {/*
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
              </FormGroup> */}

              <FormGroup className="ml-1 _customCheckbox">
                <Label check>
                  <Input
                    type="checkbox"
                    value={this.state.isEditableService}
                    // onChange={(e) =>
                    //   // alert(e.target.value)
                    //   this.setState({
                    //     isForCourse:
                    //       e.target.value == "on" ? true : false,
                    //   })
                    // }
                    name="isForCourse"
                    defaultChecked
                  />
                is it editable for user?
                          {/* {this.state.isEnabled ? "active" : "not active"} */}
                </Label>
              </FormGroup>

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
export default ChatDetailsModal;
