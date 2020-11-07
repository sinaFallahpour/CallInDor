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

import agent from "../../../core/services/agent";

import "react-block-ui/dist/style.css";
import BlockUi from "react-block-ui";

import ReactLoading from "react-loading";

class _DetailsUser extends React.Component {
    state = {
        data: {
            id: null,
            UserName: "",
            Name: "",
            LastName: "",
            ImageAddress: "",
            ImageAddress: "",
            ImageAddress: "",
            IsCompany: false,
            PhoneNumberConfirmed: false,
            IsEditableProfile: false,
            isLockOut: false,
            ProfileConfirmType: null,
            CountryCode: null,
        },
        requiredFiles: [],

        errors: {},
        errorscustom: [],
        errorMessage: "",
        Loading: false,
        pageLoading: false,
    };

    // schema = {
    //     id: Joi.number(),
    //     name: Joi.string(),
    //     persianName: Joi.string().required().label("Persian Name"),

    //     color: Joi.string().required().label("color"),

    //     minPriceForService: Joi.number()
    //         .required()
    //         .min(1)
    //         .max(10000000000000)
    //         .label("minimum Price"),

    //     minSessionTime: Joi.number()
    //         .required()
    //         .min(1)
    //         .max(10000000)
    //         .label("minimum Session Time"),

    //     acceptedMinPriceForNative: Joi.number()
    //         .required()
    //         .min(1)
    //         .max(10000000000000)
    //         .label("minimum Price For Native User"),

    //     acceptedMinPriceForNonNative: Joi.number()
    //         .required()
    //         .min(1)
    //         .max(10000000000000)
    //         .label("minimum Price For Non Native User"),

    //     roleId: Joi.string().required().label("role"),

    //     roles: Joi.label("roles"),
    // };

    async populatinUser() {
        this.setState({ pageLoading: true });
        const username = this.props.match.params.username;
        try {
            const { data } = await agent.ServiceTypes.details(username);
            let {
                id,
                userName,
                name,
                lastName,
                imageAddress,
                imageAddress,
                imageAddress,
                isCompany,
                phoneNumberConfirmed,
                isEditableProfile,
                isLockOut,
                profileConfirmType,
                countryCode,
                requiredFiles,
            } = data.result.data;

            this.setState({
                data: {
                    id,
                    userName,
                    name,
                    lastName,
                    imageAddress,
                    imageAddress,
                    imageAddress,
                    isCompany,
                    phoneNumberConfirmed,
                    isEditableProfile,
                    isLockOut,
                    profileConfirmType,
                    countryCode,
                    requiredFiles
                }
            });

        } catch (ex) {
            console.log(ex);
            if (ex?.response?.status == 404 || ex?.response?.status == 400) {
                return this.props.history.replace("/not-found");
            }
        }
        this.setState({ pageLoading: false });
    }



    async componentDidMount() {
        this.populatinUser();
    }






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
            const { data } = await agent.ServiceTypes.update(obj);
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

                            <div className={`form-group `} >
                                <label htmlFor={"asasasas"}>asasassa</label>
                                <input readOnly name={"asasasas"} id={"asasasa"} className={`form-control `} />
                            </div >

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
export default _DetailsUser;
