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

// import { modalForm } from "./ModalSourceCode"
import { toast } from "react-toastify";
import ReactTagInput from "@pathofdev/react-tag-input";
import "@pathofdev/react-tag-input/build/index.css";

import agent from "../../../core/services/agent";

import Joi from "joi-browser";
import Form from "../../../components/common/form";

class ModalForm extends Form {
  state = {
    data: {
      name: "",
      persianName: "",
      color: "",
      minPriceForService: "",
      minSessionTime: "",
      acceptedMinPriceForNative: "",
      acceptedMinPriceForNonNative: "",
      roleId: null,
    },
    tags: [],
    persinaTags: [],
    roles: [],

    isEnabled: true,
    errors: {},
    errorscustom: [],
    errorMessage: "",
    Loading: false,
  };

  schema = {
    name: Joi.string(),

    persianName: Joi.string().required().label("Persian Name"),

    color: Joi.string().required().label("color"),

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

    roleId: Joi.string().required().label("role"),
    roles: Joi.label("roles"),
  };

  async populatingRoles() {
    const { data } = await agent.Role.listActive();
    let roles = data.result.data;
    this.setState({ roles });
  }

  async componentDidMount() {
    this.populatingRoles();
  }

  doSubmit = async (e) => {
    this.setState({ Loading: true });

    const errorMessage = "";
    const errorscustom = [];
    this.setState({ errorMessage, errorscustom });
    try {
      const { isEnabled, tags, persinaTags } = this.state;
      const obj = {
        ...this.state.data,
        isEnabled,
        tags: tags.join(),
        persinaTags: persinaTags.join(),
      };

      const { data } = await agent.ServiceTypes.create(obj);
      if (data.result.status) toast.success(data.result.message);
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

  render() {
    const { errorscustom, errorMessage, roles } = this.state;
    return (
      <React.Fragment>
        <Col sm="10" className="mx-auto">
          <Card>
            <CardHeader>
              <CardTitle> Create Service </CardTitle>
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
                {this.renderInput("name", "Name")}
                {this.renderInput("persianName", "PersianName")}
                {this.renderInput("color", "Color")}
                {this.renderInput(
                  "minPriceForService",
                  "Minimm Price (For Service)$"
                )}

                {this.renderInput(
                  "acceptedMinPriceForNative",
                  "Minimm Price For Native User (For Chat,Voice,video)$"
                )}
                {this.renderInput(
                  "acceptedMinPriceForNonNative",
                  "Minimm Price For Non Native User  (For Chat,Voice,video)$"
                )}
                {this.renderInput(
                  "minSessionTime",
                  "Min Session Time (minutes) (For Chat,Voice,video)$"
                )}

                {this.renderReactSelect(
                  "roleId",
                  "Role",
                  roles.map((item) => ({
                    value: item.id,
                    label: item.name,
                  }))
                )}
                {/* {this.renderInput("minSessionTime", "Min Session Time (For Chat, Chat Voice,...)")} */}

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
                </div>

                <div className="form-group">
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
                </div>

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
