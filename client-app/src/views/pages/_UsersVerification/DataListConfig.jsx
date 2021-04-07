import React, { Component } from "react";
import {
  Button,
  UncontrolledDropdown,
  DropdownMenu,
  DropdownToggle,
  DropdownItem,
  Input,
  Alert,
} from "reactstrap";
import DataTable from "react-data-table-component";
import classnames from "classnames";
import ReactPaginate from "react-paginate";
import { history } from "../../../history";
import {
  Edit,
  Trash,
  ChevronDown,
  Plus,
  Check,
  ChevronLeft,
  ChevronRight,
} from "react-feather";
import { connect } from "react-redux";
// import {
//   getData,
//   getInitialData,
//   deleteData,
//   updateData,
//   addData,
//   filterData,
// } from "../../../redux/actions/data-list";
import Sidebar from "./DataListSidebar";
// import Chip from "../../../components/@vuexy/chips/ChipComponent";
import Checkbox from "../../../components/@vuexy/checkbox/CheckboxesVuexy";

import "../../../assets/scss/plugins/extensions/react-paginate.scss";
import "../../../assets/scss/pages/data-list.scss";

import { toast } from "react-toastify";
import agent from "../../../core/services/agent";

import profile from "../../../assets/img/_custom/user-profile.png"

import { baseUrl } from "../../../core/services/agent";


// import Spinner from "../../../components/@vuexy/spinner/Loading-spinner";

import Swal from "sweetalert2";

// const chipColors = {
//   "on hold": "warning",
//   delivered: "success",
//   pending: "primary",
//   canceled: "danger"
// }

const chipColors = {
  1: "warning",
  2: "success",
  3: "primary",
  4: "danger",
};

const selectedStyle = {
  rows: {
    selectedHighlighStyle: {
      backgroundColor: "rgba(115,103,240,.05)",
      color: "#7367F0 !important",
      boxShadow: "0 0 1px 0 #7367F0 !important",
      "&:hover": {
        transform: "translateY(0px) !important",
      },
    },
  },
};

const ActionsComponent = (props) => {
  return (
    <div className="data-list-action">
      <Edit
        className="cursor-pointer mr-1"
        size={20}
        onClick={() => {
          history.push(`/pages/Users-Details/${props.row.userName}`)
          return props.currentData(props.row);
        }}
      />
      <Trash
        className="cursor-pointer"
        size={20}

        onClick={() => {
          props.deleteRow(props.row);
        }}
      />
    </div>
  );
};

const CustomHeader = (props) => {
  return (
    <div className="data-list-header d-flex justify-content-between flex-wrap">
      <div className="actions-left d-flex flex-wrap">
        {/* <Button
          className="add-new-btn"
          color="primary"
          onClick={() => props.handleSidebar(true, true)}
          outline
        >
          <Plus size={15} />
          <span className="align-middle">Add New</span>
        </Button> */}
      </div>
      <div className="actions-right d-flex flex-wrap mt-sm-0 mt-2">
        <UncontrolledDropdown className="data-list-rows-dropdown mr-1 d-md-block d-none">
          <DropdownToggle color="" className="sort-dropdown">
            <span className="align-middle mx-50">
              row per page {props.rowsPerPage}
              {/* {`${props.index[0]} - ${props.index[1]} of ${props.total}`} */}
            </span>
            <ChevronDown size={15} />
          </DropdownToggle>
          <DropdownMenu tag="div" right>
            <DropdownItem tag="a" onClick={() => props.handleRowsPerPage(10)}>
              10
            </DropdownItem>
            <DropdownItem tag="a" onClick={() => props.handleRowsPerPage(25)}>
              25
            </DropdownItem>
            <DropdownItem tag="a" onClick={() => props.handleRowsPerPage(50)}>
              50
            </DropdownItem>
            <DropdownItem tag="a" onClick={() => props.handleRowsPerPage(100)}>
              100
            </DropdownItem>
          </DropdownMenu>
        </UncontrolledDropdown>
        <div className="filter-section">
          <Input
            type="text"
            onKeyUp={(e) => {
              if (e.keyCode === 13) {
                props.handleFilter(e);
              }
            }}
          />
        </div>
      </div>
    </div>
  );
};

class DataListConfig extends Component {
  state = {
    data: [],
    totalPages: 0,
    currentPage: 0,
    defaultAlert: false,
    confirmAlert: false,
    cancelAlert: false,
    columns: [
      {
        name: "Image",
        selector: "ImageAddress",
        minWidth: "220px",
        cell: (row) => {
          if (row.imageAddress)
            return <img src={baseUrl + row.imageAddress} height="100" alt={row.name} />
          return <img src={profile} height="100" alt={row.name} />
        },
      },
      {
        name: "Full Name",
        selector: "name",
        sortable: true,
        // minWidth: "200px",
        cell: (row) => (
          <p title={row.name + "_" + row?.lastName} className="text-truncate text-bold-500 mb-0">
            {row?.name + " " + row?.lastName}
          </p>
        ),
      },
      {
        name: "Username",
        selector: "userName",
        sortable: true,
        cell: (row) => (
          <p title={row.userName} className="text-truncate text-bold-500 mb-0">
            {row.userName}
          </p>
        ),
      },
      {
        name: "Lock status",
        selector: "isLockOut",
        sortable: true,
        cell: (row) => {
          return (
            <Checkbox
              disabled
              color="primary"
              icon={<Check className="vx-icon" size={16} />}
              label=""
              defaultChecked={row.isLockOut}
            />
          );
        },
      },
      {
        name: "phoneNumber confirmations",
        selector: "phoneNumberConfirmed",
        sortable: false,
        cell: (row) => {
          return (
            <Checkbox
              disabled
              color="primary"
              icon={<Check className="vx-icon" size={16} />}
              label=""
              defaultChecked={row.phoneNumberConfirmed}
            />
          );
        },
      },
      {
        name: "country Code",
        selector: "countryCode",
        sortable: false,
        minWidth: "40px",
        cell: (row) => {
          return (
            <p
              title={row.countryCode}
              className="text-truncate text-bold-500 mb-0"
            >
              {row.countryCode}
            </p>
          );
        },
      },
      {
        name: "Actions",
        sortable: true,
        sortable: false,
        cell: (row) => (
          <ActionsComponent
            row={row}
            getData={this.props.getData}
            parsedFilter={this.props.parsedFilter}
            currentData={this.handleCurrentData}
            deleteRow={this.handleDelete}
          />
        ),
      },
    ],
    allData: [],
    value: "",
    rowsPerPage: 10,

    sidebar: false,
    currentData: null,
    selected: [],
    totalRecords: 0,
    sortIndex: [],
    addNew: "",

    loading: true,
    loadngDelete: false,

    errors: [],
    errorMessage: "",
  };

  async populatingData() {
    this.setState({ loading: true });
    var params = this.axiosParams();
    const { data } = await agent.User.UsersListForVerification(params);
    if (data?.result) {
      await this.setState({
        data: data.result.data.users,
        allData: data.result.data.users,
        totalPages: data.result.data.totalPages,
        loading: false,
      });
      return;
    }

    toast.error(data.message, {
      autoClose: 10000,
    });
  }

  async componentDidMount() {
    this.populatingData();
  }

  async componentDidUpdate(prevProps, prevState) { }

  handleFilter = async (e) => {
    if (this.state.value == "" && e.target.value == "") return;
    await this.setState({
      loading: true,
      value: e.target.value,
      currentPage: 0,
    });
    this.populatingData();
  };

  axiosParams = () => {
    let { value, rowsPerPage, currentPage } = this.state;
    var params = new URLSearchParams();
    if (!rowsPerPage) rowsPerPage = 10;
    if (!currentPage) currentPage = 0;
    params.append("page", currentPage.toString());
    params.append("perPage", rowsPerPage.toString());
    params.append("searchedWord", value.toString());
    return params;
  };

  handleRowsPerPage = async (value) => {
    await this.setState({ rowsPerPage: value, currentPage: 0, loading: true });
    this.populatingData();
  };

  handleSidebar = (boolean, addNew = false) => {
    this.setState({ sidebar: boolean });
    if (addNew === true) this.setState({ currentData: null, addNew: true });
  };

  // data: data.result.data.users,
  // allData: data.result.data.users,

  handleDelete = async (row) => {
    this.setState({ loadngDelete: true });

    Swal.fire({
      title: "Delete!",
      text: "Are you sure?",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes,it locks",
      cancelButtonText: "Cancellation",
    }).then(async (result) => {
      if (result.value) {
        try {
          const { data } = await agent.User.lockedUser(row.userName);
          if (data.result.status) {
            // this.handleSetNewData(row);

            let datas = [...this.state.data];
            let index = datas.findIndex(el => el.userName == row.userName);

            if (data.result.data) {
              datas[index].isLockOut = true;
              this.setState({ data: [] });
              this.setState({ data: datas });
              Swal.fire("Successful locking ", "User Successfully Locked", "success");
            }
            else {
              datas[index].isLockOut = false;
              this.setState({ data: [] });
              this.setState({ data: datas });
              Swal.fire("Successful un locking ", "User Successfully un Locked", "success")
            }


          }
        } catch (ex) {
          // this.handleCatch(ex);
        }
        setTimeout(() => {
          this.setState({ loadngDelete: false });
        }, 1000);
      }
    });
  };



  handleCatch = (ex) => {
    // console.log(ex);
    // if (ex?.response?.status == 400) {
    //   const errors = ex?.response?.data?.errors;
    //   this.setState({ errors });
    // } else if (ex?.response) {
    //   const errorMessage = ex?.response?.data?.message;
    //   this.setState({ errorMessage });
    //   toast.error(errorMessage, {
    //     autoClose: 10000,
    //   });
    // }
  };

  handleCurrentData = (obj) => {
    console.log(obj);
    this.setState({ currentData: obj });
    this.handleSidebar(true);
  };

  handlePagination = async (page) => {
    await this.setState({ loading: true, currentPage: page.selected });
    this.populatingData();
  };

  // handleDelete = (state, value) => {
  //   this.setState({ [state]: value })
  // }

  render() {
    let {
      columns,
      data,
      allData,
      totalPages,
      value,
      rowsPerPage,
      currentData,
      currentPage,
      sidebar,
      totalRecords,
      sortIndex,
      errorMessage,
      errors,
    } = this.state;

    // const { errorMessage, errors } = this.state;

    return (
      <>
        <div
          className={`data-list ${this.props.thumbView ? "thumb-view" : "list-view"
            }`}
        >
          {/* {errors &&
            errors.map((err, index) => {
              return (
                <Alert key={index} className="text-center" color="danger ">
                  {err}
                </Alert>
              );
            })} */}
          <DataTable
            progressPending={this.state.loading}
            columns={columns}
            data={value.length ? allData : data}
            pagination
            paginationServer
            paginationComponent={() => (
              <ReactPaginate
                previousLabel={<ChevronLeft size={15} />}
                nextLabel={<ChevronRight size={15} />}
                breakLabel="..."
                breakClassName="break-me"
                pageCount={totalPages}
                containerClassName="vx-pagination separated-pagination pagination-end pagination-sm mb-0 mt-2"
                activeClassName="active"
                // forcePage={
                //   this.props.parsedFilter.page
                //     ? parseInt(this.props.parsedFilter.page - 1)
                //     : 0
                // }
                forcePage={currentPage}
                onPageChange={(page) => this.handlePagination(page)}
              />
            )}
            noHeader
            subHeader
            selectableRows
            responsive
            pointerOnHover
            selectableRowsHighlight
            onSelectedRowsChange={(data) =>
              this.setState({ selected: data.selectedRows })
            }
            customStyles={selectedStyle}
            subHeaderComponent={
              <CustomHeader
                handleSidebar={this.handleSidebar}
                handleFilter={this.handleFilter}
                handleRowsPerPage={this.handleRowsPerPage}
                rowsPerPage={rowsPerPage}
                total={totalRecords}
                index={sortIndex}
              />
            }
            sortIcon={<ChevronDown />}
            selectableRowsComponent={Checkbox}
            selectableRowsComponentProps={{
              color: "primary",
              icon: <Check className="vx-icon" size={12} />,
              label: "",
              size: "sm",
            }}
          />

          <Sidebar
            show={sidebar}
            data={currentData}
            updateData={this.updateData}
            addData={this.props.addData}
            handleSidebar={this.handleSidebar}
            getData={this.props.getData}
            dataParams={this.props.parsedFilter}
            addNew={this.state.addNew}
          />
          <div
            className={classnames("data-list-overlay", {
              show: sidebar,
            })}
            onClick={() => this.handleSidebar(false, true)}
          />
        </div>
      </>
    );
  }
}

export default DataListConfig;
