import React from "react";
import { Row, Col } from "reactstrap";
import SalesCard from "./SalesCard";
import SuberscribersGained from "../../ui-elements/cards/statistics/SubscriberGained";
import OrdersReceived from "../../ui-elements/cards/statistics/OrdersReceived";
import AvgSession from "../../ui-elements/cards/analytics/AvgSessions";
import SupportTracker from "../../ui-elements/cards/analytics/SupportTracker";
import ProductOrders from "../../ui-elements/cards/analytics/ProductOrders";
import SalesStat from "../../ui-elements/cards/analytics/Sales";
import ActivityTimeline from "./ActivityTimeline";
import DispatchedOrders from "./DispatchedOrders";
import "../../../assets/scss/pages/dashboard-analytics.scss";

import StatisticsCard from "../../../components/@vuexy/statisticsCard/StatisticsCard";
import {
  // Monitor,
  // UserCheck,
  // Mail,
  Eye,
  // MessageSquare,
  // ShoppingBag,
  // Heart,
  // Smile,
  // Truck,
  // Cpu,
  // Server,
  // Activity,
  // AlertOctagon
} from "react-feather";

const $primary = "#7367F0";
const $danger = "#EA5455";
const $warning = "#FF9F43";
const $info = "#00cfe8";
const $primary_light = "#9c8cfc";
const $warning_light = "#FFC085";
const $danger_light = "#f29292";
const $info_light = "#1edec5";
const $stroke_color = "#e8e8e8";
const $label_color = "#e7eef7";
const $white = "#fff"

class AnalyticsDashboard extends React.Component {
  render() {
    return (
      <React.Fragment>
        <Row className="match-height">
          <Col xl="2" lg="4" sm="6">
            <StatisticsCard
              hideChart
              iconBg="primary"
              icon={<Eye className="primary" size={22} />}
              stat="36.9k"
              statTitle="Views"
            />
          </Col>

          <Col lg="6" md="12">
            <SalesCard />
          </Col>
          <Col lg="3" md="6" sm="12">
            <SuberscribersGained />
          </Col>
          <Col lg="3" md="6" sm="12">
            <OrdersReceived />
          </Col>
        </Row>
        <Row className="match-height">
          <Col md="6" sm="12">
            <AvgSession labelColor={$label_color} primary={$primary} />
          </Col>
          <Col md="6" sm="12">
            <SupportTracker
              primary={$primary}
              danger={$danger}
              white={$white}
            />
          </Col>
        </Row>
        <Row className="match-height">
          <Col lg="4">
            <ProductOrders
              primary={$primary}
              warning={$warning}
              danger={$danger}
              primaryLight={$primary_light}
              warningLight={$warning_light}
              dangerLight={$danger_light}
            />
          </Col>
          <Col lg="4">
            <SalesStat
              strokeColor={$stroke_color}
              infoLight={$info_light}
              primary={$primary}
              info={$info}
            />
          </Col>
          <Col lg="4">
            <ActivityTimeline />
          </Col>
        </Row>
        <Row>
          <Col sm="12">
            <DispatchedOrders />
          </Col>
        </Row>
      </React.Fragment>
    )
  }
}

export default AnalyticsDashboard
