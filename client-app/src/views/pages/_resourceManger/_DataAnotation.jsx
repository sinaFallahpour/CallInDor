import React from "react";
import {
  Button,
  UncontrolledDropdown,
  DropdownMenu,
  DropdownToggle,
  DropdownItem,
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
  Col,
  UncontrolledButtonDropdown
} from "reactstrap";
import { history } from "../../../history";


import { PlusSquare, hevronDown, Plus, ChevronDown } from "react-feather";


// import CustomLoader from "./components/@vuexy/spinner/FullPageLoading";
import ComponentSpinner from "../../../components/@vuexy/spinner/Loading-spinner";


// import { modalForm } from "./ModalSourceCode"
import { toast } from "react-toastify";
import ReactTagInput from "@pathofdev/react-tag-input";
import "@pathofdev/react-tag-input/build/index.css";

import agent from "../../../core/services/agent";

import Joi from "joi-browser";
import Form from "../../../components/common/form";
import { Evented } from "leaflet";

class ModalForm extends Form {
  state = {
    // data: {
    dataAnodations: [],
    // },


    languageHeader: { header: "fa-IR", enumVal: 0, language: "persian" },// ,"en-US","ar",


    loadingData: false,
    Loading: false,

  };

  schema = {
  };

  async populatingResources() {
    await this.setState({ loadingData: true })
    console.log("-------" + this.state.languageHeader.enumVal)
    const { data } = await agent.Resources.dataAnotationsList(this.state.languageHeader.enumVal);
    let dataAnodations = data.result.data;
    this.setState({ dataAnodations, loadingData: false });
  }

  async componentDidMount() {
    this.populatingResources();
  }


  async handleLanguageHeader(languageHeader) {
    if (languageHeader == 0) {
      await this.setState({ languageHeader: { header: "fa-IR", enumVal: 0, language: "persian" } })
    }
    else if (languageHeader == 1) {
      await this.setState({ languageHeader: { header: "en-US", enumVal: 1, language: "english" } })
      // await this.setState({ languageHeader: { header: "en-US", language: "english" } })
    }
    else
      if (languageHeader == 2) {
        await this.setState({ languageHeader: { header: "ar", enumVal: 2, language: "arab" } })
      }
    this.populatingResources()
  }



  handleOnChange = (event, index) => {
    var dataAnotations = this.state.dataAnodations;
    var foundIndex = dataAnotations.findIndex(x => x.name == event.target.name);
    dataAnotations[foundIndex] = { name: event.target.name, value: event.target.value };
    this.setState({ dataAnotations })
  }


  doSubmit = async (e) => {
    this.setState({ Loading: true });

    // const errorMessage = "";
    // const errorscustom = [];
    // this.setState({ errorMessage, errorscustom });


    try {
      const { dataAnodations, languageHeader } = this.state;



      var rv = { languageHeader: languageHeader.enumVal };
      for (var i = 0; i < dataAnodations.length; ++i)
        rv[dataAnodations[i].name] = dataAnodations[i].value;


      // var dddddd = {}
      // dataAnodations.forEach(function (data) {
      //   Object.defineProperty(dddddd, data.name, {
      //     value: data.value
      //   })
      // });
      const { data } = await agent.Resources.editDataAnotationAndErrorMessages(rv, languageHeader.header);
      if (data.result.status) toast.success(data.result.message);
    } catch (ex) {

    }
    this.setState({ Loading: false });
  };

  render() {
    const { dataAnodations, loadingData } = this.state;
    if (loadingData) {
      return <> <ComponentSpinner /></>
    }


    return (
      <React.Fragment>
        <Col sm="10" className="mx-auto">

          <Button.Ripple
            onClick={() => { history.push("/pages/DataAnotation") }}
            className="mr-1 mb-1 bg-gradient-success" color="none">Error and warning Messages</Button.Ripple>

          <Button.Ripple

          
            onClick={() => { history.push("/pages/staticWord") }}
            className="mr-1 mb-1 bg-gradient-info" color="none">Site Static Word</Button.Ripple>

          {/* <Button.Ripple className="mr-1 mb-1 bg-gradient-warning" color="none">Warning</Button.Ripple> */}
          <Card>
            {/* <CardHeader>
              <CardTitle> Edit data Anotation </CardTitle>
            </CardHeader> */}

            <CardHeader>
              {/* <CardTitle> Edit data Anotation </CardTitle> */}
              <Button
                className="add-new-btn mb-2"
                color="primary"
                // onClick={this.toggleModal}
                // onClick={() => {
                //   this.handleSidebar(true, true);
                // }}
                // handleSidebar = (boolean, addNew = false) => {
                //   this.setState({ modal: boolean });
                //   if (addNew === true) this.setState({ currentData: null, addNew: true });
                // };

                outline
              >
                <Plus size={15} />
                <span className="align-middle">Add New</span>
              </Button>


              <h2> Edit data Anotation</h2>



              <div className="dropdown mr-1 mb-1">
                <UncontrolledButtonDropdown>
                  <DropdownToggle color="success" caret>
                    {this.state.languageHeader.language}
                    <ChevronDown size={15} />
                  </DropdownToggle>
                  <DropdownMenu>
                    <DropdownItem tag="a"
                      onClick={() => this.handleLanguageHeader(0)}
                    >
                      persian
                     </DropdownItem>
                    <DropdownItem tag="a"
                      onClick={() => this.handleLanguageHeader(1)}
                    >
                      english
                    </DropdownItem>
                    <DropdownItem tag="a"
                      onClick={() => this.handleLanguageHeader(2)}
                    >
                      arabic
                   </DropdownItem>
                  </DropdownMenu>
                </UncontrolledButtonDropdown>
              </div>

            </CardHeader>

            <CardBody>
              {/* {errorscustom &&
                errorscustom.map((err, index) => {
                  return (
                    <Alert key={index} className="text-center" color="danger ">
                      {err}
                    </Alert>
                  );
                })} */}

              <form onSubmit={this.handleSubmit}>
                <FormGroup row>

                  {dataAnodations.map((item, index) => {
                    return (
                      <Col md="4" className="p-1" key={index}>
                        <label htmlFor={item.name}>{item.name}</label>
                        <input
                          name={item.name}
                          id={item.name}
                          value={item.value}
                          onChange={(e) => {
                            this.handleOnChange(e, index)

                            // this.setState({ [item.name]: e.target.value })
                          }}
                          required className={`form-control`} />
                      </Col>
                    )
                  })}
                </FormGroup>




                {this.state.Loading ? (
                  <Button disabled={true} color="primary" className="mb-1">
                    <Spinner color="white" size="sm" type="grow" />
                    <span className="ml-50">Loading...</span>
                  </Button>
                ) : (
                  this.renderButton("Save")
                )}
              </form>
            </CardBody>
          </Card>
        </Col>
      </React.Fragment>
    );
  }
}
export default ModalForm;
