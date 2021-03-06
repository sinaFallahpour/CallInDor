import React from 'react'
import auth from '../../services/authService'
import { Route, Redirect } from "react-router-dom";

const  ProtectedRout = ({ path, component:Component, render,...rest }) => {
    return ( 
        <Route 
        path={ path }
        {...rest}
        render={props =>{
          if(!auth.getCurrentUser()) {
              return <Redirect to={{
                pathname:"/login",
                state:{from: props.location }
              }}/>
          }  
          else{
              return Component ? <Component {...props}/> : render(props)
          }
            
        }}
      />
     );
}
 
export default ProtectedRout;