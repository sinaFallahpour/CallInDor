import React, { Suspense, lazy } from "react";
import { Router, Switch, Route, Redirect } from "react-router-dom";
import { history } from "./history";
// import { connect } from "react-redux";
// import PropTypes from 'prop-types';
import auth from "./core/services/userService/authService";

import Spinner from "./components/@vuexy/spinner/Loading-spinner";
import knowledgeBaseCategory from "./views/pages/knowledge-base/Category";
import knowledgeBaseQuestion from "./views/pages/knowledge-base/Questions";
import { ContextLayout } from "./utility/context/Layout";

// Route-based code splitting
const Categories = lazy(() => import("./views/pages/Categories/Categories"));
const EditCategory = lazy(() => import("./views/pages/Categories/EditCategory"));

const Services = lazy(() => import("./views/pages/_Serices/Services"));
const CreateService = lazy(() => import("./views/pages/_Serices/Create"));
const EditService = lazy(() => import("./views/pages/_Serices/EditService"));

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
const error404 = lazy(() => import("./views/pages/misc/error/404"));
const error500 = lazy(() => import("./views/pages/misc/error/500"));
const authorized = lazy(() => import("./views/pages/misc/NotAuthorized"));
const maintenance = lazy(() => import("./views/pages/misc/Maintenance"));

const leafletMaps = lazy(() => import("./views/maps/Maps"));
const toastr = lazy(() => import("./extensions/toastify/Toastify"));
const sweetAlert = lazy(() => import("./extensions/sweet-alert/SweetAlert"));

 
const clipboard = lazy(() =>
  import("./extensions/copy-to-clipboard/CopyToClipboard")
);

const swiper = lazy(() => import("./extensions/swiper/Swiper")); 

const userList = lazy(() => import("./views/apps/user/list/List"));
const userEdit = lazy(() => import("./views/apps/user/edit/Edit"));
const userView = lazy(() => import("./views/apps/user/view/View"));
const Login = lazy(() => import("./views/pages/authentication/login/Login"));
const forgotPassword = lazy(() =>
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

// Set Layout and Component Using App Route
const RouteConfig = ({ component: Component, fullLayout, ...rest }) => (
  <Route
    {...rest}
    render={(props) => {
      if (!auth.getCurrentUser()) {
        return <Redirect exact to={{
          pathname: "/pages/login",
          state: {from: props.location }
        }} />;
      }
      else {
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
                <LayoutTag {...props} permission={props.user}>
                  <Suspense fallback={<Spinner />}>
                    <Component {...props} />
                  </Suspense>
                </LayoutTag>
              );
            }}
          </ContextLayout.Consumer>
        );
      }
      // return (
      //   <ContextLayout.Consumer>
      //     {(context) => {
      //       const LayoutTag =
      //         fullLayout === true
      //           ? context.fullLayout
      //           : context.state.activeLayout === "horizontal"
      //             ? context.horizontalLayout
      //             : context.VerticalLayout;
      //       return (
      //         <LayoutTag {...props} permission={props.user}>
      //           <Suspense fallback={<Spinner />}>
      //             <Component {...props} />
      //           </Suspense>
      //         </LayoutTag>
      //       );
      //     }}
      //   </ContextLayout.Consumer>
      // );
    }}
  />
);

// const mapStateToProps = (state) => {
//   return {
//     user: state.auth.login.userRole
//   };
// };

// const AppRoute = connect(mapStateToProps)(RouteConfig);

class AppRouter extends React.Component {
  render () {
    return (
      // Set the directory path if you are deploying in sub-folder
      <Router history={history}>
        <Switch>
          <Route path="/pages/login" component={Login} fullLayout />
          <RouteConfig exact path="/pages/categories" component={Categories} />
          <RouteConfig exact path="/pages/category/:id" component={EditCategory} />

          <RouteConfig exact path="/pages/Services" component={Services} />
          <RouteConfig exact path="/pages/Services/Create" component={CreateService} />
          <RouteConfig exact path="/pages/Services/:id" component={EditService} />



          <RouteConfig exact path="/" component={analyticsDashboard} />
          <RouteConfig
            path="/email"
            exact
            component={() => <Redirect to="/email/inbox" />}
          />
          <RouteConfig path="/email/:filter" component={email} />
          <RouteConfig
            path="/todo"
            exact
            component={() => <Redirect to="/todo/all" />}
          />
          <RouteConfig path="/todo/:filter" component={todo} />
          <RouteConfig path="/calendar" component={calendar} />
          <RouteConfig path="/data-list/list-view" component={listView} />
          <RouteConfig path="/data-list/thumb-view" component={thumbView} />
          <RouteConfig path="/ui-element/grid" component={grid} />
          <RouteConfig path="/ui-element/typography" component={typography} />
          <RouteConfig
            path="/ui-element/textutilities"
            component={textutilities}
          />
          <RouteConfig
            path="/ui-element/syntaxhighlighter"
            component={syntaxhighlighter}
          />
          <RouteConfig path="/colors/colors" component={colors} />
          <RouteConfig path="/icons/reactfeather" component={reactfeather} />
          <RouteConfig path="/cards/basic" component={basicCards} />
          <RouteConfig path="/cards/statistics" component={statisticsCards} />
          <RouteConfig path="/cards/analytics" component={analyticsCards} />
          <RouteConfig path="/cards/action" component={actionCards} />
          <RouteConfig path="/components/alerts" component={Alerts} />
          <RouteConfig path="/components/buttons" component={Buttons} />
          <RouteConfig path="/components/breadcrumbs" component={Breadcrumbs} />
          <RouteConfig path="/components/carousel" component={Carousel} />
          <RouteConfig path="/components/collapse" component={Collapse} />
          <RouteConfig path="/components/dropdowns" component={Dropdowns} />
          <RouteConfig path="/components/list-group" component={ListGroup} />
          <RouteConfig path="/components/modals" component={Modals} />
          <RouteConfig path="/components/pagination" component={Pagination} />
          <RouteConfig path="/components/nav-component" component={NavComponent} />
          <RouteConfig path="/components/navbar" component={Navbar} />
          <RouteConfig path="/components/tabs-component" component={Tabs} />
          <RouteConfig path="/components/pills-component" component={TabPills} />
          <RouteConfig path="/components/tooltips" component={Tooltips} />
          <RouteConfig path="/components/popovers" component={Popovers} />
          <RouteConfig path="/components/badges" component={Badge} />
          <RouteConfig path="/components/pill-badges" component={BadgePill} />
          <RouteConfig path="/components/progress" component={Progress} />
          <RouteConfig path="/components/media-objects" component={Media} />
          <RouteConfig path="/components/spinners" component={Spinners} />
          <RouteConfig path="/components/toasts" component={Toasts} />
          <RouteConfig
            path="/extra-components/auto-complete"
            component={AutoComplete}
          />
          <RouteConfig path="/extra-components/avatar" component={avatar} />
          <RouteConfig path="/extra-components/chips" component={chips} />
          <RouteConfig path="/extra-components/divider" component={divider} />
          <RouteConfig path="/forms/wizard" component={vuexyWizard} />
          <RouteConfig path="/forms/elements/select" component={select} />
          <RouteConfig path="/forms/elements/switch" component={switchComponent} />
          <RouteConfig path="/forms/elements/checkbox" component={checkbox} />
          <RouteConfig path="/forms/elements/radio" component={radio} />
          <RouteConfig path="/forms/elements/input" component={input} />
          <RouteConfig path="/forms/elements/input-group" component={group} />
          <RouteConfig
            path="/forms/elements/number-input"
            component={numberInput}
          />
          <RouteConfig path="/forms/elements/textarea" component={textarea} />
          <RouteConfig path="/forms/elements/pickers" component={pickers} />
          <RouteConfig path="/forms/elements/input-mask" component={inputMask} />
          <RouteConfig path="/forms/layout/form-layout" component={layout} />
          <RouteConfig path="/forms/formik" component={formik} />
          <RouteConfig path="/tables/reactstrap" component={tables} />
          <RouteConfig path="/tables/react-tables" component={ReactTables} />
          <RouteConfig path="/tables/agGrid" component={Aggrid} />
          <RouteConfig path="/tables/data-tables" component={DataTable} />
          <RouteConfig path="/pages/profile" component={profile} />
          <RouteConfig path="/pages/knowledge-base" exact />
          <RouteConfig
            path="/pages/knowledge-base/category"
            component={knowledgeBaseCategory}
            exact
          />
          <RouteConfig
            path="/pages/knowledge-base/category/questions"
            component={knowledgeBaseQuestion}
          />
          <RouteConfig path="/pages/search" component={search} />
          <RouteConfig
            path="/pages/account-settings"
            component={accountSettings}
          />
          <RouteConfig
            path="/misc/coming-soon"
            component={comingSoon}
            fullLayout
          />
          <RouteConfig path="/misc/error/404" component={error404} fullLayout />
          {/* <RouteConfig path="/pages/login" component={Login} fullLayout /> */}
          <RouteConfig path="/pages/register" component={register} fullLayout />
          <RouteConfig
            path="/pages/forgot-password"
            component={forgotPassword}
            fullLayout
          />
          <RouteConfig
            path="/pages/lock-screen"
            component={lockScreen}
            fullLayout
          />
          <RouteConfig
            path="/pages/reset-password"
            component={resetPassword}
            fullLayout
          />
          <RouteConfig path="/misc/error/500" component={error500} fullLayout />
          <RouteConfig
            path="/misc/not-authorized"
            component={authorized}
            fullLayout
          />
          <RouteConfig
            path="/misc/maintenance"
            component={maintenance}
            fullLayout
          />
          <RouteConfig path="/app/user/list" component={userList} />
          <RouteConfig path="/app/user/edit" component={userEdit} />
          <RouteConfig path="/app/user/view" component={userView} />
          
          
          
         
          
          
          
          <RouteConfig path="/maps/leaflet" component={leafletMaps} />
          <RouteConfig path="/extensions/sweet-alert" component={sweetAlert} />
          <RouteConfig path="/extensions/toastr" component={toastr} />

 
          <RouteConfig path="/extensions/clipboard" component={clipboard} />
          <RouteConfig component={error404} fullLayout />
        </Switch>
      </Router>
    );
  }
}

export default AppRouter;
