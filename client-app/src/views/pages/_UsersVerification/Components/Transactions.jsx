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

class Transactions extends React.Component {
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
                headerName: "amount",
                field: "amount",
                filter: true,
                // width: 200,
                cellRenderer: function (params) {
                    return `<p title='${params.data.amount}'>${params.data.amount}</p> `
                },
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
            {
                headerName: "transactionConfirmedStatus",
                field: "transactionConfirmedStatus",
                filter: true,
                cellRendererFramework: (params) => {
                    if (params.value == 0) {
                        return <div className="badge badge-pill badge-light-success">Confirmed</div>
                    }
                    else if (params.value == 1) {
                        return <div className="badge badge-pill badge-light-success">Rejected</div>
                    }
                    else if (params.value == 2) {
                        return <div className="badge badge-pill badge-light-success">Pending</div>
                    }
                },
            },

            //title={row.username}

            //username,amount,createDate,
            //transactionConfirmedStatus__,
            //transactionStatus__,transactionType,serviceTypeWithDetails,providerUserName,clientUserName


            {
                headerName: "transactionStatus",
                field: "transactionStatus",
                filter: true,
                cellRendererFramework: (params) => {
                    if (params.value == 0) {
                        return (<span className="badge badge-primary">Complete Profile Transaction</span>)
                    }
                    else if (params.value == 1) {
                        return (<span className="badge badge-success">Normal Transaction</span>)
                    }
                    else if (params.value == 2) {
                        return (<span className="badge badge-light">Service transaction</span>)
                    }

                    else { return <> _</> }
                },
            },
            {
                headerName: "transaction Type",
                field: "transactionType",
                filter: true,
                cellRendererFramework: (params) => {
                    if (params.value == 0) {
                        return (<div className="badge badge-pill badge-light-success">WhiteDrawl</div>)
                    }
                    else if (params.value == 1) {
                        return (<div className="badge badge-pill badge-light-warning">Deposit</div>)
                    }
                    else { return <> _</> }

                },
            },
            {
                headerName: "service Type",
                field: "serviceTypeWithDetails",
                filter: true,
                cellRendererFramework: (params) => {
                    if (params.value?.includes("0")) {
                        return (<button type="button" className="btn btn-sm btn-primary">Chat-Voice </button>)
                    }
                    else if (params.value?.includes("1")) {
                        return (<button type="button" className="btn  btn-sm btn-success"> Video-Call   </button>)
                    }
                    else if (params.value?.includes("2")) {
                        return (<button type="button" className="btn  btn-sm btn-warning">Voice-Call</button>)
                    }
                    else if (params.value?.includes("3")) {
                        return (<button type="button" className="btn  btn-sm btn-danger">Service</button>)
                    }
                    else if (params.value?.includes("4")) {
                        return (<button type="button" className="btn  btn-sm btn-secondary">Course</button>)
                    }
                    else { return <> _</> }

                },
            },
            {
                headerName: "provider UserName",
                field: "provider UserName",
                filter: true,
                // width: 200,
                cellRenderer: function (params) {
                    if (!params.data.providerUserName)
                        return <>_</>
                    return `<p title='${params.data.providerUserName}'>${params.data.providerUserName}</p> `
                },
            },
            {
                headerName: "client UserName",
                field: "clientUserName",
                filter: true,
                // width: 200,
                cellRenderer: function (params) {
                    if (!params.data.clientUserName)
                        return <>_</>
                    return `<p title='${params.data.clientUserName}'>${params.data.clientUserName}</p> `
                },
            },

        ],


    };

    // async componentDidMount() {
    //     const { data } = await agent.Category.listCategory();

    //     if (data?.result) {
    //         setTimeout(() => {
    //             this.setState({ rowData: data.result.data, loading: false });
    //         }, 600);
    //         return;
    //     }
    //     toast.error(data.message, {
    //         autoClose: 10000,
    //     });
    // }

    // GetAllCategory = async (newCategory) => {
    //     const rowData = [...this.state.rowData, newCategory];
    //     this.setState({ rowData });
    // };

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
                    <h4 className="text-center mt-4"> User Transactions </h4>
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
export default Transactions;
