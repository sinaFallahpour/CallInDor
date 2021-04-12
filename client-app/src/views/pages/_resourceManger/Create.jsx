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
  Col,
} from "reactstrap";

import { PlusSquare } from "react-feather";

// import { modalForm } from "./ModalSourceCode"
import { toast } from "react-toastify";
import ReactTagInput from "@pathofdev/react-tag-input";
import "@pathofdev/react-tag-input/build/index.css";

import agent from "../../../core/services/agent";

import Joi from "joi-browser";
import Form from "../../../components/common/form";

class ModalForm extends Form {
  state = {
    // data: {
    dataAnodations: [],
    // },
    
    loadingData:false,
    Loading: false,
    
  };

  schema = {
  };

  async populatingResources() {
    const { data } = await agent.Resources.dataAnotationsList();
    let dataAnodations = data.result.data;
    console.log(dataAnodations)
    this.setState({ dataAnodations, Loading: false });
  }

  async componentDidMount() {
    this.populatingResources();
  }

  // updateRequiredFileChanged = async (index, e) => {
  //   this.setState({
  //     requiredFiles: this.state.requiredFiles.map((item) =>
  //       item?._id == index ? { ...item, fileName: e.target.value } : item
  //     ),
  //   });
  // };

  // updateRequiredFilePersianChanged = async (index, e) => {
  //   this.setState({
  //     requiredFiles: this.state.requiredFiles.map((item) =>
  //       item?._id == index ? { ...item, persianFileName: e.target.value } : item
  //     ),
  //   });
  // };

  // addRequredFile = () => {
  //   this.setState({
  //     requiredFiles: [
  //       ...this.state.requiredFiles,
  //       { persianFileName: "", fileName: "", _id: Math.random() * 1000 },
  //     ],
  //   });
  // };

  // removeRequredFile = (index) => {
  //   this.setState({
  //     requiredFiles: this.state.requiredFiles.filter(function (reqFile) {
  //       return reqFile._id !== index;
  //     }),
  //   });
  // };

  doSubmit = async (e) => {
    this.setState({ Loading: true });

    // const errorMessage = "";
    // const errorscustom = [];
    // this.setState({ errorMessage, errorscustom });


    try {
      const { dataAnodations } = this.state;


      var obj = {}
      dataAnodations.forEach(function (data) {
        Object.defineProperty(obj, data.name, {
          value: data.value
        })
      });

      console.log(obj)
      // const obj = {
      //   ...this.state.data,
      // };

      await agent.Resources.dataAnotationsList

      const { data } = await agent.Resources.editDataAnotationAndErrorMessages(obj) ;
      if (data.result.status) toast.success(data.result.message);
    } catch (ex) {

    }
    this.setState({ Loading: false });
  };

  render() {
    const { dataAnodations, loadingData } = this.state;
    if (loadingData) {
      return <> loading...</>
    }


    return (
      <React.Fragment>
        <Col sm="10" className="mx-auto">
          <Card>
            <CardHeader>
              <CardTitle> Edit data Anotation </CardTitle>
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
                        <input name={item.name} id={item.name} value={item.value} onChange={(e)=>{ }} required className={`form-control`} />
                      </Col>
                    )
                  })}
                </FormGroup>

                {/* 

                {this.renderReactSelect(
                  "roleId",
                  "Role",
                  roles.map((item) => ({
                    value: item.id,
                    label: item.name,
                  }))
                )} */}

                {/* {this.renderInput("minSessionTime", "Min Session Time (For Chat, Chat Voice,...)")} */}

                {/* <div className="form-group">
                  <label>Tags</label>
                  <ReactTagInput
                    tags={this.state.tags}
                    placeholder="Type and press enter"
                    maxTags={60}
                    editable={true}
                    readOnly={false}
                    removeOnBackspace={true}
                    onChange={(newTags) => this.setState({ tags: newTags })}
                    validator={(value) => {
                      let isvalid = !!value.trim();
                      if (!isvalid) {
                        alert("tag cant be empty");
                        return isvalid;
                      }
                      isvalid = value.length < 100;
                      if (!isvalid) {
                        alert("please enter less than 100 character");
                      }
                      // Return boolean to indicate validity
                      return isvalid;
                    }}
                  />
                </div> */}

                {/* <div className="form-group">
                  <label>Persian Tags</label>
                  <ReactTagInput
                    tags={this.state.persinaTags}
                    placeholder="Type and press enter"
                    maxTags={60}
                    editable={true}
                    readOnly={false}
                    removeOnBackspace={true}
                    onChange={(newTags) =>
                      this.setState({ persinaTags: newTags })
                    }
                    validator={(value) => {
                      let isvalid = !!value.trim();
                      if (!isvalid) {
                        alert("tag cant be empty");
                        return isvalid;
                      }
                      isvalid = value.length < 100;
                      if (!isvalid) {
                        alert("please enter less than 100 character");
                      }
                      // Return boolean to indicate validity
                      return isvalid;
                    }}
                  />
                </div> */}

                {/* <div className="form-group">
                  <label htmlFor="isEnabled">Is Enabled</label>
                  <input
                    value={this.state.isEnabled}
                    checked={this.state.isEnabled}
                    onChange={(e) => {
                      this.setState({ isEnabled: !this.state.isEnabled });
                    }}
                    name="isEnabled"
                    id="isEnabled"
                    type="checkbox"
                    className="ml-1"
                  />
                </div> */}



                {/* <div className="row">
                  <input type="hidden" value="1" />
                  <div className="form-group col-12 col-md-4">
                    <label htmlFor="sa">File name (persian)</label>
                    <input name="sa" id="sa" className={`form-control `} />
                    {error && (
                      <div className="  alert alert-danger">{error}</div>
                    )}
                  </div>

                  <div className="form-group col-12 col-md-4">
                    <label htmlFor="sa">File name (english)</label>
                    <input name="sa" id="sa" className={`form-control `} />
                    {error && (
                      <div className="  alert alert-danger">{error}</div>
                    )}
                  </div>

                  <div className="form-group col-4  col-md-3">
                    <label></label>
                    <Button type="button" className="form-control btn-danger">
                      remove
                    </Button>
                  </div>
                </div> */}


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
