import React from "react"
import {
  Card,
  CardHeader,
  CardTitle,
  CardBody,
  UncontrolledDropdown,
  DropdownMenu,
  DropdownItem,
  DropdownToggle
} from "reactstrap"
import Chart from "react-apexcharts"
import { Circle, ChevronDown } from "react-feather"

class ProfileStatausAnalyst extends React.Component {
  state = {
    options: {
      colors: [this.props.primary, this.props.danger, this.props.warning],
      fill: {
        type: "gradient",
        gradient: {
          // enabled: true,
          shade: "dark",
          type: "vertical",
          shadeIntensity: 0.5,
          gradientToColors: [
            this.props.primaryLight,
            this.props.dangerLight,
            this.props.warningLight
          ],
          inverseColors: false,
          opacityFrom: 1,
          opacityTo: 1,
          stops: [0, 100]
        }
      },
      stroke: {
        lineCap: "round"
      },
      plotOptions: {
        radialBar: {
          size: 150,
          hollow: {
            size: "20%"
          },
          track: {
            strokeWidth: "100%",
            margin: 15
          },
          dataLabels: {
            name: {
              fontSize: "18px"
            },
            value: {
              fontSize: "16px"
            },
            // total: {
            //   show: true,
            //   label: "Total",

            //   formatter: () => {
            //     return 100
            //   }
            // }
          }
        }
      },
      labels: ["Accepted ", "Rejected ", "Pendding "]
    },
    // series: [70, 52, 26]
    series: [this.props.acceptedProfile, this.props.rejectedProfile, this.props.penddingProfile]

  }

  render() {
    return (
      <Card>
        <CardHeader>
          <CardTitle>Service Status (Profile) </CardTitle>
        </CardHeader>
        <CardBody>
          <Chart
            options={this.state.options}
            series={this.state.series}
            type="radialBar"
            height={350}
            className="mb-3"
          />
          <div className="chart-info d-flex justify-content-between mb-1">
            <div className="series-info d-flex align-items-center">
              <Circle strokeWidth={5} size="12" className="primary" />

              <span className="text-bold-600 ml-50">Accepted Profile</span>
            </div>
            <div className="series-result">

              <span className="align-middle">{this.props.acceptedProfile}%</span>
            </div>
          </div>


          <div className="chart-info d-flex justify-content-between">
            <div className="series-info d-flex align-items-center">
              <Circle strokeWidth={5} size="12" className="danger" />
              <span className="text-bold-600 ml-50">Rejected Profile  </span>
            </div>
            <div className="series-result">
              <span className="align-middle">{this.props.rejectedProfile}%</span>
            </div>
          </div>


          <div className="chart-info d-flex justify-content-between mb-1">
            <div className="series-info d-flex align-items-center">
              <Circle strokeWidth={5} size="12" className="warning" />
              <span className="text-bold-600 ml-50">Pendding Profile</span>
            </div>
            <div className="series-result">
              <span className="align-middle">{this.props.penddingProfile}%</span>
            </div>
          </div>



        </CardBody>
      </Card>
    )
  }
}
export default ProfileStatausAnalyst
