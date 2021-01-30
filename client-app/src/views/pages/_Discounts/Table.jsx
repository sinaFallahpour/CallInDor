import React from "react";
import {
  Card,
  CardBody,
  Input,
  Button,
  UncontrolledDropdown,
  DropdownMenu,
  DropdownItem,
  DropdownToggle,
  CardHeader,
} from "reactstrap";
import { ContextLayout } from "../../../utility/context/Layout";
import { ChevronDown, Plus } from "react-feather";

import { AgGridReact } from "ag-grid-react";
import "../../../assets/scss/plugins/tables/_agGridStyleOverride.scss";

import { CheckCircle, XCircle } from "react-feather";
import Swal from "sweetalert2";

import { toast } from "react-toastify";
import { Edit } from "react-feather";
import { NavLink } from "react-router-dom";

import Spinner from "../../../components/@vuexy/spinner/Loading-spinner";
import agent from "../../../core/services/agent";

import { history } from "../../../history";
// import Create from "./_____Create";

import FormModal from "./FormModal";

class Table extends React.Component {
  state = {
    currentData: null,
    currentDataLoading: false,
    addNew: true,

    modal: false,


    services: [],

    loading: true,
    rowData: [],
    paginationPageSize: 20,
    currenPageSize: "",
    getPageSize: "",
    defaultColDef: {
      sortable: true,
      editable: false,
      resizable: true,
      suppressMenu: true,
    },
    columnDefs: [
      {
        headerName: "English Title",
        field: "englishTitle",
        filter: true,
      },
      {
        headerName: "Persian Title",
        field: "persianTitle",
        filter: true,
      },

      {
        headerName: "Code",
        field: "code",
        filter: true,
      },
      {
        headerName: "service Name",
        field: "serviceName",
        filter: true,
      },

      {
        headerName: "ExpireTime",
        field: "expireTime",
        filter: true,
      },

      {
        headerName: "Percent",
        field: "percent",
        filter: true,
        cellRendererFramework: (params) => {
          return (
            <>{params.data.percent}%</>
          );
        },
      },
      {
        headerName: "",
        field: "id",
        filter: false,
        cellStyle: { "border-width": "0px", outline: "none" },
        cellRendererFramework: (params) => {
          return (
            <Edit
              className="mr-50"
              size={15}
              onClick={async () => {
                // window.scrollTo(0, 0);
                await this.populatingCurrentData(params.data.id);
              }}

            // onClick={() => history.push(`/pages/Areas/${params.value}`)}
            />
          );
        },
      },
    ],
  };

  // async populatingServiceTypes() {
  //   const { data } = await agent.ServiceTypes.list();
  //   let services = data.result.data;
  //   this.setState({ services });
  // }

  populateSerevies = async () => {
    this.returnLoading("Loading ");
    const { data } = await agent.ServiceTypes.list();
    this.setState({ services: data.result.data });
    Swal.close();
  };

  async populatingCurrentData(ida) {
    this.returnLoading("Loading ");
    this.setState({ currentDataLoading: true, addNew: false });
    this.populateSerevies();
    try {
      const { data } = await agent.CheckDiscount.details(ida);
      let {
        id,
        persianTitle,
        englishTitle,
        code,
        percent,
        dayCount,
        hourCount,

        serviceId,
        serviceName,
      } = data.result.data;

      const currentData = {
        id,
        persianTitle,
        englishTitle,
        code,
        percent,
        dayCount,
        hourCount,

        serviceId,
        serviceName,
      };
      this.setState({
        currentData,
      });
      this.toggleModal();
    } catch (ex) {
      console.log(ex);
      if (ex?.response?.status == 404 || ex?.response?.status == 400) {
        return history.replace("/not-found");
      }
    } finally {
      Swal.close();
    }
  }

  returnLoading = (title) => {
    Swal.fire({
      title: title,
      allowEnterKey: false,
      allowEscapeKey: false,
      allowOutsideClick: false,
    });
    Swal.showLoading();
  };

  async componentDidMount() {
    const { data } = await agent.CheckDiscount.list();

    if (data?.result) {
      this.setState({ rowData: data.result.data, loading: false });
      return;
    }

    toast.error(data.message, {
      autoClose: 10000,
    });
  }

  toggleModal = () => {
    this.setState((prevState) => ({
      modal: !prevState.modal,
    }));
  };

  handleSidebar = (boolean, addNew = false) => {
    this.populateSerevies();
    this.setState({ modal: boolean });
    if (addNew === true) this.setState({ currentData: null, addNew: true });
  };

  handleSidebar = (boolean, addNew = false, loadData = true) => {
    if (loadData == true) {
      this.populateSerevies();
    }
    this.setState({ modal: boolean });
    if (addNew === true) this.setState({ currentData: null, addNew: true });
  };

  addToDisCount = async (newDisCount) => {
    const rowData = [...this.state.rowData, newDisCount];
    this.setState({ rowData });
  };

  editToDisCount = async (newDisCount) => {
    console.log(newDisCount);
    let rowData = [...this.state.rowData];
    let index = rowData.findIndex(
      (el) => el.id == newDisCount.id /* condition */
    );
    rowData[index] = newDisCount;
    this.setState({ rowData });

    // area = this.state.rowData.find((c) => c.id == newArea.id);

    // const rowData = [...this.state.rowData, newArea];
    // this.setState({ rowData });
  };

  onGridReady = (params) => {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
    this.setState({
      currenPageSize: this.gridApi.paginationGetCurrentPage() + 1,
      getPageSize: this.gridApi.paginationGetPageSize(),
      totalPages: this.gridApi.paginationGetTotalPages(),
    });
    params.api.sizeColumnsToFit();
  };

  updateSearchQuery = (val) => {
    this.gridApi.setQuickFilter(val);
  };

  filterSize = (val) => {
    if (this.gridApi) {
      this.gridApi.paginationSetPageSize(Number(val));
      this.setState({
        currenPageSize: val,
        getPageSize: val,
      });
    }
  };

  // handledeletecurrentQuestion = () => {
  //   this.setState({ addNew: true, currentQuestion: null });
  // };

  render() {
    const {
      rowData,
      columnDefs,
      defaultColDef,
      currentQuestion,
      addNew,
    } = this.state;
    return (
      <React.Fragment>
        {/* <Create
          addToQuestions={this.addToQuestions}
          editToQuestions={this.editToQuestions}
          currentQuestion={currentQuestion}
          addNew={addNew}
        ></Create> */}

        <Card className="overflow-hidden agGrid-card">
          <CardHeader>
            <Button
              className="add-new-btn mb-2"
              color="primary"
              // onClick={this.toggleModal}
              onClick={() => {
                this.handleSidebar(true, true);
              }}
              // handleSidebar = (boolean, addNew = false) => {
              //   this.setState({ modal: boolean });
              //   if (addNew === true) this.setState({ currentData: null, addNew: true });
              // };

              outline
            >
              <Plus size={15} />
              <span className="align-middle">Add New</span>
            </Button>
          </CardHeader>

          {/* <CardHeader>
            <Button.Ripple
              color="primary"
              onClick={this.handledeletecurrentQuestion}
            >
              Create New Area
            </Button.Ripple>
          </CardHeader> */}

          <CardBody className="py-0">
            {this.state.rowData === null ? null : (
              <div className="ag-theme-material w-100 my-2 ag-grid-table">
                <div className="d-flex flex-wrap justify-content-between align-items-center">
                  <div className="mb-1">
                    <UncontrolledDropdown className="p-1 ag-dropdown">
                      <DropdownToggle tag="div">
                        {this.gridApi
                          ? this.state.currenPageSize
                          : "" * this.state.getPageSize -
                          (this.state.getPageSize - 1)}{" "}
                        -{" "}
                        {this.state.rowData.length -
                          this.state.currenPageSize * this.state.getPageSize >
                          0
                          ? this.state.currenPageSize * this.state.getPageSize
                          : this.state.rowData.length}
                        of {this.state.rowData.length}
                        <ChevronDown className="ml-50" size={15} />
                      </DropdownToggle>
                      <DropdownMenu right>
                        <DropdownItem
                          tag="div"
                          onClick={() => this.filterSize(20)}
                        >
                          20
                        </DropdownItem>
                        <DropdownItem
                          tag="div"
                          onClick={() => this.filterSize(50)}
                        >
                          50
                        </DropdownItem>
                        <DropdownItem
                          tag="div"
                          onClick={() => this.filterSize(100)}
                        >
                          100
                        </DropdownItem>
                        <DropdownItem
                          tag="div"
                          onClick={() => this.filterSize(134)}
                        >
                          134
                        </DropdownItem>
                      </DropdownMenu>
                    </UncontrolledDropdown>
                  </div>
                  <div className="d-flex flex-wrap justify-content-between mb-1">
                    <div className="table-input mr-1">
                      <Input
                        placeholder="search..."
                        onChange={(e) => this.updateSearchQuery(e.target.value)}
                        value={this.state.value}
                        disabled={this.state.loading}
                      />
                    </div>
                    {/* <div className="export-btn">
                      <Button.Ripple
                        color="primary"
                        onClick={() => this.gridApi.exportDataAsCsv()}
                      >
                        Export as CSV
                      </Button.Ripple>
                    </div> */}

                    {/* <div className="export-btn">
                      <Button.Ripple
                        color="primary"
                        // onClick={() => this.gridApi.exportDataAsCsv()}
                      >
                      Add New Category
                      </Button.Ripple>
                    </div> */}
                  </div>
                </div>
                {this.state.loading ? (
                  <Spinner></Spinner>
                ) : (
                    <ContextLayout.Consumer>
                      {(context) => (
                        <AgGridReact
                          gridOptions={{}}
                          // rowSelection="multiple"
                          defaultColDef={defaultColDef}
                          columnDefs={columnDefs}
                          rowData={rowData}
                          onGridReady={this.onGridReady}
                          colResizeDefault={"shift"}
                          animateRows={true}
                          floatingFilter={true}
                          pagination={true}
                          paginationPageSize={this.state.paginationPageSize}
                          pivotPanelShow="always"
                          enableRtl={context.state.direction === "rtl"}
                        />
                      )}
                    </ContextLayout.Consumer>
                  )}
              </div>
            )}
          </CardBody>
        </Card>

        <FormModal
          editToDisCount={this.editToDisCount}
          addToDisCount={this.addToDisCount}
          toggleModal={this.toggleModal}
          currentData={this.state.currentData}
          modal={this.state.modal}
          handleSidebar={this.handleSidebar}
          services={this.state.services}
          addNew={this.state.addNew}
        />
      </React.Fragment>
    );
  }
}
export default Table;
