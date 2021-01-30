import React from "react"
import {
  Card,
  CardHeader,
  CardTitle,
  CardBody,
  UncontrolledDropdown,
  DropdownMenu,
  DropdownItem,
  DropdownToggle,
  ListGroup,
  ListGroupItem
} from "reactstrap"
import { ChevronDown } from "react-feather"
import Chart from "react-apexcharts"

class AcceptedServiceAnalyst extends React.Component {
  state = {
    options: {
      chart: {
        dropShadow: {
          enabled: false,
          blur: 5,
          left: 1,
          top: 1,
          opacity: 0.2
        },
        toolbar: {
          show: false
        }
      },
      colors: [this.props.primary, this.props.danger, this.props.warning,],
      fill: {
        type: "gradient",
        gradient: {
          gradientToColors: [
            this.props.primaryLight,
            this.props.dangerLight,
            this.props.warningLight
          ]
        }
      },
      dataLabels: {
        enabled: false
      },
      legend: { show: false },
      stroke: {
        width: 5
      },
      labels: ["Accepted", "Rejected", "Pendding"]
    },
    series: [this.props.acceptedService, this.props.rejectedService, this.props.penddingService]
  }
  render() {
    return (
      <Card>
        <CardHeader>
          <CardTitle>Service Status (Confirm Type)  </CardTitle>
        </CardHeader>
        <CardBody className="pt-0">
          <Chart
            options={this.state.options}
            series={this.state.series}
            type="pie"
            height={290} />
        </CardBody>
        <ListGroup flush>
          <ListGroupItem className="d-flex justify-content-between">
            <div className="item-info">
              <div
                className="bg-primary"
                style={{
                  height: "10px",
                  width: "10px",
                  borderRadius: "50%",
                  display: "inline-block",
                  margin: "0 5px"
                }}
              />
              <span className="text-bold-600">Accepted</span>
            </div>
            <div className="product-result">

              <span>{this.props.acceptedService}%</span>
            </div>
          </ListGroupItem>
          <ListGroupItem className="d-flex justify-content-between">
            <div className="item-info ">
              <div
                className=" bg-danger"
                style={{
                  height: "10px",
                  width: "10px",
                  borderRadius: "50%",
                  display: "inline-block",
                  margin: "0 5px"
                }}
              />
              <span className="text-bold-600">Rejected</span>
            </div>
            <div className="product-result">
              <span>{this.props.rejectedService}% </span>
            </div>
          </ListGroupItem>
          <ListGroupItem className="d-flex justify-content-between">
            <div className="item-info">
              <div
                className="bg-warning"
                style={{
                  height: "10px",
                  width: "10px",
                  borderRadius: "50%",
                  display: "inline-block",
                  margin: "0 5px"
                }}
              />
              <span className="text-bold-600">Pendding</span>
            </div>
            <div className="product-result">
              <span> {this.props.penddingService}% </span>
            </div>
          </ListGroupItem>
        </ListGroup>
      </Card>
    )
  }
}
export default AcceptedServiceAnalyst
