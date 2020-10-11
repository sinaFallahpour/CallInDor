import React, { useState } from "react";

// import "../../styles/admin-pannel/admin.css";
import $ from 'jquery';

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
    Form
} from "reactstrap";

import { PlusCircle, PlusSquare } from "react-feather";

const CustomTagsInput = (props) => {

    const [validate, setValidate] = useState();
    const [modal, setModal] = useState(true);
    const [data, setData] = useState({ persianTitle: "", englishTitle: "" });



    const removeTags = indexToremove => {
        let newTopics = props.speciality.filter((_, index) => { return index !== indexToremove })
        props.handleDeleteTopic(newTopics)
    };

    const addTags = even => {
        even.preventDefault();

        props.toggleModal()

        // even.preventDefault();
        // const res = validateIput($(".tagsInput").val())
        // if (!res)
        //     return
        // else
        //     setValidate({ isvalid: true, error: "speciality is required" })

        // const inputValue = $(".tagsInput").val()
        // if (inputValue !== "") {
        //     // setTags([...tags, inputValue]);
        //     $(".tagsInput").val('');
        //     props.handleAddTopic(inputValue)
        // }
    };


    const validateIput = (value) => {
        if (value?.length == 0) {
            setValidate({ isvalid: false, error: "speciality is required" })
            return false
        }
        if (value?.length < 3) {
            setValidate({ isvalid: false, error: "Specialty must be at least 3 characters" })
            return false
        }
        return true
    }

    // const toggleModal = () => {
    //     setModal(x => !x)
    // }

    return (
        <>
            <FormGroup >
                <label className="_inputLable form-control-label" > Speciality </label>
                <div className="contain ">
                    {/* <Input
                        type="text"
                        required
                        minLength={3}
                        placeHolder="please Enter"
                        className="_box-shadow form-control-alternative col-12 tagsInput" />
                    {validate?.isvalid == false && <div className="  alert alert-danger">{validate?.error}</div>} */}

                    <button disabled={!props.isProfessional} className="mt-1 btn btn-warning" onClick={addTags} >
                        <PlusSquare />
                    </button>
                </div>
                <ul className="topics">
                    {props.speciality.map((tag, index) => (
                        <>
                            <li key={index}>
                                <span>{tag.persianName}
                                    <strong>   |    </strong>
                                    {tag.englishName}
                                    <i
                                        className="material-icons"
                                        onClick={() => removeTags(index)}
                                    >
                                        close
                                    </i>
                                </span>
                            </li>

                        </>
                    ))}
                </ul>
            </FormGroup >
            {/* {modal && (
                <ModalCustom modal={modal} toggleModal={toggleModal} data={data} setData={setData} />
            )} */}
        </>
    );
};
export default CustomTagsInput;




