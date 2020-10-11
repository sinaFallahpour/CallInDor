import React from "react";
import Select from "react-select";

const colourOptions = [
  { value: "ocean", label: "Ocean" },
  { value: "blue", label: "Blue" },
  { value: "purple", label: "Purple" },
  { value: "red", label: "Red" },
  { value: "orange", label: "Orange" },
];

const ReactSelect = ({
  name,
  label,
  options,
  error,
  value,
  onChange,
  ...rest
}) => {
  return (
    <>
      <div className="form-group">
        <label htmlFor={name}>{label}</label>
        <Select
          className="React"
          classNamePrefix="select"
          // defaultValue={options[1]}
          value={options.filter((option) => option.value === value)}
          name={name}
          defaultValue={{ value: "", label: "" }}
          //   value={value}
          isClearable={true}
          options={options}
          {...rest}
          onChange={(e) => {
            onChange({ currentTarget: { name: name, value: e?.value } });
          }}
        />
        {error && <div className=" alert alert-danger">{error}</div>}
      </div>
    </>
  );
};

export default ReactSelect;
