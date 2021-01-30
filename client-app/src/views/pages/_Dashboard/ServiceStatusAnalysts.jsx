import React from "react";
import {
  Card,
  CardHeader,
  CardTitle,
  CardBody,

} from "reactstrap";
import Chart from "react-apexcharts";
import {
  // Monitor,
  // ArrowUp,
  // Smartphone,
  Tablet,
  // ArrowDown,
  // ChevronDown,
} from "react-feather";

class ServiceStatusAnalysts extends React.Component {
  state = {
    options: {
      chart: {
        toolbar: {
          show: false,
        },
      },
      dataLabels: {
        enabled: false,
      },
      legend: { show: false },
      // comparedResult: [2, -3, 8],


      labels: ["chatVoice", "videoCal", "voiceCall", "service", "course"],
      stroke: { width: 0 },
      colors: [this.props.info_light, this.props.warning, this.props.danger,
      this.props.white, this.props.info],

      fill: {
        type: "gradient",
        gradient: {
          gradientToColors: [
            this.props.primaryLight,
            this.props.warningLight,
            this.props.dangerLight,
            this.props.whiteLight,
            this.props.dangerLight,

          ],
        },
      },
    },
    // series: [  58.6, 34.9, 6.5],
    series: [this.props.chatVoiceCount, this.props.videoCalCount, this.props.voiceCallCount,
    this.props.serviceCount, this.props.courseCount],


  };
  // chatVoiceCount, videoCalCount, voiceCallCount, serviceCount, courseCount



  render() {
    return (
      <Card>
        <CardHeader>
          <CardTitle> Servcie Status (serviceType) </CardTitle>

        </CardHeader>
        <CardBody className="pt-0">
          <Chart
            options={this.state.options}
            series={this.state.series}
            type="donut"
            height={290}
          />
          <div className="chart-info d-flex justify-content-between mb-1 mt-2">
            <div className="series-info d-flex align-items-center">
              <Tablet size="18" className="primary" />
              <span className="text-bold-600 mx-50">chatVoice</span>
              <span className="align-middle">  {this.props.chatVoiceCount}%</span>
            </div>
          </div>
          <div className="chart-info d-flex justify-content-between mb-1 mt-1">
            <div className="series-info d-flex align-items-center">
              <Tablet size="18" className="warning" />


              <span className="text-bold-600 mx-50">videoCal</span>
              <span className="align-middle">  {this.props.videoCalCount} %</span>
            </div>

          </div>
          <div className="chart-info d-flex justify-content-between mt-1">
            <div className="series-info d-flex align-items-center">
              <Tablet size="18" className="danger" />
              <span className="text-bold-600 mx-50">voiceCall</span>
              <span className="align-middle">  {this.props.voiceCallCount}%</span>
            </div>
          </div>
          <div className="chart-info d-flex justify-content-between mt-1">
            <div className="series-info d-flex align-items-center">
              <Tablet size="18" className="danger" />
              <span className="text-bold-600 mx-50">service</span>
              <span className="align-middle">  {this.props.serviceCount}%</span>
            </div>
          </div>

          <div className="chart-info d-flex justify-content-between mt-1">
            <div className="series-info d-flex align-items-center">
              <Tablet size="18" className="danger" />
              <span className="text-bold-600 mx-50">course</span>
              <span className="align-middle">  {this.props.courseCount}%</span>
            </div>
          </div>


        </CardBody>
      </Card>
    );
  }
}
export default ServiceStatusAnalysts;
