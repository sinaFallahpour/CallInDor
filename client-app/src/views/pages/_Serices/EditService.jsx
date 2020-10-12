import React from "react";
import {
  Card,
  CardHeader,
  CardTitle,
  CardBody,
  FormGroup,
  Button,
  Label,
  Alert,
  Col,
  Spinner as SpinnerButtton
} from "reactstrap";

import Spinner from "../../../components/@vuexy/spinner/Loading-spinner";

import ReactTagInput from "@pathofdev/react-tag-input";
import "@pathofdev/react-tag-input/build/index.css";
import { toast } from "react-toastify";





import Joi, { validate } from "joi-browser";
import Form from "../../../components/common/form";





// import LoadingOverlay from 'react-loading-overlay';
// import 'react-loading-overlay/lib/styles'



// import { Formik, Field, Form  } from "formik";
// import * as Yup from "yup";

import agent from "../../../core/services/agent";

import 'react-block-ui/dist/style.css';
import BlockUi from 'react-block-ui';

import ReactLoading from 'react-loading';

class EditService extends Form {

  state = {
    data: {
      id: null,
      name: "",
      persianName: "",
      color: "",
      minPriceForService: "",
      minSessionTime: "",
      acceptedMinPriceForNative: "",
      acceptedMinPriceForNonNative: ""
    },
    tags: [],
    persinaTags: [],
    isEnabled: true,
    errors: {},
    errorscustom: [],
    errorMessage: "",
    Loading: false,
    pageLoading: false
  };

  schema = {
    id: Joi.number(),
    name: Joi.string(),
    persianName: Joi.string()
      .required()
      .label("Persian Name"),

    color: Joi.string()
      .required()
      .label("color"),

    minPriceForService: Joi.number()
      .required()
      .min(1)
      .max(10000000000000)
      .label("minimum Price"),

    minSessionTime: Joi.number()
      .required()
      .min(1)
      .max(10000000)
      .label("minimum Session Time"),

    acceptedMinPriceForNative: Joi.number()
      .required()
      .min(1)
      .max(10000000000000)
      .label("minimum Price For Native User"),

    acceptedMinPriceForNonNative: Joi.number()
      .required()
      .min(1)
      .max(10000000000000)
      .label("minimum Price For Non Native User"),

  };


  async populatinService() {
    this.setState({ pageLoading: true })
    const serviceId = this.props.match.params.id;
    try {
      const { data } = await agent.ServiceTypes.details(serviceId);
      let { id, name, persianName, color, isEnabled, tags, persinaTags, minPriceForService, minSessionTime, acceptedMinPriceForNative, acceptedMinPriceForNonNative } = data.result.data;
      this.setState({ data: { id, name, persianName, color, minPriceForService, minSessionTime, acceptedMinPriceForNative, acceptedMinPriceForNonNative }, isEnabled, tags, persinaTags });
    }
    catch (ex) {
      console.log(ex);
      if (ex?.response?.status == 404 || ex?.response?.status == 400) {
        return this.props.history.replace("/not-found");
      }
    }
    this.setState({ pageLoading: false })
  }


  async componentDidMount() {

    this.populatinService();
  }


  doSubmit = async () => {
    this.setState({ Loading: true });
    const errorMessage = "";
    const errorscustom = [];
    this.setState({ errorMessage, errorscustom });
    try {
      const { isEnabled, tags, persinaTags } = this.state
      const obj = {
        ...this.state.data,
        isEnabled,
        tags: tags?.length == 0 ? null : tags.join(),
        persinaTags: persinaTags?.length == 0 ? null : persinaTags.join(),
      };
      const { data } = await agent.ServiceTypes.update(obj);
      if (data.result.status)
        toast.success(data.result.message)
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
    }, 800);
  };





  render() {
    const { errorscustom, errorMessage, pageLoading } = this.state


    if (pageLoading) {
      return (
        <Spinner />
      )
    }
    return (
      <Col sm="10" className="mx-auto">
        <Card >
          <CardHeader>
            <CardTitle> Edit Category </CardTitle>
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
              {this.renderInput("name", "Name")}
              {this.renderInput("persianName", "PersianName")}
              {this.renderInput("color", "Color")}
              {this.renderInput("minPriceForService", "Minimm Price (For Service)$")}

              {this.renderInput("acceptedMinPriceForNative", "Minimm Price For Native User (For Chat,Voice,video)$")}
              {this.renderInput("acceptedMinPriceForNonNative", "Minimm Price For Non Native User  (For Chat,Voice,video)$")}
              {this.renderInput("minSessionTime", "Min Session Time (minutes) (For Chat,Voice,video)$")}

              <div className="form-group">


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
                      return isvalid
                    }
                    isvalid = value.length < 100
                    if (!isvalid) {
                      alert("please enter less than 100 character")
                    }
                    // Return boolean to indicate validity
                    return isvalid;
                  }}
                />
              </div>


              <div className="form-group">


                <label>Persian Tags</label>
                <ReactTagInput
                  tags={this.state.persinaTags}
                  placeholder="Type and press enter"
                  maxTags={60}
                  editable={true}
                  readOnly={false}
                  removeOnBackspace={true}
                  onChange={(newTags) => this.setState({ persinaTags: newTags })}
                  validator={(value) => {
                    let isvalid = !!value.trim();
                    if (!isvalid) {
                      alert("tag cant be empty");
                      return isvalid
                    }
                    isvalid = value.length < 100
                    if (!isvalid) {
                      alert("please enter less than 100 character")
                    }
                    // Return boolean to indicate validity
                    return isvalid;
                  }}
                />
              </div>
              <div className="form-group">
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


              {this.state.Loading ?
                <Button disabled={true} color="primary" className="mb-1">
                  <SpinnerButtton color="white" size="sm" type="grow" />
                  <span className="ml-50">Loading...</span>
                </Button>
                :
                this.renderButton("Save")
              }

            </form>

          </CardBody>
        </Card>
      </Col>
    );
  }
}
export default EditService;
