import { Badge, Breadcrumb, Button, Col, Form, Input, Row, Select, Space, Table } from 'antd';
import { useEffect, useState } from 'react';

import storesServices from '@core/services/stores.service';
import StoresCreateComponent from './create/StoresCreate';
import { useNavigate } from 'react-router';
import { ROUTE_PATHS } from '@core/constants/url-config';
import { toggleLoading } from '@core/utils/loading/loading';

export default function StoresComponent(props) {
  const [currentPage, setCurrentPage] = useState(1);
  const [stores, setStores] = useState([]);
  const [skipPage, setSkipPage] = useState(0);
  const [total, setTotal] = useState(0);
  const pageSize = 50;
  const navigate = useNavigate();
  useEffect(() => {
    getStores(skipPage, pageSize);
  }, []);

  const getStores = async (skip: number, take: number) => {
    toggleLoading(true);
    const data: any = await storesServices.getAll(skip, take);
    if (data) {
      setStores(data);
      setTotal(data.length);
      toggleLoading(false);
    }

   
  };
  const handleViewProduct = (id) => {
    navigate(ROUTE_PATHS.StoreDetail + '/' + id);
  };
  const columns = [
    {
      title: 'Name',
      dataIndex: 'name',
      width: '20%',
    },
    {
      title: 'Location',
      dataIndex: 'location',
      width: '150',
    },
    {
      title: '#',
      dataIndex: '',
      width: '150',
      render: (_: any, record: any) => (
        <Space size='middle'>
          <a onClick={() => handleViewProduct(record.id)}>View Products</a>
        </Space>
      ),
    },
  ];

  const onGridChange = async (_pagination: any, _filters: any, sorter: any) => {
    const nextPage = (_pagination.current - 1) * pageSize;
    setSkipPage(nextPage);
    getStores(nextPage, pageSize);
    setCurrentPage(_pagination.current);
  };

  const refreshGridView = () => {
    getStores(skipPage, pageSize);
  };
  return (
    <>
      <Breadcrumb className='breadcrumb'>
        <Breadcrumb.Item>{'Home'}</Breadcrumb.Item>
        <Breadcrumb.Item>{'Shop'}</Breadcrumb.Item>
      </Breadcrumb>
      <div className='data-list page-content employee'>
        <div className='list-header'>
          <Row>
            <Col span={12}>
              <h1 className='page-title'>{'Stores'}</h1>
            </Col>
            <Col span={12}>
              <StoresCreateComponent refreshGridView={refreshGridView} />
            </Col>
          </Row>
        </div>
        <Table
          style={{ height: '400px' }}
          scroll={{ y: 400 }}
          columns={columns}
          dataSource={stores}
          onChange={onGridChange}
          rowKey='Id'
          pagination={{ current: currentPage, showSizeChanger: false, pageSize: pageSize, total: total }}
        />
      </div>
    </>
  );
}
