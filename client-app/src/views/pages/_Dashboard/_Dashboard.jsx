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
import ProfileStatausAnalyst from "./ProfileStatausAnalyst"


import "../../../assets/scss/pages/dashboard-analytics.scss";
import Spinner from "../../../components/@vuexy/spinner/Loading-spinner";

import StatisticsCard from "../../../components/@vuexy/statisticsCard/StatisticsCard"
import {
    Monitor,
    UserCheck,
    Mail,
    Eye,
    MessageSquare,
    ShoppingBag,
    Heart,
    Smile,
    Truck,
    Cpu,
    Server,
    Activity,
    AlertOctagon,
    User,
    Check

} from "react-feather";

import ServiceStatusAnalysts from "./ServiceStatusAnalysts"
import AcceptedServiceAnalyst from "./AcceptedServiceAnalyst"


import agent from '../../../core/services/agent'
import { toast } from 'react-toastify'

let $primary = "#7367F0",
    $danger = "#EA5455",
    $warning = "#FF9F43",
    $info = "#00cfe8",
    $primary_light = "#9c8cfc",
    $warning_light = "#FFC085",
    $danger_light = "#f29292",
    $info_light = "#1edec5",
    $stroke_color = "#e8e8e8",
    $label_color = "#e7eef7",
    $white = "#fff"






class Dashboard extends React.Component {

    state = {

        chatVoiceCount: null,
        videoCalCount: null,
        voiceCallCount: null,
        serviceCount: null,
        courseCount: null,


        usersCount: null,
        activeUserCount: null,
        publishedServiceProvider: null,

        acceptedService: null,
        rejectedService: null,
        penddingService: null,

        loading: true,
    };
    async componentDidMount() {
        const { data } = await agent.Home.details();
        this.setState({
            chatVoiceCount: data?.baseServiceAnalyst?.chatVoiceCount,
            videoCalCount: data?.baseServiceAnalyst?.videoCalCount,
            voiceCallCount: data?.baseServiceAnalyst?.voiceCallCount,
            serviceCount: data?.baseServiceAnalyst?.serviceCount,
            courseCount: data?.baseServiceAnalyst?.courseCount,

            usersCount: data?.usersCount,
            activeUserCount: data?.activeUserCount,
            publishedServiceProvider: data?.publishedServiceProvider,


            acceptedService: data?.serviceStatus.accepted,
            rejectedService: data?.serviceStatus.rejected,
            penddingService: data?.serviceStatus.pendding,

            acceptedProfile: data?.profileStatus.acceptedProfile,
            rejectedProfile: data?.profileStatus.rejectedProfile,
            penddingProfile: data?.profileStatus.penddingProfile,



            loading: false,
        })
    }



    render() {

        if (this.state?.loading) {
            return (<Spinner></Spinner>)
        }
        const { chatVoiceCount, videoCalCount, voiceCallCount, serviceCount, courseCount,
            usersCount, activeUserCount, publishedServiceProvider,
            acceptedService, rejectedService, penddingService,
            acceptedProfile, rejectedProfile, penddingProfile
        } = this.state;

        return (
            <React.Fragment>
                <Row className="match-height">
                    <Col xl="3" lg="3" sm="6">
                        <StatisticsCard
                            hideChart
                            iconBg="primary"
                            icon={<User className="primary" size={22} />}
                            stat={usersCount}
                            statTitle="All Users"
                        />
                    </Col>

                    <Col xl="3" lg="3" sm="6">
                        <StatisticsCard
                            hideChart
                            iconBg="primary"
                            icon={<Check className="primary" size={22} />}
                            stat={activeUserCount}
                            statTitle="Active User"
                        />
                    </Col>
                    <Col xl="3" lg="3" sm="6">
                        <StatisticsCard
                            hideChart
                            iconBg="primary"
                            icon={<Truck className="primary" size={22} />}
                            stat={publishedServiceProvider}
                            statTitle="All Service Provied"
                        />
                    </Col>

                    <Col xl="3" lg="3" sm="6">
                        <StatisticsCard
                            hideChart
                            iconBg="primary"
                            icon={<Truck className="primary" size={22} />}
                            stat="12k"
                            statTitle="Views"
                        />
                    </Col>

                </Row>

                <Row className="match-height">
                    <Col lg="4" sm="12">
                        <ServiceStatusAnalysts
                            info_light={$info_light}
                            primary={$primary}
                            warning={$warning}
                            danger={$danger}
                            info={$info}
                            white={$white}

                            primaryLight={$primary_light}
                            warningLight={$warning_light}
                            dangerLight={$danger_light}
                            whiteLight={$info_light}

                            chatVoiceCount={chatVoiceCount}
                            videoCalCount={videoCalCount}
                            voiceCallCount={voiceCallCount}
                            serviceCount={serviceCount}
                            courseCount={courseCount}
                        />
                    </Col>



                    <Col lg="4" sm="12">
                        <AcceptedServiceAnalyst
                            primary={$primary}
                            warning={$warning}
                            danger={$danger}
                            primaryLight={$primary_light}
                            warningLight={$warning_light}
                            dangerLight={$danger_light}

                            acceptedService={acceptedService}
                            rejectedService={rejectedService}
                            penddingService={penddingService}

                        />
                    </Col>

                    <Col lg="4" sm="12">
                        <ProfileStatausAnalyst
                            primary={$primary}
                            warning={$warning}
                            danger={$danger}
                            primaryLight={$primary_light}
                            warningLight={$warning_light}
                            dangerLight={$danger_light}

                            acceptedProfile={acceptedProfile}
                            rejectedProfile={rejectedProfile}
                            penddingProfile={penddingProfile}
                        />
                    </Col>

                </Row>


                <Row className="match-height">


                    <Col lg="6" md="12">
                        <SalesCard />
                    </Col>
                    <Col lg="3" md="6" sm="12">
                        <SuberscribersGained />
                    </Col>
                    <Col lg="3" md="6" sm="12">
                        <OrdersReceived />
                    </Col>

                    {/* <Col lg="4" sm="12">
                        <SessionByDevice
                            info_light={$info_light}
                            primary={$primary}
                            warning={$warning}
                            danger={$danger}
                            info={$info}
                            white={$white}
                            
                            primaryLight={$primary_light}
                            warningLight={$warning_light}
                            dangerLight={$danger_light}
                            whiteLight={$info_light}
                          
                            chatVoiceCount={chatVoiceCount}
                            videoCalCount={videoCalCount}
                            voiceCallCount={voiceCallCount}
                            serviceCount={serviceCount}
                            courseCount={courseCount}
                        />
                    </Col> */}
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

export default Dashboard
