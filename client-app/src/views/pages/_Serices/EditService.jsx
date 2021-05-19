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
  Spinner as SpinnerButtton,
} from "reactstrap";

import Spinner from "../../../components/@vuexy/spinner/Loading-spinner";
import { PlusSquare } from "react-feather";

import ReactTagInput from "@pathofdev/react-tag-input";
import "@pathofdev/react-tag-input/build/index.css";
import { toast } from "react-toastify";

import Joi, { validate } from "joi-browser";
import Form from "../../../components/common/form";

// import LoadingOverlay from 'react-loading-overlay';
// import 'react-loading-overlay/lib/styles'

// import { Formik, Field, Form  } from "formik";
// import * as Yup from "yup";

import agent,{baseUrl} from "../../../core/services/agent";

import "react-block-ui/dist/style.css";
import BlockUi from "react-block-ui";

import ReactLoading from "react-loading";

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
      acceptedMinPriceForNonNative: "",



      sitePercent: "",
      topTenPackagePrice: "",
      usersCount: "",
      dayCount: "",
      hourCount: "",

      image: null,
      roleId: null,
    },
    imageAddress: "",

    roles: [],

    tags: [],
    persinaTags: [],
    requiredFiles: [],
    isEnabled: true,
    errors: {},
    errorscustom: [],
    errorMessage: "",
    Loading: false,
    pageLoading: false,
  };

  schema = {
    id: Joi.number(),
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



    sitePercent: Joi.number()
      .required()
      .min(0)
      .max(100)
      .label("minimum for site Percent"),


    topTenPackagePrice: Joi.number()
      .required()
      .min(0)
      .max(10000)
      .label("minimum for top Ten Package Price"),



    usersCount: Joi.number()
      .required()
      .min(0)
      .max(10000)
      .label("minimum for usersCount"),



    dayCount: Joi.number()
      .required()
      .min(0)
      .max(365)
      .label("minimum for count of day of witch a person can be top"),


    hourCount: Joi.number()
      .required()
      .min(0)
      .max(24)
      .label("minimum for count of day of witch a person can be top"),


    image: Joi.optional().label("image"),

    roleId: Joi.string().required().label("role"),

    roles: Joi.label("roles"),
  };

  async populatinService() {
    this.setState({ pageLoading: true });
    const serviceId = this.props.match.params.id;
    try {
      const { data } = await agent.ServiceTypes.details(serviceId);
      let {
        id,
        name,
        persianName,
        color,
        isEnabled,
        tags,
        persinaTags,
        minPriceForService,
        minSessionTime,
        acceptedMinPriceForNative,
        acceptedMinPriceForNonNative,
        imageAddress,
        sitePercent,
        topTenPackagePrice,
        usersCount,
        dayCount,
        hourCount,

        roleId,
        requiredCertificates,
      } = data.result.data;
      this.setState({
        data: {
          id,
          name,
          persianName,
          color,
          minPriceForService,
          minSessionTime,
          acceptedMinPriceForNative,
          acceptedMinPriceForNonNative,
          sitePercent,
          topTenPackagePrice,
          usersCount,
          dayCount,
          hourCount,

          roleId,
        },
        imageAddress,
        requiredFiles: requiredCertificates?.map((item) => {
          return { ...item, _id: Math.random() * 100 };
        }),
        isEnabled,
        tags,
        persinaTags,
      });
    } catch (ex) {
      console.log(ex);
      if (ex?.response?.status == 404 || ex?.response?.status == 400) {
        return this.props.history.replace("/not-found");
      }
    }
    this.setState({ pageLoading: false });
  }

  async populatingRoles() {
    const { data } = await agent.Role.listActive();
    let roles = data.result.data;
    this.setState({ roles });
  }

  async componentDidMount() {
    this.populatinService();
    this.populatingRoles();
  }

  updateRequiredFileChanged = async (index, e) => {
    this.setState({
      requiredFiles: this.state.requiredFiles.map((item) =>
        item?._id == index ? { ...item, fileName: e.target.value } : item
      ),
    });
  };

  updateRequiredFilePersianChanged = async (index, e) => {
    this.setState({
      requiredFiles: this.state.requiredFiles.map((item) =>
        item?._id == index ? { ...item, persianFileName: e.target.value } : item
      ),
    });
  };

  addRequredFile = () => {
    this.setState({
      requiredFiles: [
        ...this.state.requiredFiles,
        { persianFileName: "", fileName: "", _id: Math.random() * 1000 },
      ],
    });
  };

  removeRequredFile = (index) => {
    this.setState({
      requiredFiles: this.state.requiredFiles.filter(function (reqFile) {
        return reqFile._id !== index;
      }),
    });
  };

  doSubmit = async () => {
    this.setState({ Loading: true });
    const errorMessage = "";
    const errorscustom = [];
    this.setState({ errorMessage, errorscustom });
    try {
      const { isEnabled, tags, persinaTags, requiredFiles } = this.state;
      const obj = {
        ...this.state.data,
        isEnabled,
        tags: tags?.length == 0 ? null : tags.join(),
        persinaTags: persinaTags?.length == 0 ? null : persinaTags.join(),
        requiredFiles,
      };


      let form = new FormData();
      for (var key in obj) {
        form.append(key, obj[key]);
      }
      form.append("image", this.state.image)

      // data.append('RequiredFiles',
      //   '[{"id":-8792234,"fileName":"velit est in incididunt",
      //  "persianFileName": "velit in id consequat"}, { "id": -12505022, "fileName": "ex nulla", "persianFileName": "irure esse in anim cupidatat" }]');


      // form.delete('RequiredFiles');
      form.delete('requiredFiles');

      // form.append("RequiredFiles", obj.requiredFiles)
      var index = 0;
      for (var pair of obj.requiredFiles) {

        // form.append("RequiredFiles[" + index + "].id", pair?.id);
        // form.append("RequiredFiles[" + index + "].fileName", pair?.fileName);
        // form.append("RequiredFiles[" + index + "].persianFileName", pair?.persianFileName);

        form.append("RequiredFiles[" + index + "].id", pair?.id ? pair.id : "");
        form.append("RequiredFiles[" + index + "].fileName", pair?.fileName);
        form.append("RequiredFiles[" + index + "].persianFileName", pair?.persianFileName);


        index++;
      }

      const { data } = await agent.ServiceTypes.update(form);
      if (data.result.status) toast.success(data.result.message);

      this.populatinService()
    } catch (ex) {
      console.log(ex)

      // console.log(ex);
      // if (ex?.response?.status == 400) {
      //   const errorscustom = ex?.response?.data?.errors;
      //   this.setState({ errorscustom });
      // } else if (ex?.response) {
      //   const errorMessage = ex?.response?.data?.Message;
      //   this.setState({ errorMessage });
      //   toast.error(errorMessage, {
      //     autoClose: 10000,
      //   });
      // }
    }
    setTimeout(() => {
      this.setState({ Loading: false });
    }, 800);
  };

  render() {
    const { errorscustom, errorMessage, pageLoading, roles } = this.state;

    if (pageLoading) {
      return <Spinner />;
    }
    return (
      <Col sm="10" className="mx-auto">
        <Card>
          <CardHeader>
            <CardTitle> Edit Category </CardTitle>
          </CardHeader>


          <img src={baseUrl + this.state.imageAddress} className="d-block mx-auto " style={{ maxHeight: "300px" }} />

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
              {this.renderInput("sitePercent", "Site percent(%)", "number")}

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


              {this.renderInput(
                "topTenPackagePrice",
                "TopTenPackagePrice"
              )}

              {this.renderInput(
                "usersCount",
                "count of top  people can buy this package"
              )}

              {this.renderInput(
                "dayCount",
                "day Count eitch a person can be yop  count of top  people can buy this package"
              )}

              {this.renderInput(
                "hourCount",
                "time (HOUR) witch a person can be top"
              )}

              {this.renderInput(
                "image",
                "image",
                "file"
              )}






              {this.renderReactSelect(
                "roleId",
                "Role",
                roles.map((item) => ({
                  value: item.id,
                  label: item.name,
                }))
              )}

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
                  onChange={(e) =>
                    this.setState({ isEnabled: !this.state.isEnabled })
                  }
                  name="isEnabled"
                  id="isEnabled"
                  type="checkbox"
                  className="ml-1"
                />
              </div>

              <hr></hr>
              <h3>required file for user </h3>
              {this.state.requiredFiles?.map((requireFIle, index) => (
                <>
                  <div key={index} className="row">
                    <input type="hidden" value={requireFIle?._id} />
                    <div className="form-group col-12 col-md-4">
                      <label htmlFor={`PersianFileName${index}`}>
                        File name (persian)
                      </label>
                      <input
                        required
                        minLength={3}
                        maxLength={120}
                        onChange={(e) => {
                          this.updateRequiredFilePersianChanged(
                            requireFIle?._id,
                            e
                          );
                        }}
                        value={`${requireFIle?.persianFileName}`}
                        name={`PersianFileName${index}`}
                        id={`PersianFileName${index}`}
                        className={`form-control `}
                      />
                      {/* {error && <div className="  alert alert-danger">{error}</div>} */}
                    </div>

                    <div className="form-group col-12 col-md-4">
                      <label htmlFor={`FileName${index}`}>
                        File name (english)
                      </label>
                      <input
                        required
                        minLength={3}
                        maxLength={120}
                        onChange={(e) => {
                          this.updateRequiredFileChanged(requireFIle?._id, e);
                        }}
                        // onChange={this.setState({
                        //   requireFIle: this.state.requiredFiles
                        // })}
                        value={`${requireFIle?.fileName}`}
                        name={`FileName${index}`}
                        id={`FileName${index}`}
                        className={`form-control `}
                      />
                      {/* {error && <div className="  alert alert-danger">{error}</div>} */}
                    </div>

                    <div className="form-group col-4  col-md-3">
                      <label></label>
                      <Button
                        type="button"
                        onClick={() => {
                          this.removeRequredFile(requireFIle?._id);
                        }}
                        className="form-control btn-danger"
                      >
                        remove
                      </Button>
                      {/* {error && <div className="  alert alert-danger">{error}</div>} */}
                    </div>
                  </div>
                </>
              ))}

              <div className="form-group col-5  col-md-3">
                <button
                  type="button"
                  className="mt-1 btn btn-warning"
                  onClick={this.addRequredFile}
                >
                  <PlusSquare />
                </button>
              </div>

              {this.state.Loading ? (
                <Button disabled={true} color="primary" className="mb-1">
                  <SpinnerButtton color="white" size="sm" type="grow" />
                  <span className="ml-50">Loading...</span>
                </Button>
              ) : (
                this.renderButton("Save")
              )}
            </form>
          </CardBody>
        </Card>
      </Col>
    );
  }
}
export default EditService;
