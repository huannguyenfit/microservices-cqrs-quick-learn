import { BrowserRouter, Route, Navigate, Routes } from "react-router-dom";
import {
  ROUTE_DYNAMIC_VARIABLE,
  ROUTE_PATHS,
} from "./@core/constants/url-config";
import Loading from "./@core/utils/loading/loading";
import MainLayout from "views/layout/main/main";
import CustomerComponent from "components/pages/customer/Customers";
import StoresComponent from "components/pages/stores/Stores";
import ProductComponent from "components/pages/products/Products";
import StoreProductListComponent from "components/pages/stores/StoresProductList";
import MyOrdersComponent from "components/pages/customer/my-orders/MyOrders";
import ExploreShoppingComponent from "components/pages/explorer/ExploreShopping";

import { AuthRoute } from "@core/components/AuthRoute";
import Login from "components/pages/login/login";
import AuthLayout from "views/layout/auth/auth";

function App() {
  return (
    <div className='App'>
      <Loading />
      <BrowserRouter>
        <Routes>
          <Route index element={<Navigate to={ROUTE_PATHS.CustomerList} />} />
          <Route path='/' element={<AuthRoute />}>
            <Route element={<MainLayout />}>
              <Route
                path={ROUTE_PATHS.CustomerList}
                element={<CustomerComponent />}
              />
              <Route
                path={`${ROUTE_PATHS.MyOrders}/${ROUTE_DYNAMIC_VARIABLE.id}`}
                element={<MyOrdersComponent />}
              />
              <Route
                path={ROUTE_PATHS.StoreList}
                element={<StoresComponent />}
              />
              <Route
                path={`${ROUTE_PATHS.StoreDetail}/${ROUTE_DYNAMIC_VARIABLE.id}`}
                element={<StoreProductListComponent />}
              />
              <Route
                path={`${ROUTE_PATHS.ExploreShopping}/${ROUTE_DYNAMIC_VARIABLE.id}`}
                element={<ExploreShoppingComponent />}
              />
              <Route
                path={ROUTE_PATHS.ProductList}
                element={<ProductComponent />}
              />
            </Route>
          </Route>
          <Route path='/' element={<AuthLayout />}>
            <Route path={ROUTE_PATHS.Login} element={<Login />} />
          </Route>

          <Route
            path='*'
            element={<Navigate to={ROUTE_PATHS.CustomerList} />}
          />
        </Routes>
      </BrowserRouter>
    </div>
  );
}
export default App;
