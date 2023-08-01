import { Button, Col, Form, Input, Row, Space } from 'antd';
import { useDispatch } from 'react-redux';
import './login.scss';
import { useState, useEffect } from 'react';


const tailLayout = {
  wrapperCol: { span: 24 },
  style: { marginTop: '50px' },
};

export default function Login(props) {
  const [redirectPath, setRedirectPath] = useState('');

  const onFinish = async (values: any) => {
    //Login and set cookie
  };

  const onFinishFailed = (errorInfo: any) => {
    console.log('Failed:', errorInfo);
  };

  return (
    <Row className='login-form'>
      <Row className='content'>
        <Col span={24}>
          <Col span={24} className='login-title'>
            <Space size='middle' direction='vertical'>
              <Col span={24} className='big-title fw-bold'>
                Login
              </Col>
              <Col span={24} className='description'>
                Login to the organization's application portal
              </Col>
            </Space>
          </Col>
          <Form layout='vertical' name='login-form' size='large' onFinish={onFinish} onFinishFailed={onFinishFailed}>
            <Form.Item label='Username' name='username' rules={[{ required: true, message: 'Please input your username!' }]}>
              <Input />
            </Form.Item>

            <Form.Item label='Password' name='password' rules={[{ required: true, message: 'Please input your password!' }]}>
              <Input.Password />
            </Form.Item>

            <Form.Item {...tailLayout}>
              <Button type='primary' htmlType='submit' className='full-width'>
                Login
              </Button>
            </Form.Item>
          </Form>
        </Col>
      </Row>
      <Row className='copy-rights' justify='center'>
        © 2022 Demo Vietnam. All rights reserved.
      </Row>
    </Row>
  );
}
