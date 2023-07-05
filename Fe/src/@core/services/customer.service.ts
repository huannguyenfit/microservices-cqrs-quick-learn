import API_PATH from '../constants/api-path';
import { getAsync, postAsync, putAsync } from '../utils/http-client';
import { Observable } from 'rxjs';
import moment, { Moment } from 'moment';
import { AxiosResponse } from 'axios';

const API_URL = import.meta.env.VITE_APP_CUSTOMER_API_GATEWAY_ENDPOINT;


class CustomerService {
    getAll = (skip:number, take: number) => {
        return getAsync(`${API_URL}${API_PATH.GET_CUSTOMER}?skip=${skip}&take=${take}`, undefined);
    }

    getById = (id): Promise<AxiosResponse<any[]>> => {
        return getAsync(`${API_URL}${API_PATH.GET_CUSTOMER_BY_ID}?id=${id}`, undefined);
    }

    add = (payload: any): Promise<AxiosResponse> => {
        return postAsync(API_URL + API_PATH.ADD_CUSTOMER, payload);
    }

    getOrders = (customerId: any): Promise<AxiosResponse> => {
        return getAsync(`${API_URL}${API_PATH.GET_ORDER_CUSTOMER}?id=${customerId}`);
    }

}

export default new CustomerService();
