import API_PATH from '@core/constants/api-path';
import { getAsync, postAsync, putAsync } from '@core/utils/http-client';
import { AxiosResponse } from 'axios';

const API_URL = import.meta.env.VITE_APP_ORDER_API_GATEWAY_ENDPOINT;


class OrderService {
    placeOrder = (payload): Promise<AxiosResponse<any[]>> => {
        return postAsync(`${API_URL}${API_PATH.PLACE_ORDER}`, payload);
    }
}

export default new OrderService();
