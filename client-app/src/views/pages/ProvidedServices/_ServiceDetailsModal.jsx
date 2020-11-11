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

import ReactTagInput from "@pathofdev/react-tag-input";
import "@pathofdev/react-tag-input/build/index.css";

// import { modalForm } from "./ModalSourceCode"
import Checkbox from "../../../components/@vuexy/checkbox/CheckboxesVuexy";

import { toast } from "react-toastify";
import agent from "../../../core/services/agent";

class ServiceDetailsModal extends React.Component {
  state = {
    id: null,
    serviceName: "",
    description: "",
    beTranslate: false,
    fileNeeded: false,
    fileDescription: "",
    price: null,
    workDeliveryTimeEstimation: "",
    howWorkConducts: "",
    deliveryItems: "",
    tags: null,  //=====================================================
    areaTitle: "",
    specialityTitle: "",
    categoryTitile: "",
    subcategoryTitile: "",
    serviceType: null,
    userName: "",
    confirmedServiceType: null,
    createDate: null,
    //c.IsDeleted,
    isEditableService: false,
    rejectReason: "",



    // categories: [],
    // services: [],

    errors: [],
    errorMessage: "",
    Loading: false,

    activeTab: "1",
    serviceModal: false,
  };

  async componentDidMount() {

  }


  componentDidUpdate(prevProps, prevState) {
    if (this.props.currenServiceServiceData !== null) {
      if (this.props.currenServiceServiceData.id != prevState.id) {
        const {
          id,
          serviceName,
          description,
          beTranslate,
          fileNeeded,
          fileDescription,
          price,
          workDeliveryTimeEstimation,
          howWorkConducts,
          deliveryItems,
          tags,//=====================================================
          areaTitle,
          specialityTitle,
          categoryTitile,
          subcategoryTitile,
          serviceType,
          userName,
          confirmedServiceType,
          createDate,
          //c.IsDeleted,
          isEditableService,
          rejectReason,

        } = this.props.currenServiceServiceData

        this.setState({
          id,
          serviceName,
          description,
          beTranslate,
          fileNeeded,
          fileDescription,
          price,
          workDeliveryTimeEstimation,
          howWorkConducts,
          deliveryItems,
          tags,//=====================================================
          areaTitle,
          specialityTitle,
          categoryTitile,
          subcategoryTitile,
          serviceType,
          userName,
          confirmedServiceType,
          createDate,
          //c.IsDeleted,
          isEditableService,
          rejectReason,
          serviceModal: this.props.serviceModal
        });
      }
    }
    if (this.props.currenServiceServiceData === null && prevProps.currenServiceServiceData !== null) {

      this.setState({
        id: null,
        serviceName: "",
        description: "",
        beTranslate: false,
        fileNeeded: false,
        fileDescription: "",
        price: null,
        workDeliveryTimeEstimation: "",
        howWorkConducts: "",
        deliveryItems: "",
        tags: null,//=====================================================
        areaTitle: "",
        specialityTitle: "",
        categoryTitile: "",
        subcategoryTitile: "",
        serviceType: null,
        userName: "",
        confirmedServiceType: null,
        createDate: null,
        //c.IsDeleted,
        isEditableService: false,
        rejectReason: "",
        serviceModal: false
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
        <Modal
          isOpen={this.props.serviceModal}
          toggle={this.props.toggleServiceModal}
          className="modal-dialog-centered modal-lg"
        >
          <ModalHeader toggle={this.props.toggleServiceModal}>
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
                <InputItem id="userName" label="User Name:" value={this.state.userName} />
                <InputItem id="serviceName" label="Service Name:" value={this.state.serviceName} />
              </FormGroup>

              <FormGroup row>
                <InputItem id="areaTitle" label="Area Title:" value={this.state.areaTitle} />
                <InputItem id="specialityTitle" label="Speciality Title:" value={this.state.specialityTitle} />
              </FormGroup>

              <FormGroup row>
                <InputItem id="Category Title" label="Category Title :" value={this.state.categoryTitile} />
                <InputItem id="subcategoryTitile" label="Subcategory Title:" value={this.state.subcategoryTitile} />
              </FormGroup>

              <FormGroup row>
                <InputItem id="confirmedServiceType" label="Confirmed service status :" value={this.returnConfirmService(this.state.confirmedServiceType)} />
                <InputItem id="price" label="price:" value={this.state.price} />
              </FormGroup>


              <FormGroup row>
                <InputItem id="Service Type" label="Service Type:" value={this.returnServiceType(this.state.serviceType)} />
                <InputItem id="rejectReason" label=" Reason for reject:" value={this.state.rejectReason} />
              </FormGroup>

              <FormGroup row>
                <InputItem id="Description" type="textarea" label="Description:" value={this.state.description} />
                <InputItem id="Description" type="textarea" label="File Description:" value={this.state.fileDescription} />

              </FormGroup>


              <FormGroup row>

                <Col md="6">
                  <h5 for="workDeliveryTimeEstimation"> work Delivery Time Estimation:</h5>
                  <Input
                    type="textarea"
                    id="workDeliveryTimeEstimation"
                    value={this.state.workDeliveryTimeEstimation}
                    readOnly

                  />
                </Col>


                <Col md="6">
                  <h5 for="howWorkConducts"> how Work Conducts:</h5>
                  <Input
                    type="textarea"
                    id="howWorkConducts"
                    value={this.state.howWorkConducts}
                    readOnly

                  />
                </Col>

              </FormGroup>



              <FormGroup row>

                <Col md="6">
                  <h5 for="deliveryItems"> delivery Items:</h5>
                  <Input
                    type="textarea"
                    id="deliveryItems"
                    value={this.state.deliveryItems}
                    readOnly
                  />
                </Col>


                <Col md="6">
                  <h5 for="howWorkConducts"> how Work Conducts:</h5>
                  <Input
                    type="textarea"
                    id="howWorkConducts"
                    value={this.state.howWorkConducts}
                    readOnly

                  />
                </Col>

              </FormGroup>





              <FormGroup row>
                <Col md="12">
                  <h5 for="tags"> Tags:</h5>
                  <ReactTagInput
                    tags={this.state?.tags?.split(",")}
                    editable={false}
                    readOnly={true}
                    removeOnBackspace={true}
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
                    label="file Needed?"
                    defaultChecked={this.state.fileNeeded}
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
export default ServiceDetailsModal;







const InputItem = ({ label, id, value, type = "text" }) => {

  return (
    <Col md="6">
      <h5 for={id}>{label}</h5>
      <Input
        type={type}
        id={id}
        value={value}
        readOnly
      />
    </Col>
  )
}
