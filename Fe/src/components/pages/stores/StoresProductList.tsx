import { Badge, Breadcrumb, Button, Col, Form, Input, Row, Select, Space, Table } from 'antd';
import { useEffect, useState } from 'react';

import { useParams } from 'react-router';
import storesService from '@core/services/stores.service';
import ProductCreateComponent from '../products/create/ProductCreate';
import { toggleLoading } from '@core/utils/loading/loading';

export default function StoreProductListComponent(props) {
  const [currentPage, setCurrentPage] = useState(1);
  const [products, setProducts] = useState([]);
  const [store, setStores] = useState(undefined);
  const [skipPage, setSkipPage] = useState(0);
  const [total, setTotal] = useState(0);
  const pageSize = 50;

  const { id } = useParams();
  useEffect(() => {
    getProductByStore(skipPage, pageSize);
    getStore();
  }, []);

  const getProductByStore = async (skip: number, take: number) => {
    toggleLoading(true)
    storesService.getProductByStore(id, skip, take).then((res: any) => {
      setProducts(res.data);

      storesService.getProductByStoreCountTotalLazy(id, skip, take).then((data: any) => {
        setTotal(data);
      });

      toggleLoading(false);
    });
  };

  const getStore = async () => {
    storesService.getById(id).then((data: any) => {
      setStores(data);
    });
  };

  const columns = [
    {
      title: 'Name',
      dataIndex: 'name',
      width: '20%',
    },
    {
      title: 'Price',
      dataIndex: 'price',
      width: '150',
      render: (price) => {
        return <>{price.toFixed(2)}</>;
      },
    },
    {
      title: 'Quantity',
      dataIndex: 'quantity',
      width: '150',
    },
  ];

  const onGridChange = async (_pagination: any, _filters: any, sorter: any) => {
    const nextPage = (_pagination.current - 1) * pageSize;
    setSkipPage(nextPage);
    getProductByStore(nextPage, pageSize);
    setCurrentPage(_pagination.current);
  };

  const refreshGridView = () => {
    getProductByStore(skipPage, pageSize);
  };

  return (
    <>
      <Breadcrumb className='breadcrumb'>
        <Breadcrumb.Item>{'Home'}</Breadcrumb.Item>
        <Breadcrumb.Item>{'Product'}</Breadcrumb.Item>
      </Breadcrumb>
      <div className='data-list page-content employee'>
        <div className='list-header'>
          <Row>
            <Col span={12}>
              <div className='d-flex flex-column'>
                <h1 className='page-title'>Store: {store?.name}</h1>
                <h3 className='m-0' >Product List</h3>
              </div>
            </Col>
            <Col span={12}>
              <ProductCreateComponent storeId={id} refreshGridView={refreshGridView} />
            </Col>
          </Row>
        </div>
        <Table
          style={{ height: '400px' }}
          scroll={{ y: 400 }}
          columns={columns}
          dataSource={products}
          onChange={onGridChange}
          rowKey='Id'
          pagination={{ current: currentPage, showSizeChanger: false, pageSize: pageSize, total: total }}
        />
      </div>
    </>
  );
}
