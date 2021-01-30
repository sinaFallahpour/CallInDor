import React from "react";

// import "../../styles/admin-pannel/admin.css";
import $ from "jquery";

import { FormGroup, Form as FormStrap } from "reactstrap";

import { PlusSquare } from "react-feather";

const CustomTagsInput = (props) => {
  const removeTags = (indexToremove) => {
    const newTopics = props.answersTBLs.filter((_, index) => {
      return index !== indexToremove;
    });
    props.handleDeleteTopic(newTopics);
  };

  const addTags = (even) => {
    even.preventDefault();
    props.toggleModal();
  };

  return (
    <>
      <FormGroup>
        <label className="_inputLable form-control-label"> Speciality </label>
        <div className="contain ">
          {/* <Input
                        type="text"
                        required
                        minLength={3}
                        placeHolder="please Enter"
                        className="_box-shadow form-control-alternative col-12 tagsInput" />
          {validate?.isvalid == false && <div className="  alert alert-danger">{validate?.error}</div>} */}

          <button
            // disabled={!props.isProfessional}
            className="mt-1 btn btn-warning"
            onClick={addTags}
          >
            <PlusSquare />
          </button>
        </div>
        <ul className="topics">
          {props.answersTBLs.map((tag, index) => (
            <>
              <li key={index}>
                <span>
                  {tag.persianName}
                  <strong> | </strong>
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
      </FormGroup>
    </>
  );
};
export default CustomTagsInput;
