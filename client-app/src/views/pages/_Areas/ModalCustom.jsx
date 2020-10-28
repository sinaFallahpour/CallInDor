import React, { useState } from "react";
import {
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  FormGroup,
  Input,
  Card,
  TabContent,
  TabPane,
  Form,
  Col,
} from "reactstrap";
const ModalCustom = (props) => {
  // toggleModal
  // modal
  const [data, setData] = useState({ persianName: "", englishName: "" });
  // state = {
  //     persianName: "",
  //     englishName: ""
  // }

  const handleSubmit = (e) => {
    e.preventDefault();

    const persianName = data.persianName;
    const englishName = data.englishName;
    if (!persianName || !englishName) {
      alert("In valid data");
    } else {
      if (persianName.trim().length < 3) {
        alert("persian name mush be at least 3 character");
        return;
      }
      if (englishName.trim().length < 3) {
        alert("english name mush be at least 3 character");
        return;
      }
      props.handleAddSpeciality(persianName, englishName);
      setData({ persianName: "", englishName: "" });
      // this.state.persianName = "";
      // this.state.englishName = "";
      props.toggleModal();
    }
  };

  return (
    <React.Fragment>
      <Col sm="10" className="mx-auto">
        <Card>
          <TabContent activeTab={true}>
            <TabPane tabId="1">
              <Modal
                isOpen={props.modal}
                toggle={props.toggleModal}
                className="modal-dialog-centered"
              >
                <ModalHeader toggle={props.toggleModal}>
                  New specilaity
                </ModalHeader>
                <ModalBody>
                  {/* {errors &&
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
                })} */}
                  <Form action="/s" className="mt-3" onSubmit={handleSubmit}>
                    <FormGroup>
                      <h5>Persian Name:</h5>
                      <Input
                        type="text"
                        id="PersianName"
                        value={data.persianName}
                        onChange={(e) => {
                          setData({ ...data, persianName: e.target?.value });
                        }}
                        placeholder="Persian Name"
                        required
                        minLength={3}
                        maxLength={100}
                        className="text-right"
                      />
                    </FormGroup>

                    <FormGroup>
                      <h5>English Name:</h5>
                      <Input
                        type="text"
                        id="EnglishName"
                        value={data.englishName}
                        onChange={(e) => {
                          setData({ ...data, englishName: e.target?.value });
                        }}
                        placeholder="English Name"
                        required
                        minLength={3}
                        maxLength={100}
                      />
                    </FormGroup>
                    {/* {this.state.Loading ?
                        <Button disabled={true} color="primary" className="mb-1">
                            <Spinner color="white" size="sm" type="grow" />
                            <span className="ml-50">Loading...</span>
                        </Button>
                        :
                        <Button color="primary">submit</Button>
                    } */}

                    <Button color="primary">submit</Button>
                  </Form>
                </ModalBody>
              </Modal>
            </TabPane>
          </TabContent>
        </Card>
      </Col>
    </React.Fragment>
  );
};

export default ModalCustom;
