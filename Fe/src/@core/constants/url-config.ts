const API_URL = import.meta.env.VITE_APP_API_ENDPOINT;

enum ROUTE_PATHS {
  Home = '/home',
  Login = '/login',
  CustomerList = '/customers',
  MyOrders = '/my-orders',

  StoreList = '/stores',
  StoreDetail = '/stores/detail',
  ProductList= '/products',
  ExploreShopping = '/explore-shopping'
}
enum ROUTE_DYNAMIC_VARIABLE {
  'id' = ':id',
}

export default API_URL;

export { ROUTE_PATHS, ROUTE_DYNAMIC_VARIABLE};
