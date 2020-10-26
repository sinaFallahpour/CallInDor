import React from "react";

const Input = ({ name, label, error, className, inputClass, ...rest }) => {
  return (
    <div className={`form-group ${className}`} >
      <label htmlFor={name}>{label}</label>
      <input {...rest} name={name} id={name} className={`form-control ${inputClass}`} />
      { error && <div className="  alert alert-danger">{error}</div>}
    </div >
  );
};

export default Input;
