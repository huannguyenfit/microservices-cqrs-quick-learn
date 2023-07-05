import { message } from 'antd';
import React, { useEffect, useState } from 'react';
import { BehaviorSubject } from 'rxjs';
import { ResponseMessage } from '../../models/common/response-message';
import LoadingLogo from '../../../assets/images/logo.svg';
import './loading.scss';

// handle loading
const loadingSubject = new BehaviorSubject<boolean>(false);

export const toggleLoading = (value: boolean) => {
  loadingSubject.next(value);
};

//handle error
const handleMessageSubject = new BehaviorSubject<ResponseMessage | null>(null);

export const toggleMessage = (value: ResponseMessage) => {
  handleMessageSubject.next(value);
};

const Loading = () => {
  const [isLoading, setIsLoading] = useState(false);
  const [count, setCount] = useState(0);

  const toggleLoading = (value: boolean) => {
    if (value) {
      setCount((previous) => previous + 1);
    } else {
      setCount((previous) => (previous > 0 ? previous - 1 : 0));
    }
  };

  useEffect(() => {
    if (count > 0) {
      setIsLoading(true);
    } else {
      setIsLoading(false);
    }
  }, [count]);

  //#region Subscribe Loading
  useEffect(() => {
    const subscribe = loadingSubject.subscribe((value) => {
      toggleLoading(value);
    });
    return () => {
      subscribe.unsubscribe();
    };
  }, []);
  //#endregion Subscribe Loading

  //#region handle error
  useEffect(() => {
    const subscribe = handleMessageSubject.subscribe((msg) => {
      if (msg) {
        message.open({
          type: msg.type,
          content: msg.message,
          key: msg.code,
          duration: msg.type === 'error' ? 3 : 1.5,
          className: 'custom-ant-message',
        });
      }
    });
    return () => {
      subscribe.unsubscribe();
    };
  }, []);
  //#endregion handle error

  return isLoading ? (
    <div className='loading-section'>
      <div className='overlay-background'></div>
      <img src={LoadingLogo} style={{width: "80px", height: "80px"}} alt='logo' />
    </div>
  ) : null;
};

export default Loading;
