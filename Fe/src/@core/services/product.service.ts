import API_PATH from '@core/constants/api-path';
import { getAsync, postAsync, putAsync } from '@core/utils/http-client';
import { AxiosResponse } from 'axios';

const API_URL = import.meta.env.VITE_APP_STORE_API_GATEWAY_ENDPOINT;


class ProductService {
    getAll = (keyword: string, minPrice: number, maxPrice: number, skip: number = 0, take: number = 100) => {
        return getAsync(`${API_URL}${API_PATH.GET_PRODUCT}?keyword=${keyword}&minPrice=${minPrice}&maxPrice=${maxPrice}&skip=${skip}&take=${take}`, undefined);
    }

    getAllCountTotalLazy = (keyword: string, minPrice: number, maxPrice: number, skip: number = 0, take: number = 100) => {
        return getAsync(`${API_URL}${API_PATH.GET_PRODUCT_COUNT_TOTAL}?keyword=${keyword}&minPrice=${minPrice}&maxPrice=${maxPrice}&skip=${skip}&take=${take}`, undefined);
    }

    add = (payload: any): Promise<AxiosResponse> => {
        return postAsync(API_URL + API_PATH.ADD_PRODUCT, payload);
    }

    search = (keyword: string, minPrice: number, maxPrice: number, skip: number = 0, take: number = 100)  => {
        return getAsync(`${API_URL}${API_PATH.SEARCH_PRODUCT}?keyword=${keyword}&minPrice=${minPrice}&maxPrice=${maxPrice}&skip=${skip}&take=${take}`, undefined);
    }

    searchCountTotalLazy = (keyword: string, minPrice: number, maxPrice: number, skip: number = 0, take: number = 100)  => {
        return getAsync(`${API_URL}${API_PATH.SEARCH_PRODUCT_COUNT_TOTAL}?keyword=${keyword}&minPrice=${minPrice}&maxPrice=${maxPrice}&skip=${skip}&take=${take}`, undefined);
    }

    checkMasterDataEnough = ()  => {
        return getAsync(`${API_URL}/product/CheckMasterDataEnough`, undefined);
    }

}

export default new ProductService();
