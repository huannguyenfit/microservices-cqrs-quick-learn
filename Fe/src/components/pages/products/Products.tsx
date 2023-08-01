import { Badge, Breadcrumb, Button, Col, Form, Input, InputNumber, Row, Select, Space, Table } from 'antd';
import { useEffect, useState } from 'react';

import productServices from '@core/services/product.service';
import ProductCreateComponent from './create/ProductCreate';
import { toggleLoading, toggleMessage } from '@core/utils/loading/loading';
import productService from '@core/services/product.service';
const { Search } = Input;

export default function ProductComponent(props) {
  const [currentPage, setCurrentPage] = useState(1);
  const [product, setProducts] = useState([]);

  const [skipPage, setSkipPage] = useState(0);
  const [keyword, setKeyword] = useState('');
  const [minPrice, setMinPrice] = useState(null);
  const [maxPrice, setMaxPrice] = useState(null);
  const [total, setTotal] = useState(0);

  const pageSize = 100;

  useEffect(() => {
    getProduct(keyword, minPrice, maxPrice, skipPage, pageSize);
  }, []);

  const getProduct = async (keyword, minPrice, maxPrice, skip: number, take: number) => {
    if (minPrice > 0 && maxPrice > 0 && maxPrice < minPrice) {
      toggleMessage({ message: 'MaxPrice must be greater than MinPrice', type: 'error' });
      return;
    }

    if (minPrice && minPrice < 0) {
      toggleMessage({ message: 'MinPrice must be greater than 0', type: 'error' });
      return;
    }

    if (maxPrice && maxPrice < 0) {
      toggleMessage({ message: 'maxPrice must be greater than 0', type: 'error' });
      return;
    }

    toggleLoading(true);
    const response = await productServices.getAll(keyword, minPrice, maxPrice, skip, take);
    if (response) {
      setProducts(response.data);

      productServices.getAllCountTotalLazy(keyword, minPrice, maxPrice, skip, take).then((data: any) => {
        setTotal(data);
      });

      toggleLoading(false);
    }
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
    {
      title: 'StoreName',
      dataIndex: 'storeName',
      width: '150',
    },
  ];

  const onGridChange = async (_pagination: any, _filters: any, sorter: any) => {
    const nextPage = (_pagination.current - 1) * pageSize;
    setSkipPage(nextPage);
    getProduct(keyword, minPrice, maxPrice, nextPage, pageSize);
    setCurrentPage(_pagination.current);
  };

  const onSearch = (value) => {
    setKeyword(value);
    setSkipPage(0);
    getProduct(value, minPrice, maxPrice, 0, pageSize);
  };

  const onMaxPriceChange = (value) => {
    setMaxPrice(value);
    setSkipPage(0);
  };

  const onMinPriceChange = (value) => {
    setMinPrice(value);

    setSkipPage(0);
  };
  return (
    <>
      <Breadcrumb className='breadcrumb'>
        <Breadcrumb.Item>{'Home'}</Breadcrumb.Item>
        <Breadcrumb.Item>{'All Products'}</Breadcrumb.Item>
      </Breadcrumb>
      <div className='data-list page-content employee'>
        <div className='list-header'>
          <Row>
            <Col span={12}>
              <h1 className='page-title'>{'All Products'}</h1>
            </Col>
            <Col span={12}></Col>
          </Row>
        </div>
        <Row>
          <Col span={24}>
            <Space className='d-flex justify-right' align='end'>
              <InputNumber
                style={{ width: '140px' }}
                formatter={(value) => `${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')}
                parser={(value) => value.replace(/\$\s?|(,*)/g, '')}
                placeholder={'Min price'}
                onChange={onMinPriceChange}
              />
              <InputNumber
                style={{ width: '140px' }}
                formatter={(value) => `${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')}
                parser={(value) => value.replace(/\$\s?|(,*)/g, '')}
                placeholder={'Max price'}
                onChange={onMaxPriceChange}
              />

              <Search
                onChange={(e) => {
                  setKeyword(e.target.value);
                  setSkipPage(0);
                }}
                placeholder='input search text'
                onSearch={(e) => onSearch(e)}
                style={{ width: 200 }}
              />
              <Button type='primary' onClick={(e) => onSearch(keyword)}>
                Search
              </Button>
            </Space>
          </Col>
        </Row>
        <Table
          style={{ height: '400px' }}
          scroll={{ y: 400 }}
          columns={columns}
          dataSource={product}
          onChange={onGridChange}
          rowKey='Id'
          pagination={{ current: currentPage, showSizeChanger: false, pageSize: pageSize, total: total }}
        />
      </div>
    </>
  );
}
