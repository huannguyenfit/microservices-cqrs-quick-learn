import { Badge, Breadcrumb, Button, Col, Form, Input, Row, Select, Space, Table } from 'antd';
import { useEffect, useState } from 'react';

import customerService from '@core/services/customer.service';
import moment from 'moment';
import { useParams } from 'react-router';
import { toggleLoading } from '@core/utils/loading/loading';

export default function MyOrdersComponent(props) {
  const [currentPage, setCurrentPage] = useState(1);
  const [orders, setOrders] = useState([]);
  const [customer, setCustomer] = useState(null);

  const [skipPage, setSkipPage] = useState(0);
  const [total, setTotal] = useState(0);
  const pageSize = 50;
  const { id } = useParams();

  useEffect(() => {
    getOrders();
    getCustomer();
  }, []);

  const getOrders = async () => {
    toggleLoading(true);
    const data: any = await customerService.getOrders(id).finally(() => toggleLoading(false));
    if (data) {
      setOrders(data);
      setTotal(data.length);
    }
  };

  const getCustomer = async () => {
    customerService.getById(id).then((data: any) => {
      setCustomer(data);
    });
  };

  const columns = [
    {
      title: 'Order #',
      dataIndex: 'id',
      width: '90px',
    },
    {
      title: 'Customer Name',
      dataIndex: '',
      width: '300px',
      render: (_: any, record: any) => (
        <div className='d-flex'>
          {record.customerName} -
          <span>{record.email}</span>
        </div>
      ),
    },
    {
      title: 'Shop Name',
      dataIndex: '',
      width: '300px',
      render: (_: any, record: any) => (
        <div className='d-flex'>
          {record.storeName} - 
          <span>{record.storeLocation}</span>
        </div>
      ),
    },
    {
      title: 'Product Name',
      dataIndex: '',
      width: '20%',
      render: (_: any, record: any) => (
        <div className='d-flex'>
          {record.productName} - 
          <span>{record.price?.toFixed(2)}</span>
        </div>
      ),
    },
  ];

  const onGridChange = async (_pagination: any, _filters: any, sorter: any) => {
    const nextPage = (_pagination.current - 1) * pageSize;
    setSkipPage(nextPage);
    getOrders();
    setCurrentPage(_pagination.current);
  };

  return (
    <>
      <Breadcrumb className='breadcrumb'>
        <Breadcrumb.Item>{'Home'}</Breadcrumb.Item>
        <Breadcrumb.Item>{'My Orders'}</Breadcrumb.Item>
      </Breadcrumb>
      <div className='data-list page-content employee'>
        <div className='list-header'>
          <Row>
            <Col span={12}>
              <h1 className='page-title'>{customer?.fullName} - My Orders</h1>
            </Col>
            <Col span={12}></Col>
          </Row>
        </div>
        <Table
          style={{ height: '400px' }}
          scroll={{ y: 400 }}
          columns={columns}
          dataSource={orders}
          onChange={onGridChange}
          rowKey='Id'
          pagination={{ current: currentPage, showSizeChanger: false, pageSize: pageSize, total: total }}
        />
      </div>
    </>
  );
}
