import React, { useState } from "react";
import {
    FormGroup,
    Input,
} from "reactstrap";
// import "../../styles/admin-pannel/admin.css";
import $ from 'jquery';

const CustomTagsInput = (props) => {

    const [validate, setValidate] = useState();
    const [modal, setModal] = useState(false);



    const removeTags = indexToremove => {
        let newTopics = props.topics.filter((_, index) => { return index !== indexToremove })
        props.handleDeleteTopic(newTopics)
    };

    const addTags = even => {
        even.preventDefault();
        const res = validateIput($(".tagsInput").val())
        if (!res)
            return
        else
            setValidate({ isvalid: true, error: "speciality is required" })

        const inputValue = $(".tagsInput").val()
        if (inputValue !== "") {
            // setTags([...tags, inputValue]);
            $(".tagsInput").val('');
            props.handleAddTopic(inputValue)
        }
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

    const toggleModal = () => {
        setModal(x => !x)
    }

    return (
        <FormGroup >
            <label className="_inputLable form-control-label" > Speciality </label>

            <div className="contain ">
                <Input
                    type="text"
                    required
                    minLength={3}
                    placeHolder="please Enter"
                    className="_box-shadow form-control-alternative col-12 tagsInput" />
                {validate?.isvalid == false && <div className="  alert alert-danger">{validate?.error}</div>}

                <button className="mt-1 btn btn-warning" onClick={(e) => addTags(e)} >Add</button>
            </div>
            <ul className="topics">
                {props.topics.map((tag, index) => (
                    <li key={index}>
                        <span>{tag}
                            <i
                                className="material-icons"
                                onClick={() => removeTags(index)}
                            >
                                close
                            </i>
                        </span>
                    </li>
                ))}

            </ul>
        </FormGroup >
    );
};
export default CustomTagsInput;
