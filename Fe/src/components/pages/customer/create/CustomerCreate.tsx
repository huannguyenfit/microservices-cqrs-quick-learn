import { Badge, Button, Checkbox, Col, Drawer, Form, Input, InputNumber, Row, Select, Space, Table, DatePicker } from 'antd';
import { useEffect, useState } from 'react';
import customerService from '@core/services/customer.service';
import { PlusOutlined } from '@ant-design/icons';
import { toggleLoading, toggleMessage } from '@core/utils/loading/loading';
const { Option } = Select;
export default function CustomerCreateComponent(props: any) {
  const [currentPage, setCurrentPage] = useState(1);
  const [visible, setVisible] = useState(false);
  const [createForm] = Form.useForm();

  const showDrawer = () => {
    setVisible(true);
  };
  const onClose = () => {
    setVisible(false);
  };
  const submit = () => {
    createForm.validateFields().then(async (value: any) => {
      toggleLoading(true);
      let payload = createForm.getFieldsValue();
      customerService.add(payload).then((res: any) => {
        resetFrom();
        props.refreshGridView();
        toggleMessage({ message: 'Create successfully', type: 'success' });
        setVisible(false);
        toggleLoading(false);
      });
    });
  };

  const resetFrom = () => {
    createForm.resetFields();
  };

  return (
    <>
      <Button type='primary' className='add-new' onClick={showDrawer}>
        <PlusOutlined />
        {'Add New'}
      </Button>

      <Drawer
        title={'Add Customer'}
        width={768}
        onClose={onClose}
        visible={visible}
        className='customer-create'
        bodyStyle={{ paddingBottom: 80 }}
        footer={
          <div className='d-flex justify-between align-center'>
            <Button className='btn-cancel' onClick={onClose} style={{ marginRight: 8 }}>
              {'Cancel'}
            </Button>
            <Button type='primary' onClick={submit}>
              {'Save'}
            </Button>
          </div>
        }
      >
        <Form layout='vertical' form={createForm} hideRequiredMark>
          <Row gutter={24}>
            <Col span={24}>
              <Form.Item name='FullName' label={'Full Name'} rules={[{ required: true, message: 'FullName is required!' }]}>
                <Input placeholder={'Full Name'} />
              </Form.Item>
            </Col>
          </Row>
          <Row gutter={24}>
            <Col span={12}>
              <Form.Item name='DOB' label={'Date of Birth'} rules={[{ required: true, message: 'DOB is required!' }]}>
                <DatePicker style={{ width: '100%' }} placeholder={'Select Date'} />
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item
                name='Email'
                label={'Email'}
                rules={[{ required: true, type: 'email', message: 'The input is not valid E-mail!' }]}
              >
                <Input placeholder={'Email'} />
              </Form.Item>
            </Col>
          </Row>
        </Form>
      </Drawer>
    </>
  );
}
