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
// import axios from "axios";

import "../../../assets/scss/plugins/tables/_agGridStyleOverride.scss";

import Breadcrumbs from "../../../components/@vuexy/breadCrumbs/BreadCrumb";

class AggridTable extends React.Component {
  state = {
    rowData: null,
    paginationPageSize: 20,
    currenPageSize: "",
    getPageSize: "",
    defaultColDef: {
      sortable: true,
      editable: true,
      resizable: true,
      suppressMenu: true,
    },
    columnDefs: [
      {
        headerName: "First Name",
        field: "firstname",
        sortable: false,
        width: 175,
        filter: false,
        pinned: window.innerWidth > 992 ? "left" : false,
        checkboxSelection: true,
        headerCheckboxSelectionFilteredOnly: true,
        headerCheckboxSelection: true,
      },
      {
        headerName: "Last Name",
        field: "lastname",
        filter: true,
        width: 175,
      },
      {
        headerName: "Email",
        field: "email",
        filter: true,
        width: 250,
        pinned: window.innerWidth > 992 ? "left" : false,
      },
      {
        headerName: "Company",
        field: "company",
        filter: true,
        width: 250,
      },
      {
        headerName: "City",
        field: "city",
        filter: true,
        width: 150,
      },
      {
        headerName: "Country",
        field: "country",
        filter: true,
        width: 150,
      },
      {
        headerName: "State",
        field: "state",
        filter: true,
        width: 125
      },
      {
        headerName: "Zip",
        field: "zip",
        filter: "agNumberColumnFilter",
        width: 140,
      },
      {
        headerName: "Followers",
        field: "followers",
        filter: "agNumberColumnFilter",
        width: 140,
      },
    ],
  };

  componentDidMount() {
    // axios.get("/api/aggrid/data").then((response) => {
    //   let rowData = response.data.data;
    //   JSON.stringify(rowData);
    //   this.setState({ rowData });
    // });

    const rowData = [
      {
        firstname: "Lana",
        lastname: "Garrigus",
        company: "Russell Builders & Hardware",
        address: "118 Ne 3rd St",
        city: "McMinnville",
        country: "Yamhill",
        state: "OR",
        zip: "97128",
        phone: "503-434-2642",
        fax: "503-434-8121",
        email: "lana@garrigus.com",
        web: "http://www.lanagarrigus.com",
        followers: 3048,
      },
      {
        firstname: "Jonathon",
        lastname: "Waldall",
        company: "Mission Hills Escrow",
        address: "300 Hampton St",
        city: "Walterboro",
        country: "Colleton",
        state: "SC",
        zip: "29488",
        phone: "843-549-9461",
        fax: "843-549-0125",
        email: "jonathon@waldall.com",
        web: "http://www.jonathonwaldall.com",
        followers: 8039,
      },
      {
        firstname: "Kristine",
        lastname: "Paker",
        company: "Chagrin Valley Massotherapy",
        address: "301 N Pine St",
        city: "Creston",
        country: "Union",
        state: "IA",
        zip: "50801",
        phone: "641-782-7169",
        fax: "641-782-7962",
        email: "kristine@paker.com",
        web: "http://www.kristinepaker.com",
        followers: 7977,
      },
    ];


    this.setState({rowData})
  }

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
        <Breadcrumbs
          breadCrumbTitle="Aggrid Table"
          breadCrumbParent="Forms & Tables"
          breadCrumbActive="Aggrid Table"
        />

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
                      />
                    </div>
                    <div className="export-btn">
                      <Button.Ripple
                        color="primary"
                        onClick={() => this.gridApi.exportDataAsCsv()}
                      >
                        Export as CSV
                      </Button.Ripple>
                    </div>
                  </div>
                </div>
                <ContextLayout.Consumer>
                  {(context) => (
                    <AgGridReact
                      gridOptions={{}}
                      rowSelection="multiple"
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
              </div>
            )}
          </CardBody>
        </Card>
      </React.Fragment>
    );
  }
}
export default AggridTable;
