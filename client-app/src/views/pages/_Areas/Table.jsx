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
import { ChevronDown } from "react-feather";

import { AgGridReact } from "ag-grid-react";
import "../../../assets/scss/plugins/tables/_agGridStyleOverride.scss";

import { CheckCircle, XCircle } from "react-feather";

import { toast } from "react-toastify";
import { Edit } from "react-feather";
import { NavLink } from "react-router-dom";

import Spinner from "../../../components/@vuexy/spinner/Loading-spinner";
import agent from "../../../core/services/agent";
import { history } from "../../../history";
import Create from "./Create";

class Table extends React.Component {
  state = {
    currentArea: null,
    currentAreaLoading: false,
    addNew: true,

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
        headerName: "English Name",
        field: "title",
        filter: true,
      },
      {
        headerName: "Persian Name",
        field: "persianTitle",
        filter: true,
      },
      {
        headerName: "service Name",
        field: "serviceName",
        filter: true,
      },
      {
        headerName: "Professional status",
        field: "isProfessional",
        filter: true,
        cellRendererFramework: (params) => {
          console.log(params);
          return params.value === true ? (
            // <input readOnly checked type="check" className="badge badge-pill badge-light-success" />
            <div className=" badge badge-pill badge-light-success">
              <CheckCircle className="text-center" />
            </div>
          ) : (
            <div className="badge badge-pill badge-light-warning">
              <XCircle />
            </div>
          );
        },
      },
      {
        headerName: "status",
        field: "isEnabled",
        filter: true,

        cellRendererFramework: (params) => {
          console.log(params);
          return params.value === true ? (
            <div className="badge badge-pill badge-light-success">Active</div>
          ) : (
            <div className="badge badge-pill badge-light-warning">
              DeActivated
            </div>
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
              onClick={async () => await this.populatingArea(params.data.id)}

              // onClick={() => history.push(`/pages/Areas/${params.value}`)}
            />
          );
        },
      },
    ],
  };

  async populatingArea(areaId) {
    this.setState({ currentAreaLoading: true, addNew: false });
    try {
      const { data } = await agent.Areas.details(areaId);
      let {
        id,
        title,
        persianTitle,
        isEnabled,
        isProfessional,
        serviceId,
        specialities,
      } = data.result.data;

      const currentArea = {
        data: { id, title, persianTitle, serviceId },
        isEnabled,
        isProfessional,
        Specialities: specialities,
      };

      this.setState({ currentArea });
    } catch (ex) {
      if (ex?.response?.status == 404 || ex?.response?.status == 400) {
        return this.props.history.replace("/not-found");
      }
    }
  }

  // async populatingArea() {
  //   const { data } = await agent.Areas.details();
  //   let categories = data.result.data;
  //   this.setState({ categories });
  // }

  async componentDidMount() {
    const { data } = await agent.Areas.list();

    if (data?.result) {
      this.setState({ rowData: data.result.data, loading: false });
      return;
    }

    toast.error(data.message, {
      autoClose: 10000,
    });
  }

  addToAreas = async (newArea) => {
    const rowData = [...this.state.rowData, newArea];
    this.setState({ rowData });
  };

  editToAreas = async (newArea) => {
    let rowData = [...this.state.rowData];
    let index = rowData.findIndex((el) => el.id == newArea.id /* condition */);
    rowData[index] = newArea;
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

  handledeleteCurrentArea = () => {
    this.setState({ addNew: true, currentArea: null });
  };

  render() {
    const {
      rowData,
      columnDefs,
      defaultColDef,
      currentArea,
      addNew,
    } = this.state;
    return (
      <React.Fragment>
        <Create
          addToAreas={this.addToAreas}
          editToAreas={this.editToAreas}
          currentArea={currentArea}
          addNew={addNew}
        ></Create>

        <Card className="overflow-hidden agGrid-card">
          <CardHeader>
            <Button.Ripple
              color="primary"
              onClick={this.handledeleteCurrentArea}
            >
              Create New Service
            </Button.Ripple>
          </CardHeader>

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
      </React.Fragment>
    );
  }
}
export default Table;
