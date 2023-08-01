import API_PATH from '@core/constants/api-path';
import { getAsync, postAsync, putAsync } from '@core/utils/http-client';
import { AxiosResponse } from 'axios';

const API_URL = import.meta.env.VITE_APP_STORE_API_GATEWAY_ENDPOINT;


class StoresService {
    getAll = (skip:number, take: number): Promise<AxiosResponse<any[]>> => {
        return getAsync(`${API_URL}${API_PATH.GET_STORE}?skip=${skip}&take=${take}`, undefined);
    }
    getById = (id): Promise<AxiosResponse<any[]>> => {
        return getAsync(`${API_URL}${API_PATH.GET_STORE_BY_ID}?id=${id}`, undefined);
    }
    
    getProductByStore = (storeId: string, skip:number, take: number): Promise<AxiosResponse<any[]>> => {
        return getAsync(`${API_URL}${API_PATH.GET_PRODUCT_BY_STORE}?id=${storeId}&skip=${skip}&take=${take}`, undefined);
    }
    
    getProductByStoreCountTotalLazy = (storeId: string, skip:number, take: number): Promise<AxiosResponse<any[]>> => {
        return getAsync(`${API_URL}${API_PATH.GET_PRODUCT_BY_STORE_COUNT_TOTAL}?id=${storeId}&skip=${skip}&take=${take}`, undefined);
    }

    add = (payload: any): Promise<AxiosResponse> => {
        return postAsync(API_URL + API_PATH.ADD_STORE, payload);
    }
}

export default new StoresService();
