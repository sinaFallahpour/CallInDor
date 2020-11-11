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

class _Questions extends React.Component {
    state = {
        data: {
            question1: "",
            answers10: "",
            answers11: "",
            answers12: "",
            answers13: "",


            question2: "",
            answers20: null,
            answers21: null,
            answers22: null,
            answers23: null,

            question3: "",
            answers30: null,
            answers30: null,
            answers30: null,
            answers30: null,


            question4: "",
            answers40: null,
            answers41: null,
            answers42: null,
            answers43: null,

            question5: "",
            answers50: null,
            answers51: null,
            answers52: null,
            answers53: null,



            // id: null,
            // aboutus: "",
            // aboutusEnglish: "",
            // address: "",
            // addressEnglish: "",
            // phoneNumber: "",
            // phoneNumberEnglish: "",
            // email: "",
            // emailEnglish: ""
        },

        loadingData: true,
        errors: {},
        errorscustom: [],
        errorMessage: "",
        Loading: false,
    };

    // schema = {
    //     id: Joi.number(),
    //     aboutus: Joi.string().required().label("About us"),
    //     aboutusEnglish: Joi.string().required().label("Aboutus (English)"),

    //     address: Joi.string().required().label("Address"),
    //     addressEnglish: Joi.string().required().label("Address (English)"),

    //     phoneNumber: Joi.string().required().label("phoneNumber"),
    //     phoneNumberEnglish: Joi.string().required().label("phoneNumber (English)"),

    //     email: Joi.string().required().email({ minDomainAtoms: 2 }).label("email"),
    //     emailEnglish: Joi.string().required().email({ minDomainAtoms: 2 }).label("email (English)"),
    // };


    // question1: "",
    // answers1: null,

    // question2: "",
    // answers2: null,

    // question3: "",
    // answers3: null,

    // question4: "",
    // answers4: null,

    // question5: "",
    // answers5: null,


    async populatingQuestions() {
        const { data } = await agent.Questions.details();
        let settings = data.result.data;
        console.log(settings)
        this.setState({
            data: {
                question1: settings.aboutus,
                aboutusEnglish: settings.aboutusEnglish,
                address: settings.address,
                addressEnglish: settings.addressEnglish,
                phoneNumber: settings.phoneNumber,
                phoneNumberEnglish: settings.phoneNumberEnglish,
                email: settings.email,
                emailEnglish: settings.emailEnglish
            },
            loadingData: false
        });
    }

    async componentDidMount() {
        this.populatingQuestions();
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
        const { errorscustom, errorMessage, data } = this.state;

        return (
            <Col sm="10" className="mx-auto">
                <Card>
                    <CardHeader>
                        <CardTitle> Questions </CardTitle>
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
                            <Row >



                                <Input type="text"
                                    name={"question1"}
                                    value={data.question1}
                                    label={"question1"}
                                    onChange={this.handleChange}
                                    className="col-lg-12"
                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers"}
                                    value={data.answers10}
                                    label={"answer1"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"
                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers11"}
                                    value={data.answers11}
                                    label={"answer1"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"
                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers1"}
                                    value={data.answers12}
                                    label={"answer1"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers1"}
                                    value={data.answers13}
                                    label={"answer1"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"                                // error={errors[name]}
                                />


                                {/* ===========================question2======================== */}


                                <Input type="text"
                                    name={"question2"}
                                    value={data.question2}
                                    label={"question2"}
                                    onChange={this.handleChange}
                                    className="col-lg-12"                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers"}
                                    value={data.answers20}
                                    label={"answer2"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers"}
                                    value={data.answers21}
                                    label={"answer2"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"
                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers12"}
                                    value={data.answers22}
                                    label={"answer2"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"
                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers12"}
                                    value={data.answers23}
                                    label={"answer2"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"
                                // error={errors[name]}
                                />





                                {/* ===========================question3======================== */}


                                <Input type="text"
                                    name={"question3"}
                                    value={data.question3}
                                    label={"question3"}
                                    onChange={this.handleChange}
                                    className="col-lg-12"
                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers"}
                                    value={data.answers30}
                                    label={"answer3"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"
                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers"}
                                    value={data.answers31}
                                    label={"answer3"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"
                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers12"}
                                    value={data.answers32}
                                    label={"answer3"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"
                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers12"}
                                    value={data.answers33}
                                    label={"answer3"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"
                                // error={errors[name]}
                                />





                                {/* ===========================question4======================== */}


                                <Input type="text"
                                    name={"question3"}
                                    value={data.question4}
                                    label={"question4"}
                                    onChange={this.handleChange}
                                    className="col-lg-12"
                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers"}
                                    value={data.answers40}
                                    label={"answer4"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"
                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers"}
                                    value={data.answers41}
                                    label={"answer4"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"
                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers12"}
                                    value={data.answers42}
                                    label={"answer4"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"
                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers12"}
                                    value={data.answers43}
                                    label={"answer4"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"
                                // error={errors[name]}
                                />




                                {/* ===========================question5======================== */}


                                <Input type="text"
                                    name={"question3"}
                                    value={data.question5}
                                    label={"question4"}
                                    onChange={this.handleChange}
                                    className="col-lg-12"
                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers"}
                                    value={data.answers50}
                                    label={"answer5"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"
                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers"}
                                    value={data.answers51}
                                    label={"answer5"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"
                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers"}
                                    value={data.answers52}
                                    label={"answer5"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"
                                // error={errors[name]}
                                />
                                <Input type="text"
                                    name={"answers"}
                                    value={data.answers52}
                                    label={"answer5"}
                                    onChange={this.handleChange}
                                    className="col-lg-3"
                                // error={errors[name]}
                                />


                            </Row>
                            {/* {this.state.Loading ? (
                                <Button disabled={true} color="primary" className="mb-1">
                                    <Spinner color="white" size="sm" type="grow" />
                                    <span className="ml-50">Loading...</span>
                                </Button>
                            ) : (
                                    this.renderButton("Save")
                                )} */}
                        </form>
                    </CardBody>
                </Card>
            </Col>
        );
    }
}
export default _Questions;



const Input = ({ name, label, error, className, ...rest }) => {
    return (
        <div className={`form-group ${className}`} >
            <label htmlFor={name}>{label}</label>
            <input {...rest} name={name} id={name} className={`form-control `} />
            {/* { error && <div className="  alert alert-danger">{error}</div>} */}
        </div >
    );
};









