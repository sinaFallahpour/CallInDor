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



import ChatDetailsModal from "./_ChatDetailsModal";
import ServiceDetailsModal from "./_ServiceDetailsModal";


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
  AlertTriangle,
} from "react-feather";
import { connect } from "react-redux";

// import Sidebar from "./DataListSidebar";
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
//     <i class="">????? ??</i>
// </button>


// <button type="button" class="text-white btn btn-sm btn-success" id="confirm-45" onclick="ToggleConfirmService('45', 'rezamm')">
//     <i class="">????? ??</i>
// </button>

// <a type="button" id="details-45" class="text-white btn btn-sm btn-info" href="/admin/Services/Details/45" dideo-checked="true">
// <i class="">??????</i>
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
          if (props.row?.serviceType == 0 || props.row?.serviceType == 1 || props.row?.serviceType == 2) {

            props.getChatServiceDetails(props.row)
            return
          }
          if (props.row?.serviceType == 3) {
            props.getServiceServiceDetails(props.row)
            return
          }
          if (props.row?.serviceType == 4) {
            return
          }
        }}
        type="button" className="text-white p-1 btn  btn-primary" id="read-45">
        <i className="">details</i>
      </button>



      <button
        onClick={() => {
          if (props.row?.confirmedServiceType == 0)
            return
          props.handleAcceptService(props.row)
        }}
        type="button"
        className="text-white btn p-1 btn-success"
        id="read-45"
      >
        <i className=""> accept </i>
      </button>

      <button
        onClick={() => {
          if (props.row?.confirmedServiceType == 1)
            return
          props.handleRejectService(props.row)
        }}
        type="button" className="text-white btn p-1 btn-danger" id="read-45" >
        <i className="">reject</i>
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

    currenChatServiceData: null,
    currenServiceServiceData: null,
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
        name: "confirmed Service status",
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
            // deleteRow={this.handleDelete}
            handleRejectService={this.handleRejectService}
            handleAcceptService={this.handleAcceptService}
            getChatServiceDetails={this.getChatServiceDetails}
            getServiceServiceDetails={this.getServiceServiceDetails}
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

    modal: false,
    serviceModal: false,

    sidebar: false,
    currentData: null,
    selected: [],
    totalRecords: 0,
    sortIndex: [],
    addNew: "",

    loading: true,
    loadngDelete: false,
    loadingDetails: false,

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

    var createDate = combineDateAndTime2(new Date(value))
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



  getChatServiceDetails = async (row) => {
    this.returnLoading('Loading service');
    const { data } = await agent.ServiceTypes.getChatServiceDetailsInAdmin(row.id);
    if (data?.result) {
      await this.setState({
        currenChatServiceData: data.result.data,
      });
      this.toggleModal()
      Swal.close();
      return;
    }
    Swal.close();
    // Swal.disableLoading()
    toast.error(data.message, {
      autoClose: 10000,
    });
  }






  getServiceServiceDetails = async (row) => {
    this.returnLoading('Loading service');
    const { data } = await agent.ServiceTypes.getServiceServiceDetailsInAdmin(row.id);
    if (data?.result) {
      await this.setState({
        currenServiceServiceData: data.result.data,
      });
      this.toggleServiceModal()
      Swal.close();
      return;
    }
    Swal.close();

    toast.error(data.message, {
      autoClose: 10000,
    });
  }


  returnLoading = (title) => {
    Swal.fire({
      title: title,
      allowEnterKey: false,
      allowEscapeKey: false,
      allowOutsideClick: false,
    });
    Swal.showLoading();

  }

  toggleModal = () => {
    this.setState((prevState) => ({
      modal: !prevState.modal,
    }));
  };


  toggleServiceModal = () => {
    this.setState((prevState) => ({
      serviceModal: !prevState.serviceModal,
    }));

  };




  // handleAcceptService

  handleRejectService = async (row) => {
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
          const obj = { serviceId: row.id, rejectReason: text }
          const { data } = await agent.ServiceTypes.rejectProvideServicesInAdmin(obj);
          console.log(data)
          if (data.result.status) {
            this.setState({
              data: this.state.data.map(el => (el.id === row.id ? Object.assign({}, el, { confirmedServiceType: 1 }) : el))
            });
            // this.handleSetNewData(row);
            Swal.fire(
              "Successful rejecting",
              "service successfully rejected",
              "success"
            );
          }
        } catch (ex) {
          let res = this.handleCatch2(ex)
          Swal.showValidationMessage(
            `${res}`
          )
        }
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

  };



  handleAcceptService = async (row) => {
    // this.setState({ loadngDelete: true });
    Swal.fire({
      title: 'Are you sure?',
      text: "confirm service ",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, consfirm it!'
    }).then(async (result) => {
      if (result.isConfirmed) {
        Swal.fire(
          "loading ...",
          "success"
        );
        try {
          const { data } = await agent.ServiceTypes.acceptProvideServicesInAdmin(row.id);
          if (data.result.status) {
            this.setState({
              data: this.state.data.map(el => (el.id === row.id ? Object.assign({}, el, { confirmedServiceType: 0 }) : el))
            });
            // this.handleSetNewData(row);
            Swal.fire(
              "Successful accepting",
              "service successfully accepted",
              "success"
            );
          }
        } catch (ex) {
          let res = this.handleCatch2(ex)
          Swal.fire(
            `Error!`,
            `${res}`,
            'warning'
          )
        }
        // Swal.showValidationMessage(
        //   `${res}`
        // )
      }

    })

    // Swal.fire(
    //   'Deleted!',
    //   'Your file has been deleted.',
    //   'success'
    // )
  }














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



  handleCatch2 = (ex) => {
    console.log(ex);
    if (ex?.response?.status == 400) {
      const errors = ex?.response?.data?.errors;
      // this.setState({ errors });
      return errors[0];
    } else if (ex?.response) {
      const errorMessage = ex?.response?.data?.message;
      // this.setState({ errorMessage });

      // toast.error(errorMessage, {
      //   autoClose: 10000,
      // });
      return errorMessage
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

          <ChatDetailsModal
            toggleModal={this.toggleModal}
            currenChatServiceData={this.state.currenChatServiceData}
            modal={this.state.modal}
          />



          <ServiceDetailsModal
            toggleServiceModal={this.toggleServiceModal}
            // toggleModal={this.toggleModal}
            currenServiceServiceData={this.state.currenServiceServiceData}
            serviceModal={this.state.serviceModal}
          />



          {/* <Sidebar
            show={sidebar}
            data={currentData}
            updateData={this.updateData}
            addData={this.props.addData}
            handleSidebar={this.handleSidebar}
            getData={this.props.getData}
            dataParams={this.props.parsedFilter}
            addNew={this.state.addNew}
          /> */}
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
