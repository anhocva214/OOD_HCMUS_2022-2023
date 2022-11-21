import { useDispatch, useSelector } from 'react-redux'
import { useEffect } from 'react'
import { fetchUsers, userSelector } from '@redux/user.redux'
import { CircleSpinner, ClapSpinner } from 'react-spinners-kit'
import { Img } from 'src/resources/img'
import { AsyncThunkAction } from '@reduxjs/toolkit'
import { useAppDispatch } from '@redux/index'
import MainLayout from '@layouts/main-layout'
import { ROUTES } from '@utils/routes'


export default function Home() {
  const dispatch = useAppDispatch()
  const { users, getUsersLoading } = useSelector(userSelector)

  useEffect(() => {
    console.log(users)
  }, [users])

  return (
    <MainLayout pageActive={ROUTES.dashboard}>
        <section className="dashboard-wrapper">
          <article className="left-wrap">
            <div className="general">
              <h1>Tổng quan</h1>
              <div className="Surplus">
                <div className="surplus-img">
                  <img className="offHover" src="./assets/img/Value.png" alt="" />
                  <img className="onHover" src="./assets/img/ValueHover.png" alt="" />
                </div>
                <div className="detail">
                  <p>Số dư</p>
                  <p className="surplus-value">$5240.21</p>
                </div>
              </div>
              <div className="Expense">
                <div className="expense-img">
                  <img className="offHover" src="./assets/img/Value.png" alt="" />
                  <img className="onHover" src="./assets/img/ValueHover.png" alt="" />
                </div>
                <div className="detail">
                  <p>Tổng chi tiêu</p>
                  <p className="expense-value">$250.80</p>
                </div>
              </div>
              <div className="Income">
                <div className="income-img">
                  <img className="offHover" src="./assets/img/Value2.png" alt="" />
                  <img className="onHover" src="./assets/img/Value2Hover.png" alt="" />
                </div>
                <div className="detail">
                  <p>Tổng thu nhập</p>
                  <p className="income-value">$550.25</p>
                </div>
              </div>
            </div>
            <div className="charts">
              <h1>Dòng tiền lưu động</h1>
              <div className="chart" />
            </div>
            <div className="recent">
              <div className="recent-heading">
                <h1>Giao dịch gần đây</h1>
                <a href="./transactions.html">Xem tất cả</a>
              </div>
              <div className="tables">
                <table>
                  <tbody><tr>
                    <th>Tên hạng mục</th>
                    <th>Loại</th>
                    <th>Số tiền</th>
                    <th>Thời gian</th>
                  </tr>
                    <tr>
                      <td>Gửi cho Nhi</td>
                      <td>Chuyển tiền</td>
                      <td>100.000đ</td>
                      <td>1/6/2021</td>
                    </tr>
                    <tr>
                      <td>Gửi cho Nhi</td>
                      <td>Chuyển tiền</td>
                      <td>100.000đ</td>
                      <td>1/6/2021</td>
                    </tr>
                    <tr>
                      <td>Gửi cho Nhi</td>
                      <td>Chuyển tiền</td>
                      <td>100.000đ</td>
                      <td>1/6/2021</td>
                    </tr>
                  </tbody></table>
              </div>
            </div>
          </article>
          <div className="right-wrap">
            <div className="calculator">
              <div className="cal-icon">
                <img src="./assets/img/heart.png" alt="heart" />
              </div>
              <p>Sức khỏe</p>
              <span><img src="./assets/img/coin.png" alt="" /></span>
              <span>1.000.000</span>
            </div>
            <div className="calculator">
              <div className="cal-icon">
                <img src="./assets/img/heart.png" alt="heart" />
              </div>
              <p>Sức khỏe</p>
              <span><img src="./assets/img/coin.png" alt="" /></span>
              <span>1.000.000</span>
            </div>
            <div className="calculator">
              <div className="cal-icon">
                <img src="./assets/img/heart.png" alt="heart" />
              </div>
              <p>Sức khỏe</p>
              <span><img src="./assets/img/coin.png" alt="" /></span>
              <span>1.000.000</span>
            </div>
            <div className="calculator">
              <div className="cal-icon">
                <img src="./assets/img/heart.png" alt="heart" />
              </div>
              <p>Sức khỏe</p>
              <span><img src="./assets/img/coin.png" alt="" /></span>
              <span>1.000.000</span>
            </div>
            <div className="calculator">
              <div className="cal-icon">
                <img src="./assets/img/heart.png" alt="heart" />
              </div>
              <p>Sức khỏe</p>
              <span><img src="./assets/img/coin.png" alt="" /></span>
              <span>1.000.000</span>
            </div>
            <div className="calculator">
              <div className="cal-icon">
                <img src="./assets/img/heart.png" alt="heart" />
              </div>
              <p>Sức khỏe</p>
              <span><img src="./assets/img/coin.png" alt="" /></span>
              <span>1.000.000</span>
            </div>
          </div>
        </section>
    </MainLayout>
  )
}
