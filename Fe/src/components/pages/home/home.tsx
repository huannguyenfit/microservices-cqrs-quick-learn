
import { Col, Row } from 'antd/lib/grid';
import './home.scss';

export default function HomePage() {
  return (
    <div className="home-page">
      <div className="welcome">
      </div>
      <div className="content">
        <Row gutter={{ xs: 8, sm: 16, md: 24, lg: 25 }}>
          <Col className="gutter-row" span={14}>
     
          </Col>
        
        </Row>
      </div>
    </div>
  );
}
