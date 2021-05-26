import React from "react";
import {
    Modal,
    ModalHeader,
    ModalBody,
    FormGroup,
    Input,
    Form,
    Alert,
    Col,
} from "reactstrap";
import { Check } from "react-feather";

import ReactTagInput from "@pathofdev/react-tag-input";
import "@pathofdev/react-tag-input/build/index.css";

// import { modalForm } from "./ModalSourceCode"
import Checkbox from "../../../components/@vuexy/checkbox/CheckboxesVuexy";

// import { toast } from "react-toastify";
// import agent from "../../../core/services/agent";

class CommentsModal extends React.Component {
    state = {

        currenCommets: [],
        curenId: null,

        errors: [],
        errorMessage: "",
        Loading: false,

        activeTab: "1",
        serviceModal: false,
    };

    async componentDidMount() {

    }

    componentDidUpdate(prevProps, prevState) {
        if (this.props.currenCommets !== null) {
            if (this.props.curenId != prevState.curenId) {
                this.setState({
                    currenCommets: this.props.currenCommets,
                    commentsModal: this.props.commentsModal,
                    curenId: this.props.curenId
                })
            }
        }
        if (this.props.currenCommets === null && prevProps.currenCommets !== null) {
            this.setState({
                currenCommets: [],
                commentsModal: false,
                curenId: null
            });
        }
    }



    toggleTab = (tab) => {
        if (this.state.activeTab !== tab) {
            this.setState({ activeTab: tab });
        }
    };

    returnDate = (date) => {

        date = new Date(date)
        const timeString = date.getHours() + ':' + date.getMinutes() + ':00';

        const year = date.getFullYear();
        const month = date.getMonth() + 1;
        const day = date.getDate();
        const dateString = `${year}/${month}/${day} ${date.getHours()}:${date.getMinutes()}`;
        return dateString

    }


    render() {
        const { currenCommets } = this.state;
        return (
            <React.Fragment>
                <Modal
                    isOpen={this.props.commentsModal}
                    toggle={this.props.toggleCommentsModal}
                    className="modal-dialog-centered modal-lg"
                >
                    <ModalHeader toggle={this.props.toggleCommentsModal}>
                        Service comments
                    </ModalHeader>
                    <ModalBody>




                        <Form action="/s" className="mt-3" >





                            <FormGroup row>

                                {/* {console.log(this.state.currenCommets.length)} */}

                                {this.state.currenCommets?.length == 0 ?

                                    <h3 className="d-block  w-100 text-center">
                                        there is no comments for this service
                                    </h3>
                                    :
                                    null
                                }
                                {this.state.currenCommets?.map((item) => {

                                    return (
                                        <Col md="6" key={item.id} className=" mb-1">
                                            <h5
                                                className="m-1"
                                                for={`item-${item.id}`}>
                                                {`${item.userName}-(${this.returnDate(item.createDate)})`}</h5>
                                            <Input
                                                type="textarea"
                                                id={`item-${item.id}`}
                                                value={item.comment}
                                                readOnly
                                            />

                                            {item.isConfirmed ?
                                                <button
                                                    className="m-1 btn btn-danger"
                                                    type="button"
                                                    onClick={() => { this.props.handleRejectComment(item) }}

                                                >reject</button>
                                                :
                                                <button className="m-1 btn btn-success"
                                                    type="button"
                                                    onClick={() => { this.props.handleRejectComment(item) }}
                                                >accept</button>
                                            }
                                        </Col>
                                    )
                                })}

                            </FormGroup>


                        </Form>
                    </ModalBody>
                </Modal>


            </React.Fragment >
        );
    }
}
export default CommentsModal;




