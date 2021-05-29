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
import "../../../../assets/scss/plugins/forms/flatpickr/flatpickr.scss";

// import ChatDetailsModal from "./_ChatDetailsModal";

import ReactPaginate from "react-paginate";
// import { history } from "../../../history";
import {
  ChevronDown,
  Check,
  ChevronLeft,
  ChevronRight,
  Award,
} from "react-feather";
// import { connect } from "react-redux";

// import Sidebar from "./DataListSidebar";
// import Chip from "../../../components/@vuexy/chips/ChipComponent";
import Checkbox from "../../../../components/@vuexy/checkbox/CheckboxesVuexy";

import "../../../../assets/scss/plugins/extensions/react-paginate.scss";
import "../../../../assets/scss/pages/data-list.scss";

import { toast } from "react-toastify";
import agent from "../../../../core/services/agent";
import { combineDateAndTime2 } from "../../../../core/main";
// import Spinner from "../../../components/@vuexy/spinner/Loading-spinner";
import { Badge } from "reactstrap"

import Swal from "sweetalert2";

import ChatModal from "./ChatModal2";

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
      <button
        onClick={() => {
          props.getTicketsDetails(props.row);
        }}
        type="button"
        className="text-white p-1 btn  btn-primary"
        id="read-45"
      >
        <i className="">messages</i>
      </button>

      <button
        onClick={() => props.handleTicketStatus(props.row)}
        type="button"
        className="text-white btn p-1 btn-success"
        id="read-45"
      >
        <i className="">  ticket status </i>
      </button>
    </div>
  );
};

const CustomHeader = (props) => {
  return (
    <div className="data-list-header d-flex justify-content-end flex-wrap">



      <div className="actions-right d-flex flex-wrap mt-sm-0 mt-2">
        {/* <UncontrolledDropdown className="data-list-rows-dropdown mr-1 d-md-block ">
          <DropdownToggle color="" className="sort-dropdown">
            <span className="align-middle mx-50">
              transaction status:{" "}
              {props.returnTransactionStatus(props.transactionStatus)}
            </span>
            <ChevronDown size={15} />
          </DropdownToggle>
          <DropdownMenu tag="div" right>
            <DropdownItem
              tag="a"
              onClick={() => props.handleTransactionStatus(null)}
            ></DropdownItem>

            <DropdownItem
              tag="a"
              onClick={() => props.handleTransactionStatus(0)}
            >
              {props.returnTransactionStatus(0)}
             </DropdownItem>
            <DropdownItem
              tag="a"
              onClick={() => props.handleTransactionStatus(1)}
            >
              {props.returnTransactionStatus(1)}
             </DropdownItem>

            <DropdownItem
              tag="a"
              onClick={() => props.handleTransactionStatus(2)}
            >
              {props.returnTransactionStatus(2)}
             </DropdownItem>

          </DropdownMenu>
        </UncontrolledDropdown> */}

        <UncontrolledDropdown className="data-list-rows-dropdown mr-1 d-md-block ">
          <DropdownToggle color="" className="sort-dropdown">
            <span className="align-middle mx-50">
              type:{" "}
              {props.returnTransactionType(props.transactionType)}
            </span>
            <ChevronDown size={15} />
          </DropdownToggle>
          <DropdownMenu tag="div" right>
            <DropdownItem
              tag="a"
              onClick={() => props.handleTransactionType(null)}
            ></DropdownItem>

            <DropdownItem
              tag="a"
              onClick={() => props.handleTransactionType(0)}
            >
              {props.returnTransactionType(0)}

            </DropdownItem>
            <DropdownItem
              tag="a"
              onClick={() => props.handleTransactionType(1)}
            >
              {props.returnTransactionType(1)}

            </DropdownItem>
          </DropdownMenu>
        </UncontrolledDropdown>

        <UncontrolledDropdown className="data-list-rows-dropdown mr-1 d-md-block ">
          <DropdownToggle color="" className="sort-dropdown">
            <span className="align-middle mx-50">
              service type :{" "}
              {props.returnServiceTypeWithDetails(props.serviceTypeWithDetails)}
            </span>
            <ChevronDown size={15} />
          </DropdownToggle>
          <DropdownMenu tag="div" right>
            <DropdownItem
              tag="a"
              onClick={() => props.handleServiceTypeWithDetails(null)}
            ></DropdownItem>

            <DropdownItem
              tag="a"
              onClick={() => props.handleServiceTypeWithDetails(0)}
            >
              {props.returnServiceTypeWithDetails(0)}

            </DropdownItem>
            <DropdownItem
              tag="a"
              onClick={() => props.handleServiceTypeWithDetails(1)}
            >
              {props.returnServiceTypeWithDetails(1)}

            </DropdownItem>

            <DropdownItem
              tag="a"
              onClick={() => props.handleServiceTypeWithDetails(2)}
            >
              {props.returnServiceTypeWithDetails(2)}
            </DropdownItem>

            <DropdownItem
              tag="a"
              onClick={() => props.handleServiceTypeWithDetails(3)}
            >
              {props.returnServiceTypeWithDetails(3)}
            </DropdownItem>


            <DropdownItem
              tag="a"
              onClick={() => props.handleServiceTypeWithDetails(4)}
            >
              {props.returnServiceTypeWithDetails(4)}
            </DropdownItem>


          </DropdownMenu>
        </UncontrolledDropdown>

        <UncontrolledDropdown className="data-list-rows-dropdown mr-1 d-md-block ">
          <DropdownToggle color="" className="sort-dropdown">
            <span className="align-middle mx-50">
              confirmed status :{" "}
              {props.returnTransactionConfirmedStatus(props.serviceTypeWithDetails)}
            </span>
            <ChevronDown size={15} />
          </DropdownToggle>
          <DropdownMenu tag="div" right>
            <DropdownItem
              tag="a"
              onClick={() => props.handleTransactionConfirmedStatus(null)}
            ></DropdownItem>

            <DropdownItem
              tag="a"
              onClick={() => props.handleTransactionConfirmedStatus(0)}
            >
              {props.returnTransactionConfirmedStatus(0)}
            </DropdownItem>

            <DropdownItem
              tag="a"
              onClick={() => props.handleTransactionConfirmedStatus(1)}
            >
              {props.returnTransactionConfirmedStatus(1)}
            </DropdownItem>

            <DropdownItem
              tag="a"
              onClick={() => props.handleTransactionConfirmedStatus(2)}
            >
              {props.returnTransactionConfirmedStatus(2)}
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
              <DropdownItem
                tag="a"
                onClick={() => props.handleRowsPerPage(null)}
              ></DropdownItem>

              <DropdownItem tag="a" onClick={() => props.handleRowsPerPage(10)}>
                10
              </DropdownItem>
              <DropdownItem tag="a" onClick={() => props.handleRowsPerPage(25)}>
                25
              </DropdownItem>
              <DropdownItem tag="a" onClick={() => props.handleRowsPerPage(50)}>
                50
              </DropdownItem>
              <DropdownItem
                tag="a"
                onClick={() => props.handleRowsPerPage(100)}
              >
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
            onChange={(date) => props.handleCreateDate(date)}
            options={{
              altInput: true,
              altFormat: "F j, Y",
              dateFormat: "Y-m-d",
            }}
          />
        </div>



        <div className="filter-section">
          <Input
            placeholder="search(title) ..."
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

    currenChatData: null,

    // c.Id,
    // c.Username,
    // c.CreateDate,
    // c.TransactionConfirmedStatus,
    // c.TransactionType,
    // c.ServiceTypeWithDetails,
    // c.ProviderUserName,
    // c.ClientUserName,
    // c.BaseMyServiceTBL.ServiceName,
    // c.CardTBL.CardName,

    columns: [
      {
        name: "Username",
        selector: "username",
        sortable: true,
        cell: (row) => (
          <p title={row.username} className="text-truncate text-bold-500 mb-0">
            {row.username}
          </p>
        ),
      },
      {
        name: "Amount",
        selector: "amount",
        sortable: true,
        // minWidth: "200px",
        cell: (row) => (
          <p title={row.amount} className="text-truncate text-bold-500 mb-0">
            ${row.amount}
          </p>
        ),
      },
      {
        name: "date of registration",
        selector: "createDate",
        sortable: true,
        // minWidth: "200px",
        cell: (row) => (
          <p title={row.createDate} className="text-truncate text-bold-500 mb-0">
            {row.createDate}
          </p>
        ),
      },

      {
        name: "transaction confirmed status ",
        selector: "transactionConfirmedStatus",
        sortable: true,
        cell: (row) => {
          if (row.transactionConfirmedStatus == 0)
            return (
              <div className="badge badge-pill badge-light-success">Confirmed</div>
            );
          if (row.transactionConfirmedStatus == 1)
            return (
              <div className="badge badge-pill badge-light-danger">Rejected</div>
            );

          if (row.transactionConfirmedStatus == 2)
            return (
              <div className="badge badge-pill badge-light-primary">Pending</div>
            );
        },
      },
      // {
      //   name: "transaction status",
      //   selector: "transactionStatus",
      //   sortable: true,
      //   cell: (row) => {
      //     if (row.transactionStatus == 0)
      //       return (
      //         <span className="badge badge-primary">CompleteProfileTransaction</span>
      //       );
      //     if (row.transactionStatus == 1)
      //       return (
      //         <span className="badge badge-success">NormalTransaction</span>
      //       );
      //     if (row.transactionStatus == 2)
      //       return (
      //         <span className="badge badge-light">service transaction</span>
      //       );
      //   },
      // },
      {
        name: "Transaction Type",
        selector: "transactionType",
        sortable: true,
        cell: (row) => {
          if (row.transactionType == 0)
            return (
              <div className="badge badge-pill badge-light-success">WhiteDrawl</div>
            );
          if (row.transactionType == 1)
            return (
              <div className="badge badge-pill badge-light-warning">Deposit</div>
            );
        },
      },
      {

        name: "service Type",
        selector: "serviceTypeWithDetails",
        sortable: true,
        minWidth: "200px",
        cell: (row) => {
          if (row.serviceTypeWithDetails?.includes("0"))
            return (
              <button type="button" className="btn btn-sm btn-primary">Chat-Voice </button>
              // <button type="button" className="btn btn-sm btn-primary">Chat-Voice(Free)</button>
            );

          if (row.serviceTypeWithDetails?.includes("1"))
            return (
              <button type="button" className="btn  btn-sm btn-success"> Video-Call   </button>
              // <button type="button" className="btn  btn-sm btn-success">Chat-Voice(Duration)</button>
            );

          if (row.serviceTypeWithDetails?.includes("2"))
            return (
              <button type="button" className="btn  btn-sm btn-warning">Voice-Call</button>
              // <button type="button" className="btn  btn-sm btn-warning">Video-Call</button>
            );
          if (row.serviceTypeWithDetails?.includes("3"))
            return (
              <button type="button" className="btn  btn-sm btn-danger">Service</button>
            );
          if (row.serviceTypeWithDetails.includes("4"))
            return (
              <button type="button" className="btn  btn-sm btn-secondary">Course</button>
            );
        },
      },
      {
        name: "Provider UserName",
        selector: "providerUserName",
        sortable: true,
        minWidth: "150px",
        cell: (row) => (
          // <div className="badge badge-pill badge-light-warning" > {row.providerUserName ? row.providerUserName : "_"}</div>
          <>{row.providerUserName ? row.providerUserName : "_"} </>
        )
      },
      {
        name: "Client UserName",
        selector: "clientUserName",
        sortable: true,
        minWidth: "150px",
        cell: (row) => (
          // <div className="badge badge-pill badge-light-success" > {row.clientUserName ? row.clientUserName : "_"}</div>
          <> {row.clientUserName ? row.clientUserName : "_"}</>
        )
      },

      // {
      //   name: "Actions",
      //   sortable: true,
      //   sortable: false,
      //   minWidth: "100px",
      //   cell: (row) => (
      //     <ActionsComponent
      //       row={row}
      //       handleTicketStatus={this.handleTicketStatus}
      //       getData={this.props.getData}
      //       parsedFilter={this.props.parsedFilter}
      //       currentData={this.handleCurrentData}
      //       getTicketsDetails={this.getTicketsDetails}
      //     />
      //   ),
      // },
    ],
    allData: [],
    value: "",
    rowsPerPage: 10,


    createDate: null,
    amount: null,
    transactionStatus: null,
    transactionType: null,
    serviceTypeWithDetails: null,
    transactionConfirmedStatus: null,




    modal: false,


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
    // // console.log(params);
    const { data } = await agent.Transactions.GetAllServiceTransactionInAdmin(params);
    await this.setState({
      data: data.result.data.transactions,
      allData: data.result.data.transactions,
      totalPages: data.result.data.totalPages,
      loading: false,
    });
  }

  async componentDidMount() {
    this.populatingData();
  }

  async componentDidUpdate(prevProps, prevState) { }


  axiosParams = () => {
    let {
      value,
      rowsPerPage,
      currentPage,

      createDate,
      // amount,
      transactionStatus,
      transactionType,
      serviceTypeWithDetails,
      transactionConfirmedStatus,

    } = this.state;


    if (!rowsPerPage) rowsPerPage = 10;
    if (!currentPage) currentPage = 0;

    let obj = {
      page: currentPage,
      perPage: rowsPerPage,
      searchedWord: value.toString(),
      createDate

    }

    // amount,
    // transactionStatus:,
    // transactionType,
    // serviceTypeWithDetails,
    // transactionConfirmedStatus,

    if (transactionStatus || transactionStatus == 0)
      obj.transactionStatus = transactionStatus

    if (transactionType || transactionType == 0)
      obj.transactionType = transactionType

    if (serviceTypeWithDetails || serviceTypeWithDetails == 0)
      obj.serviceTypeWithDetails = serviceTypeWithDetails

    if (transactionConfirmedStatus || transactionConfirmedStatus == 0)
      obj.transactionConfirmedStatus = transactionConfirmedStatus

    return obj
  };



  // ************************************************** handles for seach start  **************************************************


  handleRowsPerPage = async (value) => {
    await this.setState({ rowsPerPage: value, currentPage: 0, loading: true });
    this.populatingData();
  };



  handleFilter = async (e) => {
    if (this.state.value == "" && e.target.value == "") return;
    await this.setState({
      loading: true,
      value: e.target.value,
      currentPage: 0,
    });
    this.populatingData();
  };


  handleCreateDate = async (value) => {
    var createDate = combineDateAndTime2(new Date(value));
    await this.setState({ createDate, currentPage: 0, loading: true });
    this.populatingData();
  };




  handleAmount = async (value) => {
    await this.setState({
      amount: value,
      currentPage: 0,
      loading: true,
    });
    this.populatingData();
  };



  handleTransactionStatus = async (value) => {
    await this.setState({
      // tiketStatus: value,
      transactionStatus: value,
      currentPage: 0,
      loading: true,
    });
    this.populatingData();
  };




  handleTransactionType = async (value) => {
    await this.setState({
      // tiketStatus: value,
      transactionType: value,
      currentPage: 0,
      loading: true,
    });
    this.populatingData();
  };

  handleServiceTypeWithDetails = async (value) => {
    await this.setState({
      // tiketStatus: value,
      serviceTypeWithDetails: value,
      currentPage: 0,
      loading: true,
    });
    this.populatingData();
  };



  handleTransactionConfirmedStatus = async (value) => {
    await this.setState({
      // tiketStatus: value,
      transactionConfirmedStatus: value,
      currentPage: 0,
      loading: true,
    });
    this.populatingData();
  };

  // ************************************************** handles for seach finished  ************************************************** 

  // ************************************************** returns Enums start  ************************************************** 
  // .
  // .
  returnTransactionStatus = (TherEnum) => {
    if (TherEnum == 0) return "complete profile transaction";
    if (TherEnum == 1) return "normal transaction";
    if (TherEnum == 2) return "service transaction";
  };


  returnTransactionType = (TherEnum) => {
    if (TherEnum == 0) return "WhiteDrawl";
    if (TherEnum == 1) return "Deposit";
  };


  returnServiceTypeWithDetails = (TherEnum) => {
    if (TherEnum == 0) return "Chat-Voice(Free)";
    if (TherEnum == 1) return "Chat-Voice(Duration)";
    if (TherEnum == 2) return "Video-Call";
    if (TherEnum == 3) return "Service";
    if (TherEnum == 4) return "Course";
  };


  returnTransactionConfirmedStatus = (TherEnum) => {
    if (TherEnum == 0) return "Confirmed";
    if (TherEnum == 1) return "Rejected";
    if (TherEnum == 2) return "Pending";
  };
  // .
  // .
  // ************************************************** returns Enums finished  ************************************************** 











  handleSidebar = (boolean, addNew = false) => {
    this.setState({ sidebar: boolean });
    if (addNew === true) this.setState({ currentData: null, addNew: true });
  };

  getTicketsDetails = async (row) => {
    this.returnLoading("Loading ...");
    const { data } = await agent.Tikets.GetTiketsDetails(row.id);
    await this.setState({ currenChatData: data.result.data });


    let datas = [...this.state.data];
    let index = datas.findIndex(el => el.id == row.id);

    datas[index].isUserSendNewMessgae = false;
    this.setState({ data: [] });
    this.setState({ data: datas });

    this.toggleModal();
    Swal.close();
  };

  returnLoading = (title) => {
    Swal.fire({
      title: title,
      allowEnterKey: false,
      allowEscapeKey: false,
      allowOutsideClick: false,
    });
    Swal.showLoading();
  };

  toggleModal = () => {
    this.setState((prevState) => ({
      modal: !prevState.modal,
    }));
  };

  handleTicketStatus = async (row) => {
    // this.setState({ loadngDelete: true });
    Swal.fire({
      title: row.tiketStatus == 0 ? "close ticket" : "open ticket",
      text: "Are you sure?",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes,do that!",
    }).then(async (result) => {
      if (result.isConfirmed) {
        Swal.fire("loading ...", "success");
        try {
          const { data } = await agent.Tikets.closeOrOpenTicket(row.id);
          if (data.result.data == 0) {
            this.setState({
              data: this.state.data.map((el) =>
                el.id === row.id
                  ? Object.assign({}, el, { tiketStatus: 0 })
                  : el
              ),
            });
            Swal.fire("Successful opening ", "ticked successfully opend", "success")
          } else {
            this.setState({
              data: this.state.data.map((el) =>
                el.id === row.id
                  ? Object.assign({}, el, { tiketStatus: 1 })
                  : el
              ),
            });
            Swal.fire("Successful closing ", "ticked successfully closed", "success")
          }
        } catch (ex) {
          let res = this.handleCatch2(ex);
          Swal.fire(`Error!`, `${res}`, "warning");
        }
      }
    });
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
      return errorMessage;
    }
  };

  handleSendChatMessage = async (text, ticketId) => {
    if (!text || text.length == 0) {
      alert("text is required")
    }
    else {
      this.returnLoading("Loading ...");
      let messageDate = this.dateReturn();
      const obj = { isAdmin: true, isFile: false, text, createDate: messageDate }
      let messages = await this.state.currenChatData?.messages.push(obj)
      await this.setState({ currenChatData: this.state.currenChatData, messages, Loading: true })
      const errorMessage = "";
      const errors = [];
      this.setState({ errorMessage, errors });
      try {
        const obj = { text, ticketId }
        const { data } = await agent.Tikets.addChatMessageToTiketInAdmin(obj);
      } catch (ex) {
        this.handleCatch(ex)
        let messages = this.state.currenChatData?.messages.pop();
        this.setState({ currenChatData: this.state.currenChatData, messages, modal: false })
      } finally {
        Swal.close();
      }
    }
  }



  handleUploadFileMessage = async (file, ticketId) => {
    if (!file || file.length == 0) {
      alert("file is required")
    }

    else {
      this.returnLoading("Loading ...");
      const errorMessage = "";
      const errors = [];
      this.setState({ errorMessage, errors });
      try {
        let form = new FormData();
        form.append('File', file);
        form.append('TicketId', ticketId);

        const { data } = await agent.Tikets.addFileToTiketInAdmin(form);


        let messageDate = this.dateReturn();
        const obj = { isAdmin: true, fileAddress: data.result.data, isFile: true, text: '', createDate: messageDate }
        let messages = await this.state.currenChatData?.messages.push(obj)
        await this.setState({ currenChatData: this.state.currenChatData, messages, })

      } catch (ex) {
        this.handleCatch(ex)
        this.setState({ modal: false })
      } finally {
        Swal.close();
      }
    }
  }




  dateReturn = () => {
    const date = new Date();
    const timeString = date.getHours() + ":" + date.getMinutes() + ":00";
    const year = date.getFullYear();
    const month = date.getMonth() + 1;
    const day = date.getDate();
    const dateString = `${year}-${month}-${day} (${timeString})`;
    return dateString;
  };




  handleCurrentData = (obj) => {
    this.setState({ currentData: obj });
    this.handleSidebar(true);
  };

  handlePagination = async (page) => {
    await this.setState({ loading: true, currentPage: page.selected });
    this.populatingData();
  };

  render() {
    let {
      columns,
      data,
      allData,
      totalPages,

      value,
      rowsPerPage,
      createDate,
      transactionConfirmedStatus,
      transactionType,
      serviceTypeWithDetails,
      transactionStatus,
      amount,



      currentData,
      currentPage,
      sidebar,
      totalRecords,
      sortIndex,
      errorMessage,
      errors,
    } = this.state;


    // c.Id,
    // c.Username,
    // c.CreateDate,
    // c.TransactionConfirmedStatus,
    // c.TransactionType,
    // c.ServiceTypeWithDetails,
    // c.ProviderUserName,
    // c.ClientUserName,
    // c.BaseMyServiceTBL.ServiceName,
    // c.CardTBL.CardName,


    // handleSidebar = { this.handleSidebar }
    // handleRowsPerPage = { this.handleRowsPerPage }
    // handleFilter = { this.handleFilter }
    // handleCreateDate = { this.handleCreateDate }
    // handleAmount = { this.handleAmount }
    // handleTransactionConfirmedStatus = { this.handleTransactionConfirmedStatus }
    // handleTransactionType = { this.handleTransactionType }
    // handleTransactionStatus = { this.handleTransactionStatus }



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
                returnTransactionStatus={this.returnTransactionStatus}
                returnTransactionType={this.returnTransactionType}
                returnServiceTypeWithDetails={this.returnServiceTypeWithDetails}
                returnTransactionConfirmedStatus={this.returnTransactionConfirmedStatus}

                handleSidebar={this.handleSidebar}
                handleRowsPerPage={this.handleRowsPerPage}
                handleFilter={this.handleFilter}
                handleCreateDate={this.handleCreateDate}
                handleAmount={this.handleAmount}
                handleTransactionConfirmedStatus={this.handleTransactionConfirmedStatus}
                handleTransactionType={this.handleTransactionType}
                handleTransactionStatus={this.handleTransactionStatus}
                handleServiceTypeWithDetails={this.handleServiceTypeWithDetails}


                rowsPerPage={rowsPerPage}
                createDate={createDate}
                transactionConfirmedStatus={transactionConfirmedStatus}
                transactionType={transactionType}
                serviceTypeWithDetails={serviceTypeWithDetails}
                transactionStatus={transactionStatus}
                amount={amount}


                total={totalRecords}
                index={sortIndex}




              // priorityStatus
              // returnPriorityStatus
              // handlePriorityStatus

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

          <ChatModal
            toggleModal={this.toggleModal}
            currenChatData={this.state.currenChatData}
            modal={this.state.modal}
            handleSendChatMessage={this.handleSendChatMessage}
            handleUploadFileMessage={this.handleUploadFileMessage}
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
