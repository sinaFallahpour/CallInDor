import React from "react";
import {
    Card,
    CardHeader,
    CardTitle,
    CardBody,
    Button,
    Alert,
    Col,
    Spinner,
    Row,
} from "reactstrap";

import Joi from "joi-browser";
import Form from "../../../components/common/form";


import agent from "../../../core/services/agent";
import { toast } from "react-toastify";


// const formSchema = Yup.object().shape({
//   // id,isEnabled,title,persianTitle,serviceName,parentName
//   title: Yup.string().required(" Required").min(1, "Too Short!").max(100, "Too Long!"),
//   persianTitle: Yup.string().required(" Required").min(1, "Too Short!").max(100, "Too Long!"),
//   isEnabled: Yup.boolean().required(" Required")
//   // required: Yup.string().required(" Required"),
//   // email: Yup.string().email("Invalid email").required("Required"),
//   // number: Yup.number().required("Required"),
//   // url: Yup.string().url().required("Required"),
//   // date: Yup.date().required("Required"),
//   // minlength: Yup.string().min(4, "Too Short!").required("Required"),
//   // maxlength: Yup.string().max(5, "Too Long!").required("Required")
// });

class Settings extends Form {
    state = {
        data: {
            id: null,
            aboutus: "",
            aboutusEnglish: "",
            address: "",
            addressEnglish: "",
            phoneNumber: "",
            phoneNumberEnglish: "",
            email: "",
            emailEnglish: "",

            profileConfirmNotificationText: "",
            profileConfirmNotificationTextEnglish: "",
            profileRejectNotificationText: "",
            profileRejectNotificationTextEnglish: "",

            serviceConfirmNotificationText: "",
            serviceConfirmNotificationTextEnglish: "",
            serviceRejectNotificationText: "",
            serviceRejectNotificationTextEnglish: ""


            // profileRejectNotificationText: Joi.string().required().label("profile rejection notification text"),
            // profileRejectNotificationTextEnglish: Joi.string().required().label("profile rejection notification text (English)"),




        },

        loadingData: true,
        errors: {},
        errorscustom: [],
        errorMessage: "",
        Loading: false,
    };

    schema = {
        id: Joi.number(),
        aboutus: Joi.string().required().label("About us"),
        aboutusEnglish: Joi.string().required().label("Aboutus (English)"),

        address: Joi.string().required().label("Address"),
        addressEnglish: Joi.string().required().label("Address (English)"),

        phoneNumber: Joi.string().required().label("phoneNumber"),
        phoneNumberEnglish: Joi.string().required().label("phoneNumber (English)"),

        email: Joi.string().required().email({ minDomainAtoms: 2 }).label("email"),
        emailEnglish: Joi.string().required().email({ minDomainAtoms: 2 }).label("email (English)"),


        profileConfirmNotificationText: Joi.string().required().label("profile confirmation notification text "),
        profileConfirmNotificationTextEnglish: Joi.string().required().label("profile confirmation notification text (English)"),

        profileRejectNotificationText: Joi.string().required().label("profile rejection notification text"),
        profileRejectNotificationTextEnglish: Joi.string().required().label("profile rejection notification text (English)"),


        serviceConfirmNotificationText: Joi.string().required().label("service confirmation notification text"),
        serviceConfirmNotificationTextEnglish: Joi.string().required().label("service confirmation notification text(English)"),

        serviceRejectNotificationText: Joi.string().required().label("service rejection notification text"),
        serviceRejectNotificationTextEnglish: Joi.string().required().label("service rejection notification text(English)")


    };


    async populatingSettings() {
        const { data } = await agent.Settings.details();
        let settings = data.result.data;
        this.setState({
            data: {
                aboutus: settings.aboutus,
                aboutusEnglish: settings.aboutusEnglish,
                address: settings.address,
                addressEnglish: settings.addressEnglish,
                phoneNumber: settings.phoneNumber,
                phoneNumberEnglish: settings.phoneNumberEnglish,
                email: settings.email,
                emailEnglish: settings.emailEnglish,

                profileConfirmNotificationText: settings.profileConfirmNotificationText,
                profileConfirmNotificationTextEnglish: settings.profileConfirmNotificationTextEnglish,

                profileRejectNotificationText: settings.profileRejectNotificationText,
                profileRejectNotificationTextEnglish: settings.profileRejectNotificationTextEnglish,

                serviceConfirmNotificationText: settings.serviceConfirmNotificationText,
                serviceConfirmNotificationTextEnglish: settings.serviceConfirmNotificationTextEnglish,

                serviceRejectNotificationText: settings.serviceRejectNotificationText,
                serviceRejectNotificationTextEnglish: settings.serviceRejectNotificationTextEnglish


            },
            loadingData: false
        });
    }

    async componentDidMount() {
        this.populatingSettings();
    }

    doSubmit = async () => {
        this.setState({ Loading: true });
        const errorMessage = "";
        const errorscustom = [];
        this.setState({ errorMessage, errorscustom });
        try {
            const obj = { ...this.state.data };
            const { data } = await agent.Settings.update(obj);
            if (data.result.status) {
                toast.success(data.result.message);
            }
        } catch (ex) {
            // console.log(ex);
            // if (ex?.response?.status == 400) {
            //     const errorscustom = ex?.response?.data?.errors;
            //     this.setState({ errorscustom });
            // } else if (ex?.response) {
            //     const errorMessage = ex?.response?.data?.Message;
            //     this.setState({ errorMessage });
            //     toast.error(errorMessage, {
            //         autoClose: 10000,
            //     });
            // }
        } finally {
            setTimeout(() => {
                this.setState({ Loading: false });
            }, 800);
        }
    };
    render() {
        const { errorscustom, errorMessage, data } = this.state;

        return (
            <Col sm="10" className="mx-auto">
                <Card>
                    <CardHeader>
                        <CardTitle> Settings </CardTitle>
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
                            <Row >
                                {this.renderTextArea("aboutus", "About us(Psersian)", "textarea", "col-12", "text-right ")}
                                {this.renderTextArea("aboutusEnglish", "Aboutus (English)", "text", "col-12")}
                                {this.renderInput("address", "Address(Persian)", "text", "col-6", "text-right ")}
                                {this.renderInput("addressEnglish", "Address (English)", "text", "col-6")}
                                {this.renderInput("phoneNumber", "PhoneNumber(Persian)", "number", "col-6", "text-right ")}
                                {this.renderInput("phoneNumberEnglish", "PhoneNumber (English)", "number", "col-6")}
                                {this.renderInput("email", "Email(Persian)", "email ", "col-6")}
                                {this.renderInput("emailEnglish", "Email (English)", "email", "col-6")}
                                {this.renderInput("profileConfirmNotificationText", "profile confirm Notification Text", "text", "col-6")}
                                {this.renderInput("profileConfirmNotificationTextEnglish", "profile confirm Notification Text (English)", "text", "col-6")}
                                {this.renderInput("profileRejectNotificationText", "profile reject Notification Text", "text", "col-6")}
                                {this.renderInput("profileRejectNotificationTextEnglish", "profile reject Notification Text (English)", "text", "col-6")}
                                {this.renderInput("serviceConfirmNotificationText", "service confirm notification text", "text", "col-6")}
                                {this.renderInput("serviceConfirmNotificationTextEnglish", "service confirm notification text (English)", "text", "col-6")}
                                {this.renderInput("serviceRejectNotificationText", "service reject notification text", "text", "col-6")}
                                {this.renderInput("serviceRejectNotificationTextEnglish", "service reject notification text (English)", "text", "col-6")}

                            </Row>
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
        );
    }
}

export default Settings;
