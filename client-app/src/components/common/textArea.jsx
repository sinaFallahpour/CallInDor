import React from "react";
import { Label } from "reactstrap"

const TextArea = ({ name, label, error, className, inputClass, ...rest }) => {
    return (
        <>
            <div className={`form-group ${className}`} >
                <div className="form-label-group mt-2 mb-0">
                    <textarea
                        {...rest}
                        name={name}
                        id={name}
                        rows="5"
                        className={`form-control ${inputClass}`}
                    />
                    <Label>{label}</Label>
                </div>

                {error && <div className="  alert alert-danger">{error}</div>}

            </div >

        </>
    );
};

export default TextArea;
