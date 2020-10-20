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
const Services = lazy(() => import("./views/pages/_Serices/Services"));
const CreateService = lazy(() => import("./views/pages/_Serices/Create"));
const EditService = lazy(() => import("./views/pages/_Serices/EditService"));

const RoleManager = lazy(() =>
  import("./views/pages/_rolemanager/Rolemanager")
);
const UserManager = lazy(() => import("./views/pages/_userManager/Users"));

const Categories = lazy(() => import("./views/pages/Categories/Categories"));
const EditCategory = lazy(() =>
  import("./views/pages/Categories/EditCategory")
);

const Areas = lazy(() => import("./views/pages/_Areas/Areas"));
// const CreateArea = lazy(() => import("./views/pages/_Areas/Create"));
const EditArea = lazy(() => import("./views/pages/_Areas/EditService"));

const Test = lazy(() => import("./views/pages/_test/Test"));

// Set Layout and Component Using App Route
const RouteConfig = ({
  component: Component,
  fullLayout,
  isLoggedIn,
  title,
  ...rest
}) => (
  <Route
    {...rest}
    render={(props) => {
      if (!isLoggedIn) {
        return (
          <Redirect
            exact
            to={{
              pathname: "/pages/login",
              state: { from: props.location },
            }}
          />
        );
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
  };

  async componentDidMount() {
    const isLoggedIn = await auth.isAdminLoggedIn();
    this.setState({ isLoggedIn, loading: false });
  }

  render() {
    const { isLoggedIn, loading } = this.state;

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
              component={UserManager}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="manage role"
              exact
              path="/pages/Roles"
              component={RoleManager}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="categories"
              exact
              path="/pages/categories"
              component={Categories}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Edit Category"
              exact
              path="/pages/category/:id"
              component={EditCategory}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Areas"
              exact
              path="/pages/areas"
              component={Areas}
            />
            {/* <RouteConfig isLoggedIn={isLoggedIn} title="Create Area" exact path="/pages/areas/Create" component={CreateArea} /> */}
            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Edit Area"
              exact
              path="/pages/areas/:id"
              component={EditArea}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Services"
              exact
              path="/pages/Services"
              component={Services}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Create Service"
              exact
              path="/pages/Services/Create"
              component={CreateService}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              title="Edit Service"
              exact
              path="/pages/Services/:id"
              component={EditService}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              exact
              path="/pages/Test"
              component={Test}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              exact
              path="/"
              component={analyticsDashboard}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/email"
              exact
              component={() => <Redirect to="/email/inbox" />}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/email/:filter"
              component={email}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/todo"
              exact
              component={() => <Redirect to="/todo/all" />}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/todo/:filter"
              component={todo}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/calendar"
              component={calendar}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/data-list/list-view"
              component={listView}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/data-list/thumb-view"
              component={thumbView}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/ui-element/grid"
              component={grid}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/ui-element/typography"
              component={typography}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/ui-element/textutilities"
              component={textutilities}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/ui-element/syntaxhighlighter"
              component={syntaxhighlighter}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/colors/colors"
              component={colors}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/icons/reactfeather"
              component={reactfeather}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/cards/basic"
              component={basicCards}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/cards/statistics"
              component={statisticsCards}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/cards/analytics"
              component={analyticsCards}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/cards/action"
              component={actionCards}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/alerts"
              component={Alerts}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/buttons"
              component={Buttons}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/breadcrumbs"
              component={Breadcrumbs}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/carousel"
              component={Carousel}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/collapse"
              component={Collapse}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/dropdowns"
              component={Dropdowns}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/list-group"
              component={ListGroup}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/modals"
              component={Modals}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/pagination"
              component={Pagination}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/nav-component"
              component={NavComponent}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/navbar"
              component={Navbar}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/tabs-component"
              component={Tabs}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/pills-component"
              component={TabPills}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/tooltips"
              component={Tooltips}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/popovers"
              component={Popovers}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/badges"
              component={Badge}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/pill-badges"
              component={BadgePill}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/progress"
              component={Progress}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/media-objects"
              component={Media}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/spinners"
              component={Spinners}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/components/toasts"
              component={Toasts}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/extra-components/auto-complete"
              component={AutoComplete}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/extra-components/chips"
              component={chips}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/extra-components/divider"
              component={divider}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/wizard"
              component={vuexyWizard}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/select"
              component={select}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/switch"
              component={switchComponent}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/checkbox"
              component={checkbox}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/radio"
              component={radio}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/input"
              component={input}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/input-group"
              component={group}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/extra-components/avatar"
              component={avatar}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/number-input"
              component={numberInput}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/textarea"
              component={textarea}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/pickers"
              component={pickers}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/elements/input-mask"
              component={inputMask}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/layout/form-layout"
              component={layout}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/forms/formik"
              component={formik}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/tables/reactstrap"
              component={tables}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/tables/react-tables"
              component={ReactTables}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/tables/agGrid"
              component={Aggrid}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/tables/data-tables"
              component={DataTable}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/pages/profile"
              component={profile}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/pages/knowledge-base"
              exact
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/pages/knowledge-base/category"
              component={knowledgeBaseCategory}
              exact
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/pages/knowledge-base/category/questions"
              component={knowledgeBaseQuestion}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/pages/search"
              component={search}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/pages/account-settings"
              component={accountSettings}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/misc/coming-soon"
              component={comingSoon}
              fullLayout
            />
            {/* <RouteConfig isLoggedIn={isLoggedIn} path="/misc/error/404" component={Error404} fullLayout /> */}
            {/* <RouteConfig path="/pages/login" component={Login} fullLayout /> */}
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/pages/register"
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
              component={lockScreen}
              fullLayout
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/pages/reset-password"
              component={resetPassword}
              fullLayout
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/misc/error/500"
              component={error500}
              fullLayout
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/misc/not-authorized"
              component={authorized}
              fullLayout
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/misc/maintenance"
              component={maintenance}
              fullLayout
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/app/user/list"
              component={userList}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/app/user/edit"
              component={userEdit}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/app/user/view"
              component={userView}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/maps/leaflet"
              component={leafletMaps}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/extensions/sweet-alert"
              component={sweetAlert}
            />
            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/extensions/toastr"
              component={toastr}
            />

            <RouteConfig
              isLoggedIn={isLoggedIn}
              path="/extensions/clipboard"
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
