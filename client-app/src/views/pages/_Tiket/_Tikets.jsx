import React from "react";
import { Row, Col } from "reactstrap";
import Breadcrumbs from "../../../components/@vuexy/breadCrumbs/BreadCrumb";
import ListViewConfig from "./DataListConfig";
import queryString from "query-string";
class ProvidedServices extends React.Component {
  render() {
    return (
      <React.Fragment>
        <Breadcrumbs
          breadCrumbTitle="Tikets"
          breadCrumbParent="Data List"
          breadCrumbActive="List View"
        />
        <Row>
          <Col sm="12">
            <ListViewConfig
              parsedFilter={queryString.parse(this.props.location.search)}
            />
          </Col>
        </Row>
      </React.Fragment>
    );
  }
}

export default ProvidedServices;
