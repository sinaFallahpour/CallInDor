import React from "react";
import { Row, Col } from "reactstrap";
import Breadcrumbs from "../../../components/@vuexy/breadCrumbs/BreadCrumb";
import SweetAlert from 'react-bootstrap-sweetalert';

// import SubscribersGained from "./SubscriberGained";
// import RevenueGenerated from "./RevenueGenerated";
// import QuaterlySales from "./QuaterlySales";
// import OrdersReceived from "./OrdersReceived;"


import RoleItem from "./RoleItem";
import agent from "../../../core/services/agent";



import {
    Edit,
} from "react-feather";


import {
    Button,
    Modal,
    ModalHeader,
    ModalBody,
    ModalFooter,
    Label,
    FormGroup,
    Input,
    Card,
    CardHeader,
    CardBody,
    CardTitle,
    TabContent,
    TabPane,
    Nav,
    NavItem,
    NavLink,
    Form,
    Alert,
    Spinner,
} from "reactstrap";


// import {
//     siteTraffic,
//     siteTrafficSeries,
//     activeUsers,
//     activeUsersSeries,
//     newsLetter,
//     newsLetterSeries
// } from "./StatisticsData2";
import { toast } from "react-toastify";
import { ThemeProvider } from "styled-components";

class RoleManager extends React.Component {
    state = {
        data: {
            id: "",
            name: "",
            isEnabled: true,
        },

        addNew: true,

        roles: [],
        loading: true,
        loadingRole: false,
        loadingSubmiting: false,

        errors: [],
        errorMessage: "",
        Loading: false,
        activeTab: "1",

        modal: false,

    };

    // async componentDidMount() {
    //     const { data } = await agent.Role.list();
    //     const courses = data.result;
    //     this.setState({ courses });
    // }

    async componentDidMount() {
        const { data } = await agent.Role.list();
        if (data?.result) {
            this.setState({ roles: data.result.data, loading: false });
            return;
        }
        toast.error(data.message, {
            autoClose: 10000,
        });
    }

    toggleModal = (role) => {
        if (!role) {
            alert("role")
            this.setState({ modal: false })
            return
        }
        alert("no Role")
        this.setState((prevState) => ({
            modal: !prevState.modal,
            addNew: false,
            data: { id: role.id, name: role.name, isEnabled: role.isEnabled }
        }));
    };

    addNewModal = async (modal) => {
        alert(modal)
        await this.setState((prev) => ({
            modal,
            addNew: true,
            data: { id: null, name: "", isEnabled: false }

        }))


        // alert(this.stat.modal)
    }

    mapToViewModel(role) {
        if (role) {
            return {
                id: role.id,
                name: role.name,
                isEnabled: role.isEnabled,
            };
        }
    }



    // toggleModal1 = () => {
    //     this.setState((prevState) => ({
    //         modal: !prevState.modal,
    //     }));
    // };



    doSubmit = async (e) => {
        this.setState({ loadingSubmiting: true });
        e.preventDefault();
        const errorMessage = "";
        const errors = [];
        this.setState({ errorMessage, errors });
        try {
            const {
                name,
                isEnabled,
                id
            } = this.state.data;
            const obj = {
                name,
                isEnabled,
                id
            };


            if (this.state.addNew) {
                alert(21)
                const { data } = await agent.Role.create(obj);
                if (data.result.status) {
                    toast.success(data.result.message);
                    let roles = this.state.roles.push(data.result.data)
                    this.setState({ roles })
                    // obj.id = data.result.data.id;
                    // this.props.GetAllCategory(obj);
                }
                setTimeout(() => {
                    this.setState({
                        modal: false,
                        loadingSubmiting: false,
                        name: "",
                        isEnabled: "",
                        id: null,
                    });
                    // this.toggleModal()
                }, 2000);

            }

            else {
                const { data } = await agent.Role.update(obj);
                if (data.result.status) {
                    toast.success(data.result.message);
                    let roles = this.state.roles.push(data.result.data)
                    this.setState({ roles })
                    // obj.id = data.result.data.id;
                    // this.props.GetAllCategory(obj);
                }
                setTimeout(() => {
                    this.setState({
                        modal: false,
                        loadingSubmiting: false,
                        name: "",
                        isEnabled: "",
                        id: null,
                    });
                    // this.toggleModal()
                }, 2000);

            }

            // else {
            //     const { data } = await agent.Role.update(obj);
            //     if (data.result.status) {
            //         toast.success(data.result.message);

            //         obj.id = data.result.data.id;
            //         this.props.GetAllCategory(obj);
            //     }
            //     setTimeout(() => {
            //         this.setState({
            //             modal: false,
            //             Loading: false,
            //             title: "",
            //             persianTitle: "",
            //             serviceId: null,
            //             parentId: null,
            //         });
            //         // this.toggleModal()
            //     }, 2000);

            // }



        } catch (ex) {
            console.log(ex);
            if (ex?.response?.status == 400) {
                const errors = ex?.response?.data?.errors;
                this.setState({ errors });
            } else if (ex?.response) {
                const errorMessage = ex?.response?.data?.Message;
                this.setState({ errorMessage });
                toast.error(errorMessage, {
                    autoClose: 10000,
                });
            }

        }
        finally {

            setTimeout(() => {
                this.setState({ loadingSubmiting: false });
            }, 1000);
        }
    };











    render() {
        const { errorMessage, errors, roles, loadingRole, loading, data } = this.state

        if (loading)
            return "loading"
        if (loadingRole)
            return "loading Role"



        return (
            <React.Fragment>

                {this.state.addNew ? "add " : "edit"}
                <Breadcrumbs
                    breadCrumbTitle="Statistics Cards"
                    breadCrumbParent="Card"
                    breadCrumbActive="Statistics Cards"
                />
                <CardHeader>
                    <Button.Ripple
                        color="primary"
                        onClick={() => { this.addNewModal(true) }}
                    >
                        Create New Service
                </Button.Ripple>
                </CardHeader>
                {/* <div onClick={() => this.handleAlert("defaultAlert", true)}>asdas</div> */}

                <Row>
                    {roles.map((role) => {
                        return (
                            <Col key={role.id} lg="6" sm="6">
                                <RoleItem
                                    hideChart
                                    iconRight
                                    iconBg="success"
                                    icon={<Edit className="warning" size={22} />}
                                    stat={role.name}
                                    isEnabled={role.isEnabled}
                                    toggleModal={this.toggleModal}
                                    role={role}
                                />
                            </Col>
                        )
                    })
                    }

                    {/* <Col xl="2" lg="4" sm="6">
                        <StatisticsCard
                            hideChart
                            iconBg="warning"
                            icon={<Truck className="warning" size={22} />}
                            stat="2"
                            statTitle="Returns"
                        />
                    </Col> */}
                </Row>

                <Card>
                    <CardBody>
                        <TabContent activeTab={this.state.activeTab}>
                            <TabPane tabId="1">

                                <Modal
                                    isOpen={this.state.modal}
                                    toggle={() => { this.addNewModal(false) }}
                                    className="modal-dialog-centered"
                                >
                                    <ModalHeader toggle={() => { this.addNewModal(false) }}>
                                        {this.state.addNew ? "Add Role " : "Edit Role"}
                                    </ModalHeader>
                                    <ModalBody>
                                        {errors &&
                                            errors.map((err, index) => {
                                                return (
                                                    <Alert
                                                        key={index}
                                                        className="text-center"
                                                        color="danger "
                                                    >
                                                        {err}
                                                    </Alert>
                                                );
                                            })}
                                        <Form action="/s" className="mt-3" onSubmit={this.doSubmit}>
                                            <FormGroup>
                                                <h5 for="email">name:</h5>
                                                <Input
                                                    type="text"
                                                    id="name"
                                                    value={this.state.data.name}
                                                    onChange={(e) => {
                                                        this.setState({ data: { ...this.state.data, name: e.target.value } });
                                                    }}
                                                    placeholder="name"
                                                    required
                                                    minLength="1"
                                                    maxLength="100"
                                                />
                                            </FormGroup>

                                            <FormGroup className="ml-1 _customCheckbox">
                                                <Label check>
                                                    <Input
                                                        type="checkbox"
                                                        value={this.state.data.isEnabled}
                                                        onChange={(e) =>
                                                            // alert(e.target.value)
                                                            this.setState({
                                                                data: {
                                                                    ...this.state.data,
                                                                    isEnabled:
                                                                        e.target.value == "on" ? true : false,
                                                                }
                                                            })
                                                        }
                                                        name="isEnabled"
                                                    />
                                            Is this role  ctive?

                                        </Label>
                                            </FormGroup>


                                            {this.state.loadingSubmiting ? (
                                                <Button
                                                    disabled={true}
                                                    color="primary"
                                                    className="mb-1"
                                                >
                                                    <Spinner color="white" size="sm" type="grow" />
                                                    <span className="ml-50">Loading...</span>
                                                </Button>
                                            ) : (
                                                    <Button color="primary">submit</Button>
                                                )}
                                        </Form>
                                    </ModalBody>
                                </Modal>



                            </TabPane >
                        </TabContent >
                    </CardBody>
                </Card>


            </React.Fragment >
        )
    }
}

export default RoleManager;
