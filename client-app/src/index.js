import React, { Suspense, lazy } from "react";
import ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { IntlProviderWrapper } from "./utility/context/Internationalization";
import { Layout } from "./utility/context/Layout";
import * as serviceWorker from "./serviceWorker";
import { store } from "./redux/storeConfig/store";
import Spinner from "./components/@vuexy/spinner/Fallback-spinner";
import "./index.scss";
import "./@fake-db";
// import "../../assets/scss/plugins/extensions/toastr.scss";
import "./assets/scss/plugins/extensions/toastr.scss";
import { ToastContainer } from "react-toastify";

import "react-toastify/dist/ReactToastify.css";
const LazyApp = lazy(() => import("./App"));

// configureDatabase()

ReactDOM.render(
  <Provider store={store}>
    <Suspense fallback={<Spinner />}>
      <Layout>
        <IntlProviderWrapper>
          <ToastContainer />
          <LazyApp />
        </IntlProviderWrapper>
      </Layout>
    </Suspense>
  </Provider>,
  document.getElementById("root")
);

serviceWorker.unregister();
