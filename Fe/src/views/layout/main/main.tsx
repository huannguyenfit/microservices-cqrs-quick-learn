import { ImportOutlined } from '@ant-design/icons';
import { useNavigate, Outlet, Link } from 'react-router-dom';
import { Avatar, Col, Dropdown, Layout, Menu, Popover, Row } from 'antd';
import { default as React } from 'react';
import ModuleIcon from '../../../assets/icon/module-icon.svg';
import { ROUTE_PATHS } from '@core/constants/url-config';

import './main.scss';

const { Header, Content } = Layout;

const MainLayout = () => {
  const navigate = useNavigate();

  const userDropdownItems = (
    <Menu>
      <Menu.Item key='2' icon={<ImportOutlined />}>
        signOut
      </Menu.Item>
    </Menu>
  );

  return (
    <Layout className='main-layout'>
      <Header>
        <Row className='header' justify='space-between'>
          <Col>
            <Row>
              <Col style={{ width: 110 }}>
                <Popover id='module-dropdown' placement='bottomLeft' trigger='click'>
                  <img alt='module' src={ModuleIcon} className='app-icon cur' />
                </Popover>

                <img
                  src='https://gw.alipayobjects.com/zos/antfincdn/aPkFc8Sj7n/method-draw-image.svg'
                  alt='logo'
                  onClick={() => navigate(ROUTE_PATHS.Home)}
                  className='cur'
                  height={40}
                />
              </Col>
              <Col>
                <Menu mode='horizontal' defaultSelectedKeys={[]}>
                  <Menu.Item key={ROUTE_PATHS.CustomerList}>
                    {'Customer'}
                    <Link to={ROUTE_PATHS.CustomerList ?? ''} />
                  </Menu.Item>
                  <Menu.Item key={ROUTE_PATHS.StoreList}>
                    {'Shop'}
                    <Link to={ROUTE_PATHS.StoreList ?? ''} />
                  </Menu.Item>
                  <Menu.Item key={ROUTE_PATHS.ProductList}>
                    {'Product'}
                    <Link to={ROUTE_PATHS.ProductList ?? ''} />
                  </Menu.Item>
                </Menu>
              </Col>
            </Row>
          </Col>
          <Col className='right'>
            <Dropdown overlay={userDropdownItems} trigger={['click']}>
              <div className='user-info cur'>
                <Avatar size={32} src={'http://cdn.onlinewebfonts.com/svg/img_568656.png'} />
                <div className='box-info'>
                  <div className='name'>admin</div>
                </div>
              </div>
            </Dropdown>
          </Col>
        </Row>
      </Header>
      <Layout>
        <Layout className='body-layout'>
          <Content
            style={{
              margin: 0,
              display: 'flex',
              flexDirection: 'column',
            }}
          >
            <Outlet />
          </Content>
        </Layout>
      </Layout>
    </Layout>
  );
};

export default MainLayout;
