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
import "../../../assets/scss/plugins/forms/flatpickr/flatpickr.scss";

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
import Checkbox from "../../../components/@vuexy/checkbox/CheckboxesVuexy";

import "../../../assets/scss/plugins/extensions/react-paginate.scss";
import "../../../assets/scss/pages/data-list.scss";

import { toast } from "react-toastify";
import agent from "../../../core/services/agent";
import { combineDateAndTime2 } from "../../../core/main";
// import Spinner from "../../../components/@vuexy/spinner/Loading-spinner";

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
        <UncontrolledDropdown className="data-list-rows-dropdown mr-1 d-md-block ">
          <DropdownToggle color="" className="sort-dropdown">
            <span className="align-middle mx-50">
              ticket status:{" "}
              {props.returnTiketStatus(props.tiketStatus)}
            </span>
            <ChevronDown size={15} />
          </DropdownToggle>
          <DropdownMenu tag="div" right>
            <DropdownItem
              tag="a"
              onClick={() => props.handleTiketStatus(null)}
            ></DropdownItem>

            <DropdownItem
              tag="a"
              onClick={() => props.handleTiketStatus(0)}
            >
              Open
            </DropdownItem>
            <DropdownItem
              tag="a"
              onClick={() => props.handleTiketStatus(1)}
            >
              Closed
            </DropdownItem>

          </DropdownMenu>
        </UncontrolledDropdown>



        <UncontrolledDropdown className="data-list-rows-dropdown mr-1 d-md-block ">
          <DropdownToggle color="" className="sort-dropdown">
            <span className="align-middle mx-50">
              priority status:{" "}
              {props.returnPriorityStatus(props.priorityStatus)}
            </span>
            <ChevronDown size={15} />
          </DropdownToggle>
          <DropdownMenu tag="div" right>
            <DropdownItem
              tag="a"
              onClick={() => props.handlePriorityStatus(null)}
            ></DropdownItem>

            <DropdownItem
              tag="a"
              onClick={() => props.handlePriorityStatus(0)}
            >
              instantaneous
            </DropdownItem>
            <DropdownItem
              tag="a"
              onClick={() => props.handlePriorityStatus(1)}
            >
              normal
            </DropdownItem>

            <DropdownItem
              tag="a"
              onClick={() => props.handlePriorityStatus(2)}
            >
              for information
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
        name: "title",
        selector: "title",
        sortable: true,
        // minWidth: "200px",
        cell: (row) => (
          <p title={row.title} className="text-truncate text-bold-500 mb-0">
            {row.title}
          </p>
        ),
      },
      {
        name: "createDate",
        selector: "createDate",
        sortable: true,
        cell: (row) => {
          const dateString = this.returnCreateDate(row.createDate);
          return (
            <p title={dateString} className="text-truncate text-bold-500 mb-0">
              {dateString}
            </p>
          );
        },
      },


      {
        name: "priority status ",
        selector: "priorityStatus",
        sortable: true,
        cell: (row) => {
          if (row.priorityStatus == 0)
            return (
              <div className="badge badge-pill badge-light-success">instantaneous</div>
            );
          if (row.priorityStatus == 1)
            return (
              <div className="badge badge-pill badge-light-warning">normal</div>
            );

          if (row.priorityStatus == 2)
            return (
              <div className="badge badge-pill badge-light-primary">for information</div>
            );
        },
      },
      {
        name: "tiket status",
        selector: "tiketStatus",
        sortable: true,
        cell: (row) => {
          if (row.tiketStatus == 0)
            return (
              <div className="badge badge-pill badge-light-success">open</div>
            );
          if (row.tiketStatus == 1)
            return (
              <div className="badge badge-pill badge-light-warning">closed</div>
            );
        },
      },

      {
        name: "is user send new messgae",
        selector: "isUserSendNewMessgae",
        minWidth: "60px",
        sortable: false,
        cell: (row) => {
          return (
            <Checkbox
              disabled
              color="primary"
              icon={<Check className="vx-icon" size={16} />}
              label=""
              defaultChecked={row.isUserSendNewMessgae}
            />
          );
        },
      },

      {
        name: "Actions",
        sortable: true,
        sortable: false,
        minWidth: "100px",
        cell: (row) => (
          <ActionsComponent
            row={row}
            handleTicketStatus={this.handleTicketStatus}
            getData={this.props.getData}
            parsedFilter={this.props.parsedFilter}
            currentData={this.handleCurrentData}
            getTicketsDetails={this.getTicketsDetails}
          />
        ),
      },
    ],
    allData: [],
    value: "",
    rowsPerPage: 10,
    createDate: null,
    tiketStatus: null,
    priorityStatus: null,
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
    const { data } = await agent.Tikets.GetAllTiketsInAdmin(params);
    await this.setState({
      data: data.result.data.tikets,
      allData: data.result.data.tikets,
      totalPages: data.result.data.totalPages,
      loading: false,
    });
  }

  async componentDidMount() {
    this.populatingData();
  }

  async componentDidUpdate(prevProps, prevState) { }

  returnCreateDate = (createDate) => {
    const date = new Date(createDate);
    const timeString = date.getHours() + ":" + date.getMinutes() + ":00";
    const year = date.getFullYear();
    const month = date.getMonth() + 1;
    const day = date.getDate();
    const dateString = `${year}-${month}-${day} (${timeString})`;
    return dateString;
  };




  returnTiketStatus = (ticketStatus) => {
    if (ticketStatus == 0) return "Open";
    if (ticketStatus == 1) return "Closed";
  };



  returnPriorityStatus = (priorityStatus) => {
    if (priorityStatus == 0) return "instantaneous";
    if (priorityStatus == 1) return "normal";
    if (priorityStatus == 2) return "for information";
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

  axiosParams = () => {
    let {
      value,
      rowsPerPage,
      currentPage,
      createDate,
      tiketStatus,
      priorityStatus,
    } = this.state;
    var params = new URLSearchParams();
    if (!rowsPerPage) rowsPerPage = 10;
    if (!currentPage) currentPage = 0;
    params.append("page", currentPage.toString());
    params.append("perPage", rowsPerPage.toString());
    params.append("searchedWord", value.toString());

    if (createDate) params.append("createDate", createDate);

    if (tiketStatus || tiketStatus == 0)
      params.append("tiketStatus", tiketStatus);

    if (priorityStatus || priorityStatus == 0)
      params.append("priorityStatus", priorityStatus);


    return params;
  };

  handleRowsPerPage = async (value) => {
    await this.setState({ rowsPerPage: value, currentPage: 0, loading: true });
    this.populatingData();
  };



  handleCreateDate = async (value) => {
    var createDate = combineDateAndTime2(new Date(value));
    await this.setState({ createDate, currentPage: 0, loading: true });
    this.populatingData();
  };


  handleTiketStatus = async (value) => {
    await this.setState({
      tiketStatus: value,
      currentPage: 0,
      loading: true,
    });
    this.populatingData();
  };




  handlePriorityStatus = async (value) => {
    await this.setState({
      // tiketStatus: value,
      priorityStatus: value,
      currentPage: 0,
      loading: true,
    });
    this.populatingData();
  };


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





    // await this.setState({ data: [] })
    // await this.setState({
    //   // data: this.state.data.map(el => (el.id === row.id ? Object.assign({}, el, { isUserSendNewMessgae: false }) : el))
    //   data: { ...this.state.data, tikets: this.state.tikets.map(el => (el.id === row.id ? Object.assign({}, el, { isUserSendNewMessgae: false }) : el)) }
    // });

    // console.log(this.state.data);

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
      serviceType,
      tiketStatus,
      priorityStatus,
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

                returnTiketStatus={this.returnTiketStatus}
                returnPriorityStatus={this.returnPriorityStatus}
                handleFilter={this.handleFilter}
                handleRowsPerPage={this.handleRowsPerPage}

                handleCreateDate={this.handleCreateDate}
                handleTiketStatus={this.handleTiketStatus}
                handlePriorityStatus={this.handlePriorityStatus}

                createDate={createDate}
                tiketStatus={tiketStatus}
                priorityStatus={priorityStatus}
                rowsPerPage={rowsPerPage}
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

          {/* <ChatDetailsModal
            toggleModal={this.toggleModal}
            currenChatServiceData={this.state.currenChatServiceData}
            modal={this.state.modal}
          /> */}

          {/* <ChatModal
            toggleModal={this.toggleModal}
            currenChatData={this.state.currenChatServiceData}
            modal={this.state.modal}
          /> */}

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
