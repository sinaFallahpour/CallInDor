import React from "react";
import { Row, Col } from "reactstrap";
import Breadcrumbs from "../../../components/@vuexy/breadCrumbs/BreadCrumb";
import Table from "./Table";

class _Questions extends React.Component {
  render() {
    return (
      <React.Fragment>
        <Breadcrumbs
          breadCrumbTitle="Questions "
          breadCrumbParent="Forms & Tables"
          breadCrumbActive="Aggrid Table"
        />
        <Row>
          <Col sm="12">
            <Table />
          </Col>
        </Row>
      </React.Fragment>
    );
  }
}

export default _Questions;
