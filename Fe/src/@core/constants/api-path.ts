const API_PATH = {
  LOGIN: '/connect/token',

  GET_CUSTOMER: '/customer/getall',
  GET_CUSTOMER_BY_ID: '/customer/GetById',

  GET_ORDER_CUSTOMER: '/customer/GetOrderByCustomerId',
  ADD_CUSTOMER: '/customer/add',

  GET_STORE: '/store/getall',
  GET_STORE_BY_ID: '/store/GetById',
  ADD_STORE: '/store/add',
  GET_PRODUCT_BY_STORE: '/store/GetProductByStoreId',
  GET_PRODUCT_BY_STORE_COUNT_TOTAL: '/store/GetProductByStoreIdCountTotal',
  
  GET_PRODUCT: '/product/getall',
  GET_PRODUCT_COUNT_TOTAL: '/product/GetAllCountTotal',
  ADD_PRODUCT: '/product/add',
  SEARCH_PRODUCT: '/product/search',
  SEARCH_PRODUCT_COUNT_TOTAL: '/product/searchCountTotal',


  PLACE_ORDER: '/order/PlaceOrder',

}

export default API_PATH;
