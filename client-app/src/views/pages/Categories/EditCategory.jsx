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
  Spinner,
  FormText,
} from "reactstrap";

import Joi from "joi-browser";
import Form from "../../../components/common/form";

// import { Formik, Field, Form  } from "formik";
// import * as Yup from "yup";

import agent, { baseUrl } from "../../../core/services/agent";
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

class EditCategory extends Form {
  state = {
    data: {
      id: null,
      title: "",
      persianTitle: "",
      // isEnabled: false,
      serviceName: "",
      parentId: null,
      serviceId: null,
      image: null
      // parentId: null,
    },
    imageAddress: "",
    isEnabled: null,
    isForCourse: null,
    isSubCategory: null,
    isSupplier: null,
    categories: [],
    services: [],

    errors: {},
    errorscustom: [],
    errorMessage: "",
    Loading: false,
  };

  schema = {
    id: Joi.number(),
    title: Joi.string().required().label("Title"),

    persianTitle: Joi.string().required().label("PersianTitle"),

    parentId: Joi.optional().label("parent"),
    image: Joi.optional().label("image"),


    // isEnabled: Joi
    // .boolean()
    //   // .required()
    //   .label("isEnabled"),

    serviceId: Joi.number()
      .error(() => {
        return {
          message: "service Is Required",
        };
      })
      .required()
      .label("service"),
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

  async populatinCategory() {
    const catId = this.props.match.params.id;
    try {
      const { data } = await agent.Category.details(catId);
      let {
        id,
        title,
        persianTitle,
        isEnabled,
        isForCourse,
        isSubCategory,
        isSupplier,
        serviceId,
        parentId,
        imageAddress
      } = data.result.data;
      this.setState({
        data: { id, title: title, persianTitle, serviceId, parentId },
        isEnabled,
        isForCourse,
        isSubCategory,
        isSupplier,
        imageAddress
      });
    } catch (ex) {
      console.clear();
      if (ex?.response?.status == 404 || ex?.response?.status == 400) {
        return this.props.history.replace("/not-found");
      }
    }
  }

  async componentDidMount() {
    this.populatingCategories();
    this.populatingServiceTypes();
    this.populatinCategory();
  }

  // doSubmit = async () => {
  //   await saveMovie(this.state.data);
  //   this.props.history.push("/movies");
  // };

  doSubmit = async () => {
    this.setState({ Loading: true });
    const errorMessage = "";
    const errorscustom = [];
    this.setState({ errorMessage, errorscustom });
    try {
      const { isEnabled, isForCourse, isSubCategory, isSupplier } = this.state
      const obj = { ...this.state.data, isEnabled, isForCourse, isSubCategory, isSupplier };


      let form = new FormData();
      for (var key in obj) {
        form.append(key, obj[key]);
      }
      form.append("image", this.state.image)

      // let form = new FormData();
      // form.append('title', title);
      // form.append('persianTitle', persianTitle);
      // form.append('isEnabled', isEnabled);
      // form.append('isForCourse', isForCourse);
      // form.append('isSubCategory', isSubCategory);
      // form.append('serviceId', serviceId);
      // form.append('parentId', parentId);
      // form.append('serviceName', serviceName);
      // form.append('parentName', parentName);
      // form.append('image', image);



      const { data } = await agent.Category.update(form);

      if (data.result.status) {
        toast.success(data.result.message);
      }
      this.populatinCategory()

    } catch (ex) {
      // console.log(ex);
      // if (ex?.response?.status == 400) {
      //   const errorscustom = ex?.response?.data?.errors;
      //   this.setState({ errorscustom });
      // } else if (ex?.response) {
      //   const errorMessage = ex?.response?.data?.message;
      //   this.setState({ errorMessage });
      //   toast.error(errorMessage, {
      //     autoClose: 10000,
      //   });
      // }
    } finally {
      setTimeout(() => {
        this.setState({ Loading: false });
      }, 800);
    }


  };

  render() {
    const { errorscustom, errorMessage, categories, services } = this.state;

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

            <img src={baseUrl + this.state.imageAddress} className="d-block mx-auto " style={{ maxHeight: "300px" }} />
            <form onSubmit={this.handleSubmit}>
              {this.renderInput("title", "Title")}
              {this.renderInput("persianTitle", "persianTitle")}
              {/* {this.renderSelect("parentId", "Parent", this.state.categories.map(item => ({ name: item.title, id: item.id })))} */}
              {this.renderReactSelect(
                "parentId",
                "Parent",
                categories.map((item) => ({
                  value: item.id,
                  label: item.title,
                }))
              )}

              {/* {this.renderSelect("serviceId", "Service", this.state.services)} */}
              {this.renderReactSelect(
                "serviceId",
                "Service",
                services.map((item) => ({ value: item.id, label: item.name }))
              )}

              {this.renderInput(
                "image",
                "image",
                "file"
              )}

              {/* {this.renderCheckBox("isEnabled", "Is Enabled", "checbox")} */}

              <div className="form-group _customCheckbox">
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
                <label className="ml-2" htmlFor="isEnabled">Is Enabled ?</label>
              </div>

              <div className="form-group _customCheckbox">
                <input
                  value={this.state.isForCourse}
                  checked={this.state.isForCourse}
                  onChange={(e) =>
                    this.setState({ isForCourse: !this.state.isForCourse })
                  }
                  name="isForCourse"
                  id="isForCourse"
                  type="checkbox"
                  className="ml-1"
                />
                <label className="ml-2" htmlFor="isForCourse">Is this category for Course?  </label>

              </div>

              <div className="form-group _customCheckbox">
                <input
                  value={this.state.isSubCategory}
                  checked={this.state.isSubCategory}
                  onChange={(e) =>
                    this.setState({ isSubCategory: !this.state.isSubCategory })
                  }
                  name="isSubCategory"
                  id="isSubCategory"
                  type="checkbox"
                  className="ml-1"
                />
                <label className="ml-2" htmlFor="isSubCategory">Is this a sub category ?</label>
              </div>

              <div className="form-group _customCheckbox">
                <input
                  value={this.state.isSupplier}
                  checked={this.state.isSupplier}
                  onChange={(e) =>
                    this.setState({ isSupplier: !this.state.isSupplier })
                  }
                  name="isSupplier"
                  id="isSupplier"
                  type="checkbox"
                  className="ml-1"
                />
                <label className="ml-2" htmlFor="isSupplier">is for isSupplier ?</label>
              </div>

              {this.state.Loading ? (
                <Button disabled={true} color="primary" className="mb-1">
                  <Spinner color="white" size="sm" type="grow" />
                  <span className="ml-50">Loading...</span>
                </Button>
              ) : (
                this.renderButton("Save")
              )}

              {/* {this.renderSelect("genreId", "Genre", this.state.genres)} */}
              {/* {this.renderInput("numberInStock", "Number in Stock", "number")}
            {this.renderInput("dailyRentalRate", "Rate")}
            {this.renderButton("Save")} */}
            </form>
          </CardBody>
        </Card>
      </Col>
    );
  }
}
export default EditCategory;
