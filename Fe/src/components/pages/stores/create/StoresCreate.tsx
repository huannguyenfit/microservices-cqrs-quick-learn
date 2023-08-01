import { Badge, Button, Checkbox, Col, Drawer, Form, Input, InputNumber, Row, Select, Space, Table, DatePicker } from 'antd';
import { useEffect, useState } from 'react';
import { PlusOutlined } from '@ant-design/icons';
import storesServices from '@core/services/stores.service';
import { toggleLoading, toggleMessage } from '@core/utils/loading/loading';
const { Option } = Select;
export default function StoresCreateComponent(props: any) {
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
      storesServices.add(payload).then((res: any) => {
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
        title={'Add Shop'}
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
            <Col span={12}>
              <Form.Item name='Name' label={'Name'} rules={[{ required: true, message: 'Name is required!' }]}>
                <Input placeholder={'Name'} />
              </Form.Item>
            </Col>
          </Row>
          <Row gutter={24}>
            <Col span={24}>
              <Form.Item name='Location' label={'Location'} rules={[{ required: true, message: 'Location is required!' }]}>
                <Input placeholder={'Location'} />
              </Form.Item>
            </Col>
          </Row>
        </Form>
      </Drawer>
    </>
  );
}
