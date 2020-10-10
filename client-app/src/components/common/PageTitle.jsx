import { useEffect } from "react";

const PageTitle = (props) => {
  useEffect(() => {
    document.title = "CallInDoor | " + props.title;
  }, [props.title]);

  return props.children;
};

export default PageTitle;
