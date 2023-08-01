import axios, { AxiosInstance, AxiosResponse, ResponseType } from 'axios';
import HttpStatus from 'http-status-codes';
import qs from 'qs';
import cookie from 'react-cookies';
import API_URL from '../constants/url-config';
import { ResponseMessage } from '../models/common/response-message';
import { toggleLoading, toggleMessage } from './loading/loading';

const axiosInstance = (
  handleErrorAutomatic: boolean,
  contentType: string = 'application/json',
  responseType: ResponseType = 'json',
  isShowMessage: boolean = true
): AxiosInstance => {
  const instance = axios.create({
    baseURL: '',
    headers: {
      'Content-Type': contentType,
      Authorization: `Bearer ${cookie.load('ACCESS_TOKEN')}`,
    },
    responseType: responseType,
  });

  instance.interceptors.response.use(
    (response) => {
      const method = response.config.method;
      return response.data;
    },
    (error) => {
      //Handle exception for your business
      toggleLoading(false);

      let _error: ResponseMessage = {
        type: 'error',
        message: '',
        code: '',
      };

      if (
        error.response &&
        (error.response.status === HttpStatus.UNAUTHORIZED || error.response.status === HttpStatus.FORBIDDEN)
      ) {
        handleUnAuthorize();
      }

      return Promise.reject(error);
    }
  );
  return instance;
};

export const getAsync = (
  url: string,
  params?: { [key: string]: any },
  isShowMessage: boolean = true,
  handleErrorAutomatic: boolean = true
) => {
  return axiosInstance(handleErrorAutomatic, 'application/json', 'json', isShowMessage).get(url, {
    params: params,
    paramsSerializer: function (params) {
      return qs.stringify(params, { arrayFormat: 'repeat' });
    },
  });
};

export const postAsync = (url: string, json?: object, isShowMessage: boolean = true, handleErrorAutomatic: boolean = true) => {
  return axiosInstance(handleErrorAutomatic, 'application/json', 'json', isShowMessage).post(url, json);
};

export const putAsync = (
  url: string,
  json?: object,
  successMessage?: string,
  isShowMessage: boolean = true,
  handleErrorAutomatic: boolean = true
) => {
  return axiosInstance(handleErrorAutomatic, 'application/json', 'json', isShowMessage).put(url, json);
};

export const deleteAsync = (url: string, isShowMessage: boolean = true, handleErrorAutomatic: boolean = true) => {
  return axiosInstance(handleErrorAutomatic, 'application/json', 'json', isShowMessage).delete(url);
};

const parseFormdata = (model: any) => {
  const formdata = new FormData();
  Object.keys(model || {}).forEach((p) => {
    if (model[p]) {
      if (Array.isArray(model[p])) {
        (model[p] as Array<any>).forEach((q) => {
          formdata.append(p + '[]', q);
        });
      } else {
        formdata.append(p, model[p]);
      }
    }
  });

  return formdata;
};

function handleUnAuthorize() {
  Object.keys(cookie.loadAll()).forEach((item) => {
    cookie.remove(item);
  });

  localStorage.clear();
  sessionStorage.clear();

  // redirect to sign in page
  if (window.location.href.indexOf('/login') === -1) {
    window.location.href = `/login?url=${window.location.pathname + window.location.search}`;
  }
}
