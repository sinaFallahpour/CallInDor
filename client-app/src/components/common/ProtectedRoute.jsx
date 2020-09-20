// import React from 'react'
// import auth from '../../core/services/userService/authService'
// import { Route, Redirect } from "react-router-dom";

// const RouteConfig = ({ component: Component, fullLayout, ...rest }) => (
//     <Route
//         {...rest}
//         render={(props) => {
//             if (!auth.getCurrentUser()) {
//                 return <Redirect to={{
//                     pathname: "/login",
//                     state: { from: props.location }
//                 }} />;
//             }
//             else {
//                 // return Component ? <Component {...props} /> : render(props)
//                 return (
//                     <ContextLayout.Consumer>
//                         {(context) => {
//                             const LayoutTag =
//                                 fullLayout === true
//                                     ? context.fullLayout
//                                     : context.state.activeLayout === "horizontal"
//                                         ? context.horizontalLayout
//                                         : context.VerticalLayout;
//                             return (
//                                 <LayoutTag {...props} permission={props.user}>
//                                     <Suspense fallback={<Spinner />}>
//                                         <Component {...props} />
//                                     </Suspense>
//                                 </LayoutTag>
//                             );
//                         }}
//                     </ContextLayout.Consumer>
//                 );
//             }
//             // return (
//             //   <ContextLayout.Consumer>
//             //     {(context) => {
//             //       const LayoutTag =
//             //         fullLayout === true
//             //           ? context.fullLayout
//             //           : context.state.activeLayout === "horizontal"
//             //             ? context.horizontalLayout
//             //             : context.VerticalLayout;
//             //       return (
//             //         <LayoutTag {...props} permission={props.user}>
//             //           <Suspense fallback={<Spinner />}>
//             //             <Component {...props} />
//             //           </Suspense>
//             //         </LayoutTag>
//             //       );
//             //     }}
//             //   </ContextLayout.Consumer>
//             // );
//         }}
//     />
// );



// const mapStateToProps = (state) => {
//     return {
//       user: state.auth.login.userRole
//     };
//   };
// const AppRoute = connect(mapStateToProps)(RouteConfig);

// export default ProtectedRout;