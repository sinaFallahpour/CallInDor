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
    currentUser: null,
    currentUserLoading: false,
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
        headerName: "Email",
        field: "email",
        filter: true,
      },
      {
        headerName: "Name",
        field: "name",
        filter: true,
      },
      {
        headerName: "Last Name",
        field: "lastName",
        filter: true,
      },
      {
        headerName: "Role",
        field: "roleName",
        filter: true,
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
                window.scrollTo(0, 0)
                await this.populatingUser(params.data.id)
              }}

            // onClick={() => history.push(`/pages/Areas/${params.value}`)}
            />
          );
        },
      },
    ],
  };

  async populatingUser(userId) {
    this.setState({ currentUserLoading: true, addNew: false });
    try {
      const { data } = await agent.User.details(userId);
      let {
        id,
        email,
        name,
        lastName,
        roleId,
        roleName
        // serviceId,
        // specialities,
      } = data.result.data;

      const currentUser = {
        data: { id, email, name, lastName, roleId, roleName },
      };
      this.setState({ currentUser });
    } catch (ex) {
      if (ex?.response?.status == 404 || ex?.response?.status == 400) {
        return history.replace("/not-found");
      }
    }
  }


  async componentDidMount() {
    const { data } = await agent.User.list();

    if (data?.result) {
      this.setState({ rowData: data.result.data, loading: false });
      return;
    }

    toast.error(data.message, {
      autoClose: 10000,
    });
  }

  addToUsers = async (newUser) => {
    const rowData = [...this.state.rowData, newUser];
    this.setState({ rowData });
  };

  editToUsers = async (newUser) => {
    let rowData = [...this.state.rowData];
    console.clear()
    console.log(newUser)
    let index = rowData.findIndex((el) => el.id == newUser.id /* condition */);
    rowData[index] = newUser;
    console.log(rowData)
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

  handledeleteCurrentUser = () => {
    this.setState({ addNew: true, currentUser: null });
  };

  render() {
    const {
      rowData,
      columnDefs,
      defaultColDef,
      currentUser,
      addNew,
    } = this.state;
    return (
      <React.Fragment>
        <Create
          addToUsers={this.addToUsers}
          editToUsers={this.editToUsers}
          currentUser={currentUser}
          addNew={addNew}
        ></Create>

        <Card className="overflow-hidden agGrid-card">
          <CardHeader>
            <Button.Ripple
              color="primary"
              onClick={this.handledeleteCurrentUser}
            >
              Create New User
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
