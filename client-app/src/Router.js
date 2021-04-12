import React, { Suspense, lazy } from "react";
import { Router, Switch, Route, Redirect } from "react-router-dom";
import { history } from "./history";
// import { connect } from "react-redux";
// import PropTypes from 'prop-types';
import auth from "./core/services/userService/authService";
import CustomLoader from "./components/@vuexy/spinner/FullPageLoading";

import Spinner from "./components/@vuexy/spinner/Loading-spinner";
import knowledgeBaseCategory from "./views/pages/knowledge-base/Category";
import knowledgeBaseQuestion from "./views/pages/knowledge-base/Questions";
import { ContextLayout } from "./utility/context/Layout";
import { Alert } from "reactstrap";
import PageTitle from "./components/common/PageTitle";
import Permissoin from "./core/permissions";

const analyticsDashboard = lazy(() =>
  import("./views/dashboard/analytics/AnalyticsDashboard")
);

const email = lazy(() => import("./views/apps/email/Email"));
const todo = lazy(() => import("./views/apps/todo/Todo"));
const calendar = lazy(() => import("./views/apps/calendar/Calendar"));

const grid = lazy(() => import("./views/ui-elements/grid/Grid"));
const typography = lazy(() =>
  import("./views/ui-elements/typography/Typography")
);
const textutilities = lazy(() =>
  import("./views/ui-elements/text-utilities/TextUtilities")
);
const syntaxhighlighter = lazy(() =>
  import("./views/ui-elements/syntax-highlighter/SyntaxHighlighter")
);
const colors = lazy(() => import("./views/ui-elements/colors/Colors"));
const reactfeather = lazy(() =>
  import("./views/ui-elements/icons/FeatherIcons")
);
const basicCards = lazy(() => import("./views/ui-elements/cards/basic/Cards"));
const statisticsCards = lazy(() =>
  import("./views/ui-elements/cards/statistics/StatisticsCards")
);
const analyticsCards = lazy(() =>
  import("./views/ui-elements/cards/analytics/Analytics")
);
const actionCards = lazy(() =>
  import("./views/ui-elements/cards/actions/CardActions")
);
const Alerts = lazy(() => import("./components/reactstrap/alerts/Alerts"));
const Buttons = lazy(() => import("./components/reactstrap/buttons/Buttons"));
const Breadcrumbs = lazy(() =>
  import("./components/reactstrap/breadcrumbs/Breadcrumbs")
);
const Carousel = lazy(() =>
  import("./components/reactstrap/carousel/Carousel")
);
const Collapse = lazy(() =>
  import("./components/reactstrap/collapse/Collapse")
);
const Dropdowns = lazy(() =>
  import("./components/reactstrap/dropdowns/Dropdown")
);
const ListGroup = lazy(() =>
  import("./components/reactstrap/listGroup/ListGroup")
);
const Modals = lazy(() => import("./components/reactstrap/modal/Modal"));
const Pagination = lazy(() =>
  import("./components/reactstrap/pagination/Pagination")
);
const NavComponent = lazy(() =>
  import("./components/reactstrap/navComponent/NavComponent")
);
const Navbar = lazy(() => import("./components/reactstrap/navbar/Navbar"));
const Tabs = lazy(() => import("./components/reactstrap/tabs/Tabs"));
const TabPills = lazy(() =>
  import("./components/reactstrap/tabPills/TabPills")
);
const Tooltips = lazy(() =>
  import("./components/reactstrap/tooltips/Tooltips")
);
const Popovers = lazy(() =>
  import("./components/reactstrap/popovers/Popovers")
);
const Badge = lazy(() => import("./components/reactstrap/badge/Badge"));
const BadgePill = lazy(() =>
  import("./components/reactstrap/badgePills/BadgePill")
);
const Progress = lazy(() =>
  import("./components/reactstrap/progress/Progress")
);
const Media = lazy(() => import("./components/reactstrap/media/MediaObject"));
const Spinners = lazy(() =>
  import("./components/reactstrap/spinners/Spinners")
);
const Toasts = lazy(() => import("./components/reactstrap/toasts/Toasts"));
const avatar = lazy(() => import("./components/@vuexy/avatar/Avatar"));
const AutoComplete = lazy(() =>
  import("./components/@vuexy/autoComplete/AutoComplete")
);
const chips = lazy(() => import("./components/@vuexy/chips/Chips"));
const divider = lazy(() => import("./components/@vuexy/divider/Divider"));
const vuexyWizard = lazy(() => import("./components/@vuexy/wizard/Wizard"));
const listView = lazy(() => import("./views/ui-elements/data-list/ListView"));
const thumbView = lazy(() => import("./views/ui-elements/data-list/ThumbView"));
const select = lazy(() => import("./views/forms/form-elements/select/Select"));
const switchComponent = lazy(() =>
  import("./views/forms/form-elements/switch/Switch")
);
const checkbox = lazy(() =>
  import("./views/forms/form-elements/checkboxes/Checkboxes")
);
const radio = lazy(() => import("./views/forms/form-elements/radio/Radio"));
const input = lazy(() => import("./views/forms/form-elements/input/Input"));
const group = lazy(() =>
  import("./views/forms/form-elements/input-groups/InputGoups")
);
const numberInput = lazy(() =>
  import("./views/forms/form-elements/number-input/NumberInput")
);
const textarea = lazy(() =>
  import("./views/forms/form-elements/textarea/Textarea")
);
const pickers = lazy(() =>
  import("./views/forms/form-elements/datepicker/Pickers")
);
const inputMask = lazy(() =>
  import("./views/forms/form-elements/input-mask/InputMask")
);
const layout = lazy(() => import("./views/forms/form-layouts/FormLayouts"));
const formik = lazy(() => import("./views/forms/formik/Formik"));
const tables = lazy(() => import("./views/tables/reactstrap/Tables"));
const ReactTables = lazy(() =>
  import("./views/tables/react-tables/ReactTables")
);
const Aggrid = lazy(() => import("./views/tables/aggrid/Aggrid"));
const DataTable = lazy(() => import("./views/tables/data-tables/DataTables"));
const profile = lazy(() => import("./views/pages/profile/Profile"));

const knowledgeBase = lazy(() =>
  import("./views/pages/knowledge-base/KnowledgeBase")
);
const search = lazy(() => import("./views/pages/search/Search"));
const accountSettings = lazy(() =>
  import("./views/pages/account-settings/AccountSettings")
);

const comingSoon = lazy(() => import("./views/pages/misc/ComingSoon"));
const Error404 = lazy(() => import("./views/pages/misc/error/404"));
const error500 = lazy(() => import("./views/pages/misc/error/500"));
const authorized = lazy(() => import("./views/pages/misc/NotAuthorized"));
const maintenance = lazy(() => import("./views/pages/misc/Maintenance"));

const leafletMaps = lazy(() => import("./views/maps/Maps"));
const toastr = lazy(() => import("./extensions/toastify/Toastify"));
const sweetAlert = lazy(() => import("./extensions/sweet-alert/SweetAlert"));

const clipboard = lazy(() =>
  import("./extensions/copy-to-clipboard/CopyToClipboard")
);

const userList = lazy(() => import("./views/apps/user/list/List"));
const userEdit = lazy(() => import("./views/apps/user/edit/Edit"));
const userView = lazy(() => import("./views/apps/user/view/View"));
const Login = lazy(() => import("./views/pages/authentication/login/Login"));
const LogOut = lazy(() => import("./views/pages/authentication/LogOut"));
const AccesDenied = lazy(() =>
  import("./views/pages/authentication/Accesdenied")
);

const ForgotPassword = lazy(() =>
  import("./views/pages/authentication/ForgotPassword")
);
const lockScreen = lazy(() =>
  import("./views/pages/authentication/LockScreen")
);
const resetPassword = lazy(() =>
  import("./views/pages/authentication/ResetPassword")
);
const register = lazy(() =>
  import("./views/pages/authentication/register/Register")
);

// Route-based code splitting

const Dashboard = lazy(() => import("./views/pages/_Dashboard/_Dashboard"));

const Services = lazy(() => import("./views/pages/_Serices/Services"));
const CreateService = lazy(() => import("./views/pages/_Serices/Create"));
const EditService = lazy(() => import("./views/pages/_Serices/EditService"));




// const Services = lazy(() => import("./views/pages/_Serices/Services"));
const DataAnotation = lazy(() => import("./views/pages/_resourceManger/Create"));
// const EditService = lazy(() => import("./views/pages/_Serices/EditService"));




const RoleManager = lazy(() =>
  import("./views/pages/_rolemanager/Rolemanager")
);
const UserManager = lazy(() => import("./views/pages/_userManager/Users"));

const Users = lazy(() => import("./views/pages/_Users/Users"));

const Categories = lazy(() => import("./views/pages/Categories/Categories"));
const EditCategory = lazy(() =>
  import("./views/pages/Categories/EditCategory")
);

const Areas = lazy(() => import("./views/pages/_Areas/Areas"));
// const CreateArea = lazy(() => import("./views/pages/_Areas/Create"));
const EditArea = lazy(() => import("./views/pages/_Areas/EditService"));

const UsersVerification = lazy(() =>
  import("./views/pages/_UsersVerification/_Users")
);
const UsersDetails = lazy(() =>
  import("./views/pages/_UsersVerification/_DetailsUser")
);

const ProvidedServices = lazy(() =>
  import("./views/pages/ProvidedServices/_ProvidedServices")
);


const Transactions = lazy(() =>
  import("./views/pages/_Transactions/_Transactions")
);

const Tikets = lazy(() => import("./views/pages/_Tiket/_Tikets"));

const Test = lazy(() => import("./views/pages/_test/Test"));
const Settings = lazy(() => import("./views/pages/_Settings/Settings"));
const Questions = lazy(() => import("./views/pages/_Questions/_Questions"));

const Discounts = lazy(() => import("./views/pages/_Discounts/_DisCounts"));

// const Chat = lazy(() => import("./views/pages/_Tiket/Chat"));

// const chat = lazy(() => import("./view/pages/"))

const Chat = lazy(() => import("./views/pages/_Tiket/_Tikets"));

// Set Layout and Component Using App Route
const RouteConfig = ({
  component: Component,
  fullLayout,
  isLoggedIn,
  user,
  role,
  permission,
  title,
  ...rest
}) => (
  <Route
    {...rest}
    render={(props) => {
      if (!user.isLoggedIn || user?.userRole?.toLowerCase() == "user") {
        return (
          <Redirect
            exact
            to={{
              pathname: "/pages/login",
              state: { from: props.location },
            }}
          />
        );
      } else if (permission || role) {
        if (role) {
          if (user?.userRole?.toLowerCase() != role.toLowerCase()) {
            history.push("/misc/not-authorized");
          }
        }
        if (
          permission &&
          !user?.userPermissions?.some((v) => permission?.includes(v))
        ) {
          history.push("/misc/not-authorized");
          // return (
          //   <Redirect
          //     exact
          //     to={{
          //       pathname: "/pages/Accesdenied",
          //       state: { from: props.location },
          //     }}
          //   />
          // );
        } else {
          // return Component ? <Component {...props} /> : render(props)
          return (
            <ContextLayout.Consumer>
              {(context) => {
                const LayoutTag =
                  fullLayout === true
                    ? context.fullLayout
                    : context.state.activeLayout === "horizontal"
                      ? context.horizontalLayout
                      : context.VerticalLayout;
                return (
                  // permission={}
                  <LayoutTag {...props}>
                    <Suspense fallback={<Spinner />}>
                      <PageTitle title={title}>
                        <Component {...props} />
                      </PageTitle>
                    </Suspense>
                  </LayoutTag>
                );
              }}
            </ContextLayout.Consumer>
          );
        }
      } else {
        return (
          <ContextLayout.Consumer>
            {(context) => {
              const LayoutTag =
                fullLayout === true
                  ? context.fullLayout
                  : context.state.activeLayout === "horizontal"
                    ? context.horizontalLayout
                    : context.VerticalLayout;
              return (
                // permission={}
                <LayoutTag {...props}>
                  <Suspense fallback={<Spinner />}>
                    <PageTitle title={title}>
                      <Component {...props} />
                    </PageTitle>
                  </Suspense>
                </LayoutTag>
              );
            }}
          </ContextLayout.Consumer>
        );
      }
    }}
  />
);

// const mapStateToProps = (state) => {
//   return {
//     user: state.auth.login.userRole
//   };
// };

// const AppRoute = connect(mapStateToProps)(RouteConfig);

const NotProtexctedRouteConfig = ({
  component: Component,
  path,
  fullLayout,
  title,
  ...rest
}) => (
  <Route
    path={path}
    render={(props) => {
      return (
        <ContextLayout.Consumer>
          {(context) => {
            // const LayoutTag = context.fullLayout

            const LayoutTag =
              fullLayout === true
                ? context.fullLayout
                : context.state.activeLayout === "horizontal"
                  ? context.horizontalLayout
                  : context.VerticalLayout;
            return (
              <LayoutTag {...props} permission={props.user}>
                <Suspense fallback={<Spinner />}>
                  <PageTitle title={title}>
                    <Component {...props} />
                  </PageTitle>
                </Suspense>
              </LayoutTag>
            );
          }}
        </ContextLayout.Consumer>
      );
    }}
  />
);

class AppRouter extends React.Component {
  state = {
    loading: true,
    isLoggedIn: false,
    userPermissions: [],
    userRole: "",
  };

  async componentDidMount() {
    // console.clear();
    // console.log(auth.getPermissons());
    const userPermissions = auth.getPermissons();
    console.log(userPermissions);
    const userRole = auth.getRole();
    console.log(userRole);

    const isLoggedIn = await auth.checkTokenIsValid();
    console.log(isLoggedIn);
    this.setState({ isLoggedIn, userPermissions, userRole, loading: false });
  }

  render() {
    const { isLoggedIn, loading, userPermissions, userRole } = this.state;
    const user = { userPermissions, userRole, isLoggedIn };
    if (loading) {
      return <CustomLoader />;
    }

    return (
      // Set the directory path if you are deploying in sub-folder

      <Suspense fallback={<CustomLoader />}>
        <Router history={history}>
          <Switch>
            <NotProtexctedRouteConfig
              path="/pages/login"
              title="Login"
              component={Login}
              fullLayout
            />

            <NotProtexctedRouteConfig
              path="/pages/forgot-password"
              title="ForgotPassword"
              component={ForgotPassword}
              fullLayout
            />

            <Route path="/pages/logout" component={LogOut} fullLayout />
            <NotProtexctedRouteConfig
              path="/pages/Accesdenied"
              title="َAccess Denied"
              component={AccesDenied}
              fullLayout
            />

            {/* <Route
              path="/pages/forgot-password"
              render={(props) => {
                return (
                  <ContextLayout.Consumer>
                    {(context) => {
                      const LayoutTag = context.fullLayout
                      return (
                        <LayoutTag {...props} permission={props.user}>
                          <Suspense fallback={<Spinner />}>
                            <PageTitle title="ForgotPassword">
                              <ForgotPassword {...props} />
                            </PageTitle>
                          </Suspense>
                        </LayoutTag>
                      );
                    }}
                  </ContextLayout.Consumer>
                );
              }}
            /> */}
            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="manage admins"
              exact
              path="/pages/admins"
              role="admin"
              user={{ ...user }}
              component={UserManager}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="manage role"
              exact
              path="/pages/Roles"
              role="admin"
              user={{ ...user }}
              component={RoleManager}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Users"
              exact
              path="/pages/users"
              // role="admin"
              permission={[Permissoin.getAllUsersList]}
              user={{ ...user }}
              component={Users}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="categories"
              exact
              path="/pages/categories"
              role="admin"
              user={{ ...user }}
              component={Categories}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Edit Category"
              exact
              path="/pages/category/:id"
              role="admin"
              user={{ ...user }}
              component={EditCategory}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Areas"
              exact
              path="/pages/areas"
              role="admin"
              user={{ ...user }}
              component={Areas}
            />
            {/* <RouteConfig isLoggedIn={isLoggedIn} title="Create Area" exact path="/pages/areas/Create" component={CreateArea} /> */}
            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Edit Area"
              exact
              path="/pages/areas/:id"
              role="admin"
              user={{ ...user }}
              component={EditArea}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Dashboard"
              exact
              path="/"
              role="admin"
              user={{ ...user }}
              component={Dashboard}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Services"
              exact
              path="/pages/Services"
              role="admin"
              user={{ ...user }}
              component={Services}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Create Service"
              exact
              path="/pages/Services/Create"
              role="admin"
              user={{ ...user }}
              component={CreateService}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Edit Service"
              exact
              path="/pages/Services/:id"
              role="admin"
              user={{ ...user }}
              component={EditService}
            />








            {/* <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Services"
              exact
              path="/pages/Services"
              role="admin"
              user={{ ...user }}
              component={Services}
            />
           */}
          
            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="edit data anotation errro messages"
              exact
              path="/pages/resourceManger/dataAnotation"
              role="admin"
              user={{ ...user }}
              component={DataAnotation}
            />
            {/* <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Edit Service"
              exact
              path="/pages/Services/:id"
              role="admin"
              user={{ ...user }}
              component={EditService}
            /> */}











            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Settings"
              exact
              path="/pages/settings"
              role="admin"
              user={{ ...user }}
              component={Settings}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="questions"
              exact
              path="/pages/questions"
              role="admin"
              user={{ ...user }}
              component={Questions}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="tikets"
              exact
              path="/pages/Tikets"
              role="admin"
              user={{ ...user }}
              component={Tikets}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="mange discount"
              exact
              path="/pages/discount"
              role="admin"
              user={{ ...user }}
              component={Discounts}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Users-Verification"
              exact
              path="/pages/UsersVerification"
              // role="admin"
              permission={[Permissoin.getAllUsersList]}
              user={{ ...user }}
              component={UsersVerification}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Users-Details"
              exact
              path="/pages/Users-Details/:username"
              // role="admin"
              permission={[Permissoin.editUser]}
              user={{ ...user }}
              component={UsersDetails}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Provided-Services"
              exact
              path="/pages/Provided-Services"
              // role="admin"
              permission={[Permissoin.getAllProvidedService]}
              user={{ ...user }}
              component={ProvidedServices}
            />


            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="transactions"
              exact
              path="/pages/Transactions"
              role="admin"
              user={{ ...user }}
              component={Transactions}
            />



            <RouteConfig
              isLoggedIn={isLoggedIn}
              exact
              path="/pages/Test"
              // role="admin"
              user={{ ...user }}
              permission={[Permissoin.getAllUsersList, Permissoin.editUser]}
              component={Test}
            />












            {/* 
            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Admin Dashboard"
              exact
              path="/"
              // role="admin"
              user={{ ...user }}
              component={analyticsDashboard}
            /> */}

            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/email"
              role="admin"
              user={{ ...user }}
              exact
              component={() => <Redirect to="/email/inbox" />}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/email/:filter"
              role="admin"
              user={{ ...user }}
              component={email}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/todo"
              role="admin"
              user={{ ...user }}
              exact
              component={() => <Redirect to="/todo/all" />}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/todo/:filter"
              role="admin"
              user={{ ...user }}
              component={todo}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/calendar"
              role="admin"
              user={{ ...user }}
              component={calendar}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/data-list/list-view"
              role="admin"
              user={{ ...user }}
              component={listView}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/data-list/thumb-view"
              role="admin"
              user={{ ...user }}
              component={thumbView}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/ui-element/grid"
              role="admin"
              user={{ ...user }}
              component={grid}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/ui-element/typography"
              role="admin"
              user={{ ...user }}
              component={typography}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/ui-element/textutilities"
              role="admin"
              user={{ ...user }}
              component={textutilities}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/ui-element/syntaxhighlighter"
              role="admin"
              user={{ ...user }}
              component={syntaxhighlighter}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/colors/colors"
              role="admin"
              user={{ ...user }}
              component={colors}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/icons/reactfeather"
              role="admin"
              user={{ ...user }}
              component={reactfeather}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/cards/basic"
              role="admin"
              user={{ ...user }}
              component={basicCards}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/cards/statistics"
              role="admin"
              user={{ ...user }}
              component={statisticsCards}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/cards/analytics"
              role="admin"
              user={{ ...user }}
              component={analyticsCards}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/cards/action"
              role="admin"
              user={{ ...user }}
              component={actionCards}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/alerts"
              role="admin"
              user={{ ...user }}
              component={Alerts}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/buttons"
              role="admin"
              user={{ ...user }}
              component={Buttons}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/breadcrumbs"
              role="admin"
              user={{ ...user }}
              component={Breadcrumbs}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/carousel"
              role="admin"
              user={{ ...user }}
              component={Carousel}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/collapse"
              role="admin"
              user={{ ...user }}
              component={Collapse}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/dropdowns"
              role="admin"
              user={{ ...user }}
              component={Dropdowns}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/list-group"
              role="admin"
              user={{ ...user }}
              component={ListGroup}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/modals"
              role="admin"
              user={{ ...user }}
              component={Modals}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/pagination"
              role="admin"
              user={{ ...user }}
              component={Pagination}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/nav-component"
              role="admin"
              user={{ ...user }}
              component={NavComponent}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/navbar"
              role="admin"
              user={{ ...user }}
              component={Navbar}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/tabs-component"
              role="admin"
              user={{ ...user }}
              component={Tabs}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/pills-component"
              role="admin"
              user={{ ...user }}
              component={TabPills}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/tooltips"
              role="admin"
              user={{ ...user }}
              component={Tooltips}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/popovers"
              role="admin"
              user={{ ...user }}
              component={Popovers}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/badges"
              role="admin"
              user={{ ...user }}
              component={Badge}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/pill-badges"
              role="admin"
              user={{ ...user }}
              component={BadgePill}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/progress"
              role="admin"
              user={{ ...user }}
              component={Progress}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/media-objects"
              role="admin"
              user={{ ...user }}
              component={Media}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/spinners"
              role="admin"
              user={{ ...user }}
              component={Spinners}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/toasts"
              role="admin"
              user={{ ...user }}
              component={Toasts}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/extra-components/auto-complete"
              role="admin"
              user={{ ...user }}
              component={AutoComplete}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/extra-components/chips"
              role="admin"
              user={{ ...user }}
              component={chips}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/extra-components/divider"
              role="admin"
              user={{ ...user }}
              component={divider}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/wizard"
              role="admin"
              user={{ ...user }}
              component={vuexyWizard}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/select"
              role="admin"
              user={{ ...user }}
              component={select}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/switch"
              role="admin"
              user={{ ...user }}
              component={switchComponent}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/checkbox"
              role="admin"
              user={{ ...user }}
              component={checkbox}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/radio"
              role="admin"
              user={{ ...user }}
              component={radio}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/input"
              role="admin"
              user={{ ...user }}
              component={input}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/input-group"
              role="admin"
              user={{ ...user }}
              component={group}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/extra-components/avatar"
              role="admin"
              user={{ ...user }}
              component={avatar}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/number-input"
              role="admin"
              user={{ ...user }}
              component={numberInput}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/textarea"
              role="admin"
              user={{ ...user }}
              component={textarea}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/pickers"
              role="admin"
              user={{ ...user }}
              component={pickers}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/input-mask"
              role="admin"
              user={{ ...user }}
              component={inputMask}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/layout/form-layout"
              role="admin"
              user={{ ...user }}
              component={layout}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/formik"
              role="admin"
              user={{ ...user }}
              component={formik}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/tables/reactstrap"
              role="admin"
              user={{ ...user }}
              component={tables}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/tables/react-tables"
              role="admin"
              user={{ ...user }}
              component={ReactTables}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/tables/agGrid"
              role="admin"
              user={{ ...user }}
              component={Aggrid}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/tables/data-tables"
              role="admin"
              user={{ ...user }}
              component={DataTable}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/pages/profile"
              role="admin"
              user={{ ...user }}
              component={profile}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/pages/knowledge-base/category"
              role="admin"
              user={{ ...user }}
              component={knowledgeBaseCategory}
              exact
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/pages/knowledge-base/category/questions"
              role="admin"
              user={{ ...user }}
              component={knowledgeBaseQuestion}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/pages/search"
              role="admin"
              user={{ ...user }}
              component={search}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/pages/account-settings"
              role="admin"
              user={{ ...user }}
              component={accountSettings}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/misc/coming-soon"
              role="admin"
              user={{ ...user }}
              component={comingSoon}
              fullLayout
            />
            {/* <RouteConfig isLoggedIn={isLoggedIn} path="/misc/error/404" component={Error404} fullLayout /> */}
            {/* <RouteConfig path="/pages/login" component={Login} fullLayout /> */}
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/pages/register"
              role="admin"
              user={{ ...user }}
              component={register}
              fullLayout
            />
            {/* <RouteConfig
            path="/pages/forgot-password"
            component={forgotPassword}
            fullLayout
          /> */}
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/pages/lock-screen"
              role="admin"
              user={{ ...user }}
              component={lockScreen}
              fullLayout
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/pages/reset-password"
              role="admin"
              user={{ ...user }}
              component={resetPassword}
              fullLayout
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/misc/error/500"
              role="admin"
              user={{ ...user }}
              component={error500}
              fullLayout
            />
            <NotProtexctedRouteConfig
              isLoggedIn={isLoggedIn}
              title="not-authorized"
              path="/misc/not-authorized"
              // role="admin"
              user={{ ...user }}
              component={authorized}
              fullLayout
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/misc/maintenance"
              role="admin"
              user={{ ...user }}
              component={maintenance}
              fullLayout
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/app/user/list"
              role="admin"
              user={{ ...user }}
              component={userList}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/app/user/edit"
              role="admin"
              user={{ ...user }}
              component={userEdit}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/app/user/view"
              role="admin"
              user={{ ...user }}
              component={userView}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/maps/leaflet"
              role="admin"
              user={{ ...user }}
              component={leafletMaps}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/extensions/sweet-alert"
              role="admin"
              user={{ ...user }}
              component={sweetAlert}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/extensions/toastr"
              role="admin"
              user={{ ...user }}
              component={toastr}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/extensions/clipboard"
              role="admin"
              user={{ ...user }}
              component={clipboard}
            />
            {/* <Route component={Error404} fullLayout /> */}

            <Route
              render={(props) => {
                return (
                  <ContextLayout.Consumer>
                    {(context) => {
                      const LayoutTag = context.fullLayout;
                      return (
                        <LayoutTag {...props} permission={props.user}>
                          <Suspense fallback={<Spinner />}>
                            <Error404 {...props} />
                          </Suspense>
                        </LayoutTag>
                      );
                    }}
                  </ContextLayout.Consumer>
                );
              }}
            />
          </Switch>
        </Router>
      </Suspense>
    );
  }
}

export default AppRouter;
