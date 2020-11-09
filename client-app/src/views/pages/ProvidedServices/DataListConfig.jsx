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
import Flatpickr from "react-flatpickr";

import "flatpickr/dist/themes/light.css";
import "../../../assets/scss/plugins/forms/flatpickr/flatpickr.scss"



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

import Sidebar from "./DataListSidebar";
// import Chip from "../../../components/@vuexy/chips/ChipComponent";
import Checkbox from "../../../components/@vuexy/checkbox/CheckboxesVuexy";

import "../../../assets/scss/plugins/extensions/react-paginate.scss";
import "../../../assets/scss/pages/data-list.scss";

import { toast } from "react-toastify";
import agent from "../../../core/services/agent";
import { combineDateAndTime2 } from "../../../core/main";
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


// <td class="text-right" style="vertical-align:middle">

// <button type="button" class="text-white btn btn-sm btn-success" id="read-45" onclick="ToggleServiceReaded(45)" ;="">
//     <i class="">بررسی شد</i>
// </button>


// <button type="button" class="text-white btn btn-sm btn-success" id="confirm-45" onclick="ToggleConfirmService('45', 'rezamm')">
//     <i class="">تایید شد</i>
// </button>

// <a type="button" id="details-45" class="text-white btn btn-sm btn-info" href="/admin/Services/Details/45" dideo-checked="true">
// <i class="">جزییات</i>
// </a>


// </td >

const ActionsComponent = (props) => {

  return (
    <div className="data-list-action">
      {/* <Edit
        className="cursor-pointer mr-1"
        size={20}
        onClick={() => {
          return props.currentData(props.row);
        }}
      />
      <Trash
        className="cursor-pointer"
        size={20}
        onClick={() => {
          props.deleteRow(props.row);
        }}
      /> */}
      <button
        onClick={() => {
          if (props.row?.confirmedServiceType == 0)
            return
          props.handleAcceptService(props.row)
        }}
        type="button"
        class="text-white btn  btn-success"
        id="read-45"
      >
        <i class=""> accept </i>
      </button>

      <button
        onClick={() => {
          if (props.row?.confirmedServiceType == 1)
            return
          props.handleRejectService(props.row)
        }}
        type="button" class="text-white btn  btn-danger" id="read-45" onclick="ToggleServiceReaded(45)">
        <i class="">reject</i>
      </button>
    </div >
  );
};

const CustomHeader = (props) => {
  return (
    <div className="data-list-header d-flex justify-content-end flex-wrap">

      <div className="actions-right d-flex flex-wrap mt-sm-0 mt-2">
        <UncontrolledDropdown className="data-list-rows-dropdown mr-1 d-md-block">
          <DropdownToggle color="" className="sort-dropdown">
            <span className="align-middle mx-50">
              service :{props.returnServicetype(props.serviceType)}
            </span>
            <ChevronDown size={15} />
          </DropdownToggle>
          <DropdownMenu tag="div" right>
            <DropdownItem tag="a" onClick={() => props.handleServiceType(null)}>

            </DropdownItem>
            <DropdownItem tag="a" onClick={() => props.handleServiceType(0)}>
              Chat-Voice
            </DropdownItem>
            <DropdownItem tag="a" onClick={() => props.handleServiceType(1)}>
              Video-Call
            </DropdownItem>
            <DropdownItem tag="a" onClick={() => props.handleServiceType(2)}>
              Voice-Call
            </DropdownItem>
            <DropdownItem tag="a" onClick={() => props.handleServiceType(3)}>
              Service
            </DropdownItem>
            <DropdownItem tag="a" onClick={() => props.handleServiceType(4)}>
              Course
            </DropdownItem>
          </DropdownMenu>
        </UncontrolledDropdown>
      </div>

      <div className="actions-right d-flex flex-wrap mt-sm-0 mt-2">
        <UncontrolledDropdown className="data-list-rows-dropdown mr-1 d-md-block ">
          <DropdownToggle color="" className="sort-dropdown">
            <span className="align-middle mx-50">
              confirmedServiceType: {props.returnConfirmService(props.confirmedServiceType)}
            </span>
            <ChevronDown size={15} />
          </DropdownToggle>
          <DropdownMenu tag="div" right>
            <DropdownItem tag="a" onClick={() => props.handleConfirmedServiceType(null)}>
            </DropdownItem>

            <DropdownItem tag="a" onClick={() => props.handleConfirmedServiceType(0)}>
              Confirmed
            </DropdownItem>
            <DropdownItem tag="a" onClick={() => props.handleConfirmedServiceType(1)}>
              Rejected
            </DropdownItem>
            <DropdownItem tag="a" onClick={() => props.handleConfirmedServiceType(2)}>
              Pending
            </DropdownItem>

          </DropdownMenu>
        </UncontrolledDropdown>

        <div className="actions-right d-flex flex-wrap mt-sm-0 mt-2">
          <UncontrolledDropdown className="data-list-rows-dropdown mr-1 d-md-block ">
            <DropdownToggle color="" className="sort-dropdown">
              <span className="align-middle mx-50">
                row per page {props.rowsPerPage}
              </span>
              <ChevronDown size={15} />
            </DropdownToggle>
            <DropdownMenu tag="div" right>
              <DropdownItem tag="a" onClick={() => props.handleRowsPerPage(null)}>

              </DropdownItem>

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
        </div>

        <div className="filter-section">
          <Flatpickr
            id="startDate"
            className="form-control"
            value={props.createDate}
            onChange={date => props.handleCreateDate(date)}
            options={{ altInput: true, altFormat: "F j, Y", dateFormat: "Y-m-d", }}
          />


          {/* <Input
            type="text"
            onKeyUp={(e) => {
              if (e.keyCode === 13) {
                props.handleFilter(e);
              }
            }}
          /> */}
        </div>

        <div className="filter-section">
          <Input
            placeholder="search ..."
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
        name: "serviceName",
        selector: "serviceName",
        sortable: true,
        // minWidth: "200px",
        cell: (row) => (
          <p
            title={row.serviceName}
            className="text-truncate text-bold-500 mb-0"
          >
            {row.serviceName}
          </p>
        ),
      },
      {
        name: "serviceType",
        selector: "serviceType",
        sortable: true,
        cell: (row) => {
          return (<p title={row.serviceType} className="text-truncate text-bold-500 mb-0" >
            {this.returnServicetype(row.serviceType)}
          </p>)
        }
      },
      {
        name: "category",
        selector: "serviceTypeName",
        sortable: true,
        cell: (row) => {
          return (<p title={row.serviceTypeName} className="text-truncate text-bold-500 mb-0" >
            {row.serviceTypeName}
          </p>)
        }
      },

      {
        name: "category",
        selector: "confirmedServiceType",
        sortable: true,
        cell: (row) => {
          if (row.confirmedServiceType == 0)
            return (<div className="badge badge-pill badge-light-success">accepted</div>)
          if (row.confirmedServiceType == 1)
            return (<div className="badge badge-pill badge-light-warning">rejected</div>)
          if (row.confirmedServiceType == 2)
            return (<div className="badge badge-pill badge-light-primary">pendding</div>)
          return (<p title={row.confirmedServiceType} className="text-truncate text-bold-500 mb-0" >
            {row.confirmedServiceType}
          </p>)
        }
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
            handleRejectService={this.handleRejectService}
            handleAcceptService={this.handleAcceptService}
          />
        ),
      },
    ],
    allData: [],
    value: "",
    rowsPerPage: 10,
    createDate: null,
    serviceType: null,
    confirmedServiceType: null,



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
    const { data } = await agent.ServiceTypes.getAllProvideServicesInAdmin(params);
    if (data?.result) {
      console.log(data.result.data)
      await this.setState({
        data: data.result.data.providesdService,
        allData: data.result.data.providesdService,
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



  returnServicetype = (serviceType) => {
    if (serviceType == 0)
      return "Chat-Voice"
    if (serviceType == 1)
      return "Video-Call"
    if (serviceType == 2)
      return "Voice-Call"
    if (serviceType == 3)
      return "Service"
    if (serviceType == 4)
      return "Course"
  }

  returnConfirmService = (confirmService) => {
    if (confirmService == 0)
      return "Confirmed"
    if (confirmService == 1)
      return "Rejected"
    if (confirmService == 2)
      return "Pending"
  }

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
    let { value, rowsPerPage, currentPage, createDate, serviceType, confirmedServiceType } = this.state;
    var params = new URLSearchParams();
    if (!rowsPerPage) rowsPerPage = 10;
    if (!currentPage) currentPage = 0;
    params.append("page", currentPage.toString());
    params.append("perPage", rowsPerPage.toString());
    params.append("searchedWord", value.toString());
    if (createDate)
      params.append("createDate", createDate)
    if (serviceType || serviceType == 0) {
      params.append("serviceType", serviceType)
    }
    if (confirmedServiceType || confirmedServiceType == 0) {
      params.append("confirmedServiceType", confirmedServiceType)
    }
    return params;
  };

  handleRowsPerPage = async (value) => {
    await this.setState({ rowsPerPage: value, currentPage: 0, loading: true });
    this.populatingData();
  };

  // rowsPerPage: 10,
  // createDate: null,
  // serviceType: null,
  // confirmedServiceType: null,

  handleServiceType = async (value) => {
    await this.setState({ serviceType: value, currentPage: 0, loading: true });
    this.populatingData();
  };

  handleCreateDate = async (value) => {
    // alert(value.getHours())
    // alert(value)
    // alert(new Date(value))
    // var ds = new Date(value)
    // alert(ds.getHours())

    var createDate = combineDateAndTime2(new Date(value))
    alert(createDate)
    await this.setState({ createDate, currentPage: 0, loading: true });
    this.populatingData();
  };

  handleConfirmedServiceType = async (value) => {
    await this.setState({ confirmedServiceType: value, currentPage: 0, loading: true });
    this.populatingData();
  };


  handleSidebar = (boolean, addNew = false) => {
    this.setState({ sidebar: boolean });
    if (addNew === true) this.setState({ currentData: null, addNew: true });
  };

  // data: data.result.data.users,
  // allData: data.result.data.users,




  // handleAcceptService


  handleRejectService = async (row) => {
    console.log(row);
    // this.setState({ loadngDelete: true });


    Swal.fire({
      title: 'reason for rejecting the service',
      input: 'text',
      inputAttributes: {
        autocapitalize: 'off'
      },
      showCancelButton: true,
      confirmButtonText: 'Reject',
      showLoaderOnConfirm: true,
      preConfirm: async (text) => {
        try {
          const { data } = await agent.ServiceTypes.rejectProvideServicesInAdmin()(row.userName);
          if (data.result.status) {
            // this.handleSetNewData(row);
            Swal.fire(
              "Successful rejecting",
              "service successfully rejected",
              "success"
            );
          }
        } catch (ex) {
          Swal.showValidationMessage(
            `Request failed: ${ex}`
          )
        }
        setTimeout(() => {
          this.setState({ loadngDelete: false });
        }, 1000);




        // return fetch(`//api.github.com/users/${text}`)
        //   .then(response => {
        //     if (!response.ok) {
        //       throw new Error(response.statusText)
        //     }
        //     return response.json()
        //   })
        //   .catch(error => {
        //     Swal.showValidationMessage(
        //       `Request failed: ${error}`
        //     )
        //   })
      },
      allowOutsideClick: () => !Swal.isLoading()
    }).then((result) => {
      // if (result.isConfirmed) {
      //   Swal.fire({
      //     title: `${result.value.login}'s avatar`,
      //     imageUrl: result.value.avatar_url
      //   })
      // }
    })










    // Swal.fire({
    //   title: "Reject service!",
    //   text: "Do you want to reject this Service ?",
    //   icon: "warning",
    //   showCancelButton: true,
    //   confirmButtonColor: "#3085d6",
    //   cancelButtonColor: "#d33",
    //   confirmButtonText: "Yes,reject it",
    //   cancelButtonText: "Cancellation",
    // }).then(async (result) => {
    //   if (result.value) {
    //     try {
    //       const { data } = await agent.ServiceTypes.rejectProvideServicesInAdmin()(row.userName);
    //       if (data.result.status) {
    //         this.handleSetNewData(row);
    //         if (data.result.data)
    //           Swal.fire(
    //             "Successful locking ",
    //             "User Successfully Locked",
    //             "success"
    //           );
    //         else
    //           Swal.fire(
    //             "Successful un locking ",
    //             "User Successfully un Locked",
    //             "success"
    //           );
    //       }
    //     } catch (ex) {
    //       this.handleCatch(ex);
    //     }
    //     setTimeout(() => {
    //       this.setState({ loadngDelete: false });
    //     }, 1000);
    //   }
    // });




  };




  handleAcceptService = async (row) => {
    console.log(row);
    this.setState({ loadngDelete: true });

    Swal.fire({
      title: 'Submit your Github username',
      input: 'text',
      inputAttributes: {
        autocapitalize: 'off'
      },
      showCancelButton: true,
      confirmButtonText: 'Look up',
      showLoaderOnConfirm: true,
      preConfirm: (login) => {
        return fetch(`//api.github.com/users/${login}`)
          .then(response => {
            if (!response.ok) {
              throw new Error(response.statusText)
            }
            return response.json()
          })
          .catch(error => {
            Swal.showValidationMessage(
              `Request failed: ${error}`
            )
          })
      },
      allowOutsideClick: () => !Swal.isLoading()
    }).then((result) => {
      if (result.isConfirmed) {
        Swal.fire({
          title: `${result.value.login}'s avatar`,
          imageUrl: result.value.avatar_url
        })
      }
    })







  };








  handleSetNewData = (row) => {
    this.setState((prevState) => {
      return {
        data: prevState.data.map((el) => {
          if (el.userName == row.userName) {
            return { ...el, isLockOut: !el.isLockOut };
          } else {
            return el;
          }
        }),
      };
    });

    // this.setState((pre) => {
    //   console.log(11)
    //   data: pre.data.map((el) => {
    //     if (el.userName == row.userName) {
    //       return { ...el, isLockOut: !el.isLockOut }
    //     }
    //     else {
    //       return el
    //     }
    //   })
    // })
    // this.setState((prev) => ({ );

    // this.setState({

    //   data: data.filter((el) => {
    //     return el.userName == row.username
    //   ),
    // data: data.map((el) => {
    //   if (el.userName == row.userName) {
    //     return { ...el, isLockOut: !el.isLockOut }
    //   }
    //   else {
    //     return el
    //   }
    // }),
    //   allData: allData.map((el) => {
    //     if (el.userName == row.userName) {
    //       return { ...el, isLockOut: !el.isLockOut }
    //     }
    //     else {
    //       return el
    //     }
    //   }),
    // }, () => {
    //   console.log(this.state.data)
    //   console.log(this.state.allData)
    // });
    Swal.fire("Successful locking ", "User Successfully Locked", "success");

  };

  handleCatch = (ex) => {
    console.log(ex);
    if (ex?.response?.status == 400) {
      const errors = ex?.response?.data?.errors;
      this.setState({ errors });
    } else if (ex?.response) {
      const errorMessage = ex?.response?.data?.message;
      this.setState({ errorMessage });
      toast.error(errorMessage, {
        autoClose: 10000,
      });
    }
  };

  handleCurrentData = (obj) => {
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
      createDate,
      serviceType,
      confirmedServiceType,
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
          {errors &&
            errors.map((err, index) => {
              return (
                <Alert key={index} className="text-center" color="danger ">
                  {err}
                </Alert>
              );
            })}
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
                returnServicetype={this.returnServicetype}
                returnConfirmService={this.returnConfirmService}
                handleFilter={this.handleFilter}
                handleRowsPerPage={this.handleRowsPerPage}
                handleServiceType={this.handleServiceType}
                handleCreateDate={this.handleCreateDate}
                handleConfirmedServiceType={this.handleConfirmedServiceType}

                createDate={createDate}
                serviceType={serviceType}
                confirmedServiceType={confirmedServiceType}
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

// const mapStateToProps = (state) => {
//   return {
//     dataList: state.dataList,
//   };
// };

// export default connect(mapStateToProps, {
//   getData,
//   deleteData,
//   updateData,
//   addData,
//   getInitialData,
//   filterData,
// })(DataListConfig);

export default DataListConfig;
