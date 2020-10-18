import React from "react";
import { Row, Col } from "reactstrap";
import Breadcrumbs from "../../../components/@vuexy/breadCrumbs/BreadCrumb";
import Table from "./Table";

class Users extends React.Component {
  render() {
    return (
      <React.Fragment>
        <Breadcrumbs
          breadCrumbTitle="Users "
          breadCrumbParent="Forms & Tables"
          breadCrumbActive="Users   "
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

export default Users;
