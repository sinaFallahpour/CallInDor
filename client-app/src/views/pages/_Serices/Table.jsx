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

import { toast } from "react-toastify";

import { Edit } from "react-feather";

import { NavLink } from "react-router-dom";
import Spinner from "../../../components/@vuexy/spinner/Loading-spinner";
import agent from "../../../core/services/agent";
import { history } from "../../../history";

class Table extends React.Component {
  state = {
    currentCategory: null,
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
        headerName: "Name",
        field: "name",
        filter: true,
      },
      {
        headerName: "PersianName",
        field: "persianName",
        filter: true,
      },
      {
        headerName: "Minimm Price For Service($)",
        field: "minPriceForService",
        filter: true,
      },
      {
        headerName: "Minimm Session Time(minutes)",
        field: "minSessionTime",
        filter: true,
      },
      {
        headerName: "Minimm Price For Native User (For Chat,Voice,video)$ ",
        field: "acceptedMinPriceForNative",
        filter: true,
      },
      {
        headerName: "Minimm Price For Non Native User (For Chat,Voice,video)$",
        field: "acceptedMinPriceForNonNative",
        filter: true,
      },
      {
        headerName: "Color",
        field: "color",
        filter: true,
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
              onClick={() => history.push(`/pages/Services/${params.value}`)}
            />
          );
        },
      },
    ],
  };

  async componentDidMount() {
    const { data } = await agent.ServiceTypes.GetAll();

    if (data?.result) {
      setTimeout(() => {
        this.setState({ rowData: data.result.data, loading: false });
      }, 600);
      return;
    }
    toast.error(data.message, {
      autoClose: 10000,
    });
  }

  GetAllCategory = async (newCategory) => {
    const rowData = [...this.state.rowData, newCategory];
    this.setState({ rowData });
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

  render() {
    const { rowData, columnDefs, defaultColDef } = this.state;
    return (
      <React.Fragment>
        <Card className="overflow-hidden agGrid-card">
          <CardHeader>
            <Button.Ripple
              color="primary"
              onClick={() => history.push("/pages/Services/Create")}
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
