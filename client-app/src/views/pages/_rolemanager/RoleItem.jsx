import React from "react";
import { Card, CardBody } from "reactstrap";
import Chart from "react-apexcharts";

class RoleItem extends React.Component {
  render() {
    return (
      <Card>
        <CardBody
          className={`${
            this.props.className ? this.props.className : "stats-card-body"
          } d-flex ${
            !this.props.iconRight && !this.props.hideChart
              ? "flex-column align-items-start"
              : this.props.iconRight
              ? "justify-content-between flex-row-reverse align-items-center"
              : this.props.hideChart && !this.props.iconRight
              ? "justify-content-center flex-column text-center"
              : null
          } ${!this.props.hideChart ? "pb-0" : "pb-2"} pt-2`}
        >
          <div className="icon-section">
            <div
              className={`avatar avatar-stats p-50 m-0 ${
                this.props.iconBg
                  ? `bg-rgba-${this.props.iconBg}`
                  : "bg-rgba-primary"
              }`}
            >
              <div
                className="avatar-content"
                onClick={() => {
                  this.props.toggleModal(this.props.role);
                }}
              >
                {this.props.icon}
              </div>
            </div>
          </div>
          <div className="title-section">
            <h2 className="text-bold-600 mt-1 mb-25">{this.props.stat}</h2>
            <div>
              {this.props.isEnabled ? (
                <p className="badge badge-pill badge-light-success">Active</p>
              ) : (
                <p className="badge badge-pill badge-light-warning">
                  DeActivated
                </p>
              )}
            </div>
          </div>
        </CardBody>
        {!this.props.hideChart && (
          <Chart
            options={this.props.options}
            series={this.props.series}
            type={this.props.type}
            height={this.props.height ? this.props.height : 100}
          />
        )}
      </Card>
    );
  }
}
export default RoleItem;
