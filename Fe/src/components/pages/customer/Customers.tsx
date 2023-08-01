import {
  Badge,
  Breadcrumb,
  Button,
  Col,
  Form,
  Input,
  Row,
  Select,
  Space,
  Table,
} from "antd";
import { useEffect, useState } from "react";

import customerService from "@core/services/customer.service";
import CustomerCreateComponent from "./create/CustomerCreate";
import moment from "moment";
import { useNavigate } from "react-router";
import { ROUTE_PATHS } from "@core/constants/url-config";
import { toggleLoading } from "@core/utils/loading/loading";
import * as XLSX from "xlsx";
export default function CustomerComponent(props) {
  const [currentPage, setCurrentPage] = useState(1);
  const [customers, setCustomers] = useState([]);
  const [skipPage, setSkipPage] = useState(0);
  const [total, setTotal] = useState(0);
  const pageSize = 50;
  const navigate = useNavigate();

  useEffect(() => {
    getCustomers(skipPage, pageSize);
  }, []);

  const getCustomers = async (skip: number, take: number) => {
    toggleLoading(true);
    const data: any = await customerService.getAll(skip, take);
    if (data) {
      setCustomers(data);
      setTotal(data.length);
      toggleLoading(false);
    }
  };

  const handleExplorerProduct = (customerId) => {
    navigate(ROUTE_PATHS.ExploreShopping + "/" + customerId);
  };

  const handleMyOrders = (customerId) => {
    navigate(ROUTE_PATHS.MyOrders + "/" + customerId);
  };

  const columns = [
    {
      title: "Full Name",
      dataIndex: "fullName",
      width: "20%",
    },
    {
      title: "DOB",
      dataIndex: "DOB",
      width: "150",
      render: (dob: string) => {
        return <span>{moment(dob).format("MM/DD/YYYY")}</span>;
      },
    },
    {
      title: "Email",
      dataIndex: "email",
      width: "250",
    },
    {
      title: "#",
      dataIndex: "",
      width: "150",
      render: (_: any, record: any) => (
        <Space size='middle'>
          <a onClick={() => handleExplorerProduct(record.id)}>
            Explore Shopping
          </a>
        </Space>
      ),
    },
    {
      title: "#",
      dataIndex: "",
      width: "150",
      render: (_: any, record: any) => (
        <Space size='middle'>
          <a onClick={() => handleMyOrders(record.id)}>My Order</a>
        </Space>
      ),
    },
  ];

  const onGridChange = async (_pagination: any, _filters: any, sorter: any) => {
    const nextPage = (_pagination.current - 1) * pageSize;
    setSkipPage(nextPage);
    getCustomers(nextPage, pageSize);
    setCurrentPage(_pagination.current);
  };

  const refreshGridView = () => {
    getCustomers(skipPage, pageSize);
  };
  const handleChange = async (e) => {
    const file = e.target.files[0];
    const data = await file.arrayBuffer();
    const wb = XLSX.read(data);
    const ws = wb.Sheets[wb.SheetNames[0]];
    const json  = XLSX.utils.sheet_to_dif(ws);
    console.log(json);
  };
  return (
    <>
      <input type={"file"} onChange={(e) => handleChange(e)} />

      <Breadcrumb className='breadcrumb'>
        <Breadcrumb.Item>{"Home"}</Breadcrumb.Item>
        <Breadcrumb.Item>{"Customer"}</Breadcrumb.Item>
      </Breadcrumb>
      <div className='data-list page-content employee'>
        <div className='list-header'>
          <Row>
            <Col span={12}>
              <h1 className='page-title'>{"Customer"}</h1>
            </Col>
            <Col span={12}>
              <CustomerCreateComponent refreshGridView={refreshGridView} />
            </Col>
          </Row>
        </div>
        <Table
          style={{ height: "400px" }}
          scroll={{ y: 400 }}
          columns={columns}
          dataSource={customers}
          onChange={onGridChange}
          rowKey='Id'
          pagination={{
            current: currentPage,
            showSizeChanger: false,
            pageSize: pageSize,
            total: total,
          }}
        />
      </div>
    </>
  );
}
