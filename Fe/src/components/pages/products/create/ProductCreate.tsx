import { Badge, Button, Checkbox, Col, Drawer, Form, Input, InputNumber, Row, Select, Space, Table, DatePicker } from 'antd';
import { useEffect, useState } from 'react';
import { PlusOutlined } from '@ant-design/icons';
import { toggleMessage } from '@core/utils/loading/loading';
import productService from '@core/services/product.service';
interface IProps {
  storeId: string;
  refreshGridView: () => void;
}
export default function ProductCreateComponent(props: IProps) {
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

      let payload = createForm.getFieldsValue();
      if (props?.storeId) {
        payload.StoreId = parseInt(props?.storeId);
      }
      productService.add(payload).then((res: any) => {
        resetFrom();
        props.refreshGridView();
        toggleMessage({ message: 'Create successfully', type: 'success' });
        setVisible(false);
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
        {'Add Product'}
      </Button>

      <Drawer
        title={'Add Product'}
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
            <Col span={12}>
              <Form.Item name='Price' label={'Price'} rules={[{ required: true, message: 'Price is required!' }]}>
                <InputNumber
                style={{width: '100%'}}
                  formatter={(value) => `${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')}
                  parser={(value) => value.replace(/\$\s?|(,*)/g, '')}
                  placeholder={'Price'}
                />
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item name='Quantity' label={'StockQuantity'} rules={[{ required: true, message: 'Quantity is required!' }]}>
                <InputNumber
                               style={{width: '100%'}}

                  formatter={(value) => `${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')}
                  parser={(value) => value.replace(/\$\s?|(,*)/g, '')}
                  placeholder={'Price'}
                />
              </Form.Item>
            </Col>
          </Row>
        </Form>
      </Drawer>
    </>
  );
}
