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
} from "reactstrap";
import { AgGridReact } from "ag-grid-react";
import { ContextLayout } from "../../../utility/context/Layout";
import { ChevronDown } from "react-feather";
import "../../../assets/scss/plugins/tables/_agGridStyleOverride.scss";
import { toast } from "react-toastify";

import ModalForm from "./ModalForm";
// import ModalEditForm from "./ModalEditForm";

import Spinner from "../../../components/@vuexy/spinner/Loading-spinner";
import agent from "../../../core/services/agent";



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
      // {
      //   headerName: "First Name",
      //   field: "isEnabled",
      //   sortable: false,
      //   width: 300,
      //   filter: false,
      //   checkboxSelection: true,
      //   headerCheckboxSelectionFilteredOnly: true,
      //   headerCheckboxSelection: true,
      // },
      {
        headerName: "Title",
        field: "title",
        filter: true,
        width: 200,
      },
      {
        headerName: "PersianTitle",
        field: "persianTitle",
        filter: true,
        width: 200,
      },
      {
        headerName: "Parent Name ",
        field: "parentName",
        filter: true,
        width: 200,

        cellRenderer: function (params) {
          if (!params.data) return '';
          if (!params.value)
            return `_ `;
          else return params.value
        }
      },
      {
        headerName: "ServiceName",
        field: "serviceName",
        filter: true,
        width: 200,
      },
      {
        headerName: "",
        field: "id",
        filter: false,
        width: 210,
        cellStyle: { "border-width": "0px", outline: 'none' },
        cellRenderer: function (params) {
          console.log(params)
          if (!params.data) return '';
          return  `<a class="btn btn-success " href="/pages/category/${params.value}">Edit</a>`
          // `<button class="btn btn-sm btn-success">Edit</button>`;
        }
      },

    ],
  };

  async componentDidMount() {
    const { data } = await agent.Category.list();

    if (data?.result) {
      setTimeout(() => {
        this.setState({ rowData: data.result.data, loading: false });
      }, 2000);
      return;
    }
    toast.error(data.message, {
      autoClose: 10000,
    });
  }

  GetAllCategory = async (newCategory) => {
    const rowData = [...this.state.rowData, newCategory];
    this.setState({ rowData })
  };


  onGridReady = (params) => {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
    this.setState({
      currenPageSize: this.gridApi.paginationGetCurrentPage() + 1,
      getPageSize: this.gridApi.paginationGetPageSize(),
      totalPages: this.gridApi.paginationGetTotalPages(),
    });
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
        {/* {this.state.currentCategory!=null? <ModalEditForm /> :null  } */}
        {/* <ModalEditForm/> */}
        <ModalForm GetAllCategory={this.GetAllCategory}></ModalForm>
        <Card className="overflow-hidden agGrid-card">
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
                          : this.state.rowData.length}{" "}
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
