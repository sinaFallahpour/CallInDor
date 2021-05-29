import React from "react";
import ReactDOM from "react-dom";
import { Input, Button } from "reactstrap";
import { MessageSquare, Menu, Star, Send, Upload } from "react-feather";
import PerfectScrollbar from "react-perfect-scrollbar";
// import { connect } from "react-redux"
// import { togglePinned, sendMessage } from "../../../redux/actions/chat/index"

// import ChatLog from "./ChatLog"

import { Modal, ModalHeader, ModalBody } from "reactstrap";
import userImg from "../../../../assets/img/portrait/small/avatar-s-11.jpg";

import { baseUrl } from "../../../../core/services/agent";

import "../../../../assets/scss/pages/app-chat.scss";
const mql = window.matchMedia(`(min-width: 992px)`);

//sender image

class Chat extends React.Component {
  state = {
    id: null,
    title: "",
    userName: "",
    name: "",
    lastName: "",
    imageAddress: "",
    messages: null,

    msg: "",
    activeUser: null,
    activeChat: null,

    errors: [],
    errorMessage: "",
    Loading: false,

    activeTab: "1",
    modal: false,
  };

  componentDidMount() { }



  //   {
  //   "id": 30,
  //     "isAdmin": true,
  //       "isFile": false,
  //         "text": "aasasas",
  //           "createDate": "2020-12-05T16:12:37.2978395",
  //             "fileAddress": null
  // },

  componentDidUpdate(prevProps, prevState) {
    if (this.props.currenChatData !== null) {
      if (this.props.currenChatData.id != prevState.id) {
        const {
          id,
          title,
          userName,
          name,
          lastName,
          imageAddress,
          messages,
        } = this.props.currenChatData;
        this.setState({
          id,
          title,
          userName,
          name,
          lastName,
          imageAddress,
          messages,
          modal: this.props.modal,
        });
      }
    }
    if (
      this.props.currenChatData === null &&
      prevProps.currenChatData !== null
    ) {
      this.setState({
        title: "",
        userName: "",
        name: "",
        lastName: "",
        id: null,
        imageAddress: "",
        messages: null,
        modal: false,
      });
    }
  }

  // componentDidMount() {
  //     // this.scrollToBottom()
  //     this.setState({
  //         activeUser: {
  //             uid: 1,
  //             displayName: "sina fallahpour",
  //             about: "about sina fallahpour",
  //             photoURL: "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD…LRa0UZncvooTcTDl3eOWW2I3b+nCTizFlU9XwrM+Tdx0o/9k=",
  //             status: "offline"
  //         },

  //         activeChat: {
  //             msg: [
  //                 {
  //                     textContent: "Hello. How can I help You?",
  //                     time: "Mon Aug 22 2020 07:45:15 GMT+0000 (GMT)",
  //                     isSent: false,
  //                     isSeen: true
  //                 },
  //                 {
  //                     textContent: "Can I get details of my last transaction I made last month?",
  //                     time: "Mon Aug 22 2020 07:46:10 GMT+0000 (GMT)",
  //                     isSent: true,
  //                     isSeen: true
  //                 },
  //                 {
  //                     textContent: "We need to check if we can provide you such information.",
  //                     time: "Mon Aug 22 2020 07:45:15 GMT+0000 (GMT)",
  //                     isSent: false,
  //                     isSeen: true
  //                 }
  //             ]
  //         }
  //     })

  // }
  // componentDidUpdate() {
  //     // this.scrollToBottom()
  // }

  handleSendMessage = async (text) => {
    if (!text || text.length == 0) {
      alert("text is required")
    }
    this.setState({ msg: "" });
    await this.props.handleSendChatMessage(text, this.state.id)
  };

  handleTime = (time_to, time_from) => {
    const date_time_to = new Date(Date.parse(time_to));
    const date_time_from = new Date(Date.parse(time_from));
    return (
      date_time_to.getFullYear() === date_time_from.getFullYear() &&
      date_time_to.getMonth() === date_time_from.getMonth() &&
      date_time_to.getDate() === date_time_from.getDate()
    );
  };


  returnDate = (createDate) => {
    const date = new Date(createDate);
    const timeString = date.getHours() + ":" + date.getMinutes() + ":00";
    const year = date.getFullYear();
    const month = date.getMonth() + 1;
    const day = date.getDate();
    const dateString = `${year}-${month}-${day} (${timeString})`;
    return dateString;
  };

  render() {
    const { activeUser } = this.state;
    let activeUserUid = activeUser && activeUser.uid ? activeUser.uid : null,
      activeChat = this.state.activeChat;
    // // // // // // activeUser && activeUser.uid
    // // // // // //   ? this.props.chat.chats[activeUserUid]
    // // // // // //   : null

    const { messages } = this.state;

    let renderChats = messages?.map((chat, i) => {
      let renderSentTime = () => {
        if (
          i > 0 &&
          !this.handleTime(chat.createDate, messages[i - 1].createDate)
        ) {
          return (
            <div className="divider">
              <div className="divider-text">
                {new Date().getDate() +
                  " " +
                  new Date().toLocaleString("default", {
                    month: "short",
                  })}
              </div>
            </div>
          );
        }
      };
      let renderAvatar = () => {
        if (i > 0) {
          if (chat.isAdmin === true && messages[i - 1].isAdmin !== true) {
            return (
              <div className="chat-avatar">
                <div className="avatar m-0">
                  <img src={userImg} alt="chat avatar" height="40" width="40" />
                </div>
              </div>
            );
          } else if (chat?.isAdmin !== true) {
            return (
              <div className="chat-avatar">
                <div className="avatar m-0">
                  <img
                    src={baseUrl + this.state.imageAddress}
                    alt="chat avatar"
                    height="40"
                    width="40"
                  />
                </div>
              </div>
            );
          } else {
            return "";
          }
        } else {
          return (
            <div className="chat-avatar">
              <div className="avatar m-0">
                <img
                  src={
                    chat.isAdmin ? userImg : baseUrl + this.state.imageAddress
                  }
                  alt="chat avatar"
                  height="40"
                  width="40"
                />
              </div>
            </div>
          );
        }
      };

      return (
        <React.Fragment key={i}>
          {/* {renderSentTime()} */}
          <div
            className={`chat ${chat.isAdmin !== true ? "chat-left" : "chat-right"
              }`}
          >
            {/* generate users and admin image */}
            {renderAvatar()}
            <div className="chat-body">
              {chat.isFile ?

                <a
                  className="chat-content" href={baseUrl + chat.fileAddress}
                  style={{ display: "flex", flexDirection: "column" }}
                  target="_blank">
                  <span> uploaded file</span>
                  <Upload size={15} />
                  <span style={{ fontSize: "11px" }}> {this.returnDate(chat.createDate)}</span>
                </a>

                :

                <div className=" chat-content " style={{ display: "flex", flexDirection: "column" }}  >
                  <span> {chat.text}</span>
                  <span style={{ fontSize: "11px" }}> {this.returnDate(chat.createDate)}</span>
                </div>
              }

              {/* <div className="chat-content">
                {this.returnDate(chat.createDate)}
              </div> */}
            </div>
          </div>
        </React.Fragment>
      );
    });
    // : null

    // let renderChats =
    //     activeChat && activeChat !== undefined && activeChat.msg
    //         ? activeChat.msg?.map((chat, i) => {
    //             let renderSentTime = () => {
    //                 if (
    //                     i > 0 &&
    //                     !this.handleTime(chat.time, activeChat.msg[i - 1].time)
    //                 ) {
    //                     return (
    //                         <div className="divider">
    //                             <div className="divider-text">
    //                                 {new Date().getDate() +
    //                                     " " +
    //                                     new Date().toLocaleString("default", {
    //                                         month: "short"
    //                                     })}
    //                             </div>
    //                         </div>
    //                     )
    //                 }
    //             }
    //             let renderAvatar = () => {
    //                 if (i > 0) {
    //                     if (
    //                         chat.isSent === true &&
    //                         activeChat.msg[i - 1].isSent !== true
    //                     ) {
    //                         return (
    //                             <div className="chat-avatar">
    //                                 <div className="avatar m-0">
    //                                     <img
    //                                         src={userImg}
    //                                         alt="chat avatar"
    //                                         height="40"
    //                                         width="40"
    //                                     />
    //                                 </div>
    //                             </div>
    //                         )
    //                     } else if (chat?.isSent !== true) {
    //                         return (
    //                             <div className="chat-avatar">
    //                                 <div className="avatar m-0">
    //                                     <img
    //                                         src={activeUser?.photoURL}
    //                                         alt="chat avatar"
    //                                         height="40"
    //                                         width="40"
    //                                     />
    //                                 </div>
    //                             </div>
    //                         )
    //                     } else {
    //                         return ""
    //                     }
    //                 } else {
    //                     return (
    //                         <div className="chat-avatar">
    //                             <div className="avatar m-0">
    //                                 <img
    //                                     src={chat.isSent ? userImg : activeUser.photoURL}
    //                                     alt="chat avatar"
    //                                     height="40"
    //                                     width="40"
    //                                 />
    //                             </div>
    //                         </div>
    //                     )
    //                 }
    //             }
    //             return (
    //                 <React.Fragment key={i}>
    //                     {renderSentTime()}
    //                     <div
    //                         className={`chat ${chat.isSent !== true ? "chat-left" : "chat-right"
    //                             }`}>
    //                         {renderAvatar()}
    //                         <div className="chat-body">
    //                             <div className="chat-content">{chat.textContent}</div>
    //                         </div>
    //                     </div>
    //                 </React.Fragment>
    //             )
    //         })
    //         : null

    return (
      <Modal
        isOpen={this.props.modal}
        toggle={this.props.toggleModal}
        className="modal-dialog-centered modal-lg"
      >
        <ModalHeader toggle={this.props.toggleModal}>
          Ticket Messages
        </ModalHeader>
        <ModalBody>
          <div className="chat-application position-relative">
            <div className="">
              <div className="chat-app-window">
                {/* <div
                                    className={`start-chat-area ${activeUser !== null ? "d-none" : "d-flex"
                                        }`}>
                                    <span className="mb-1 start-chat-icon">
                                        <MessageSquare size={50} />
                                    </span>
                                    <h4
                                        className="py-50 px-1 sidebar-toggle start-chat-text"
                                        onClick={() => {
                                            if (this.props.mql.matches === false) {
                                                this.props.mainSidebar(true)
                                            } else {
                                                return null
                                            }
                                        }}>
                                        Start Conversation
                                    </h4>
                                </div> */}
                <div
                  className="active-chat d-block"
                // className={`active-chat ${activeUser === null ? "d-none" : "d-block"
                //                                         }`}
                >


                  <div className="chat_navbar">
                    <header className="chat_header d-flex justify-content-between align-items-center p-1">
                      <div className="d-flex align-items-center">
                        <div
                          className="sidebar-toggle d-block d-lg-none mr-1"
                          onClick={() => this.props.mainSidebar(true)}
                        >
                          <Menu size={24} />
                        </div>
                        <div
                          className="avatar user-profile-toggle m-0 m-0 mr-1"
                        // onClick={() => this.props.handleReceiverSidebar("open")}
                        >
                          {/* <img
                                                        src={activeUser !== null ? activeUser?.photoURL : ""}
                                                        alt={activeUser !== null ? activeUser?.displayName : ""}
                                                        height="40"
                                                        width="40"
                                                    /> */}

                          <img
                            src={baseUrl + this.state.imageAddress}
                            alt={`${this.state.name} ${this.state.lastName}`}
                            height="40"
                            width="40"
                          />

                          {/* <span
                                                        className={`
                                                        ${activeUser !== null &&
                                                                activeUser.status === "do not disturb"
                                                                ? "avatar-status-busy"
                                                                : activeUser !== null && activeUser?.status === "away"
                                                                    ? "avatar-status-away"
                                                                    : activeUser !== null && activeUser?.status === "offline"
                                                                        ? "avatar-status-offline"
                                                                        : "avatar-status-online"
                                                            }
                                                        `}
                                                    /> */}
                        </div>
                        <h6 className="mb-0">
                          {`${this.state.name} ${this.state.lastName}`}
                          {/* {activeUser !== null ? activeUser?.displayName : ""} */}
                        </h6>
                      </div>
                      <span
                        className="favorite"
                      // onClick={() => {
                      //     if (activeChat) {
                      //         this.props.togglePinned(
                      //             activeUser.uid,
                      //             !activeChat.isPinned
                      //         )
                      //     }
                      // }}
                      >
                        {/* <Star
                                                    size={22}
                                                    stroke={
                                                        activeChat && activeChat.isPinned === true
                                                            ? "#FF9F43"
                                                            : "#626262"
                                                    }
                                                /> */}
                      </span>
                    </header>
                  </div>
                  <PerfectScrollbar
                    className="user-chats"
                    options={{
                      wheelPropagation: false,
                    }}
                    ref={(el) => {
                      this.chatArea = el;
                    }}
                  >
                    <div className="chats">{renderChats}</div>
                  </PerfectScrollbar>
                  <div className="chat-app-form">
                    <form
                      className="chat-app-input d-flex align-items-center"
                      onSubmit={(e) => {
                        e.preventDefault();
                        this.handleSendMessage(this.state.msg);
                      }}
                    >
                      <Input
                        type="text"
                        className="message mr-1 ml-50"
                        placeholder="Type your message"
                        value={this.state.msg}
                        onChange={(e) => {
                          e.preventDefault();
                          this.setState({
                            msg: e.target.value,
                          });
                        }}
                        required
                      />


                      {/* fileHandler = (event) => {
                        this.setState({ file: event.target.files[0] });
  }; */}

                      {/* <Button type="button" color="primary mr-1"> */}

                      <label htmlFor="inp" className="d-block btn btn-primary mr-1">
                        <Upload className="" size={15} />
                      </label>
                      <input type="file" id="inp"
                        onChange={(e) => {
                          this.props.handleUploadFileMessage(e.target.files[0], this.state.id)
                        }}

                        className="d-none" />

                      {/* </Button> */}


                      <Button color="primary">
                        <Send className="d-lg-none" size={15} />
                        <span className="d-lg-block d-none">Send</span>

                        {/* <span className="d-lg-block d-none">Send</span> */}
                      </Button>


                    </form>
                  </div>
                </div>
              </div>
            </div>

            {/* <ChatLog
                    mql={mql}
                /> */}
          </div>
        </ModalBody>
      </Modal>
    );
  }
}

export default Chat;
