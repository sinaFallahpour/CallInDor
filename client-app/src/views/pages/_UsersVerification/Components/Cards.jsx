import React from "react";
import { NavLink } from "react-router-dom";
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
import { ContextLayout } from "../../../../utility/context/Layout";

import { Edit, Trash2, ChevronDown, Check } from "react-feather";

import Checkbox from "../../../../components/@vuexy/checkbox/CheckboxesVuexy";

import "../../../../assets/scss/plugins/tables/_agGridStyleOverride.scss";
import { toast } from "react-toastify";



import Spinner from "../../../../components/@vuexy/spinner/Loading-spinner";
import agent, { baseUrl } from "../../../../core/services/agent";

import { history } from "../../../../history";

class Cards extends React.Component {
    state = {
        currentCategory: null,
        loading: true,
        // rowData: [],
        paginationPageSize: 20,
        currenPageSize: "",
        getPageSize: "",
        defaultColDef: {
            // minWidth: 600,
            sortable: true,
            editable: false,
            resizable: true,
            suppressMenu: true,
        },


        // title={row.username}

        // username,amount,createDate,
        // transactionConfirmedStatus__,transactionStatus__,transactionType,serviceTypeWithDetails,providerUserName,clientUserName

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
                headerName: "username",
                field: "username",
                filter: true,
                // width: 200,
                cellRenderer: function (params) {
                    return `<p title='${params.data.username}'>${params.data.username}</p> `
                },
            },
            {
                headerName: "Card Name",
                field: "cardName",
                filter: true,
                // width: 200,
                cellRenderer: function (params) {
                    return `<p title='${params.data.cardName}'>${params.data.cardName}</p> `
                },
            },
            {

                headerName: "Card Number",
                field: "cardNumber",
                filter: true,

                cellRenderer: function (params) {
                    return params.value
                },
                // width: 200,
            },

            {
                headerName: "createDate",
                field: "createDate",
                filter: true,
                cellRenderer: function (params) {
                    return params.value
                },
                // width: 200,
            },

        ],


    };

    onGridReady = (params) => {
        this.gridApi = params.api;
        this.gridColumnApi = params.columnApi;
        this.setState({
            currenPageSize: this.gridApi.paginationGetCurrentPage() + 1,
            getPageSize: this.gridApi.paginationGetPageSize(),
            totalPages: this.gridApi.paginationGetTotalPages(),
        });
        // params.api.sizeColumnsToFit();
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
        const { columnDefs, defaultColDef } = this.state;
        const { rowData, loading } = this.props;

        return (
            <React.Fragment>
                <Card className="overflow-hidden agGrid-card">
                    <h4 className="text-center mt-4"> User Cards </h4>
                    <CardBody className="py-0" >
                        {rowData === null ? null : (
                            <div className="ag-theme-material w-100 my-2 ag-grid-table" style={{ minHeight: "600px" }}   >
                                <div className="d-flex flex-wrap justify-content-between align-items-center">
                                    <div className="mb-1">
                                        <UncontrolledDropdown className="p-1 ag-dropdown">
                                            <DropdownToggle tag="div">
                                                {this.gridApi
                                                    ? this.state.currenPageSize
                                                    : "" * this.state.getPageSize -
                                                    (this.state.getPageSize - 1)}{" "}
                                             -{" "}
                                                {rowData.length -
                                                    this.state.currenPageSize * this.state.getPageSize >
                                                    0
                                                    ? this.state.currenPageSize * this.state.getPageSize
                                                    : this.props.rowData.length}{" "}
                                           of {rowData.length}
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
                                                disabled={loading}
                                            />
                                        </div>
                                    </div>
                                </div>
                                { loading ? (
                                    <Spinner></Spinner>
                                ) : (
                                    <ContextLayout.Consumer>
                                        {(context) => (
                                            <AgGridReact
                                                rowHeight={60}
                                                gridOptions={{}}
                                                // rowSelection="multiple"
                                                defaultColDef={defaultColDef}
                                                columnDefs={columnDefs}
                                                rowData={this.props.rowData}
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
export default Cards;
