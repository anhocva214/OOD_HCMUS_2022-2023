import { useDispatch, useSelector } from 'react-redux'
import { useEffect } from 'react'
import { userSelector } from '@redux/user.redux'
import { CircleSpinner, ClapSpinner } from 'react-spinners-kit'
import { Img } from 'src/resources/img'
import { AsyncThunkAction } from '@reduxjs/toolkit'
import { useAppDispatch } from '@redux/index'
import MainLayout from '@layouts/main-layout'
import { ROUTES } from '@utils/routes'
import { GetServerSideProps } from 'next'
import { User } from 'src/models/response/user.model'
import { Progress, Skeleton } from 'antd'
import { userApi } from '@apis/user.api'
import { CATEGORIES, categorySelector, getAllCategories } from '@redux/category.redux'
import dynamic from 'next/dynamic'
import Link from 'next/link'
import { transactionSelector } from '@redux/transaction.redux'
import _ from 'lodash'

const TransactionTable = dynamic(() => import('@components/transaction/table'), {
  ssr: false,
})


export const getServerSideProps: GetServerSideProps = async ({ req, res }) => {
  try {
    let id = req.cookies?.userId
    let user = await userApi.getUserById(id)
    return { props: { user } }
  }
  catch (err) {
    // console.log(err)
    return { props: {}, redirect: { destination: '/dang-nhap' } }
  }
}

interface IProps {
  user: User
}

export default function Home({ user }: IProps) {
  const dispatch = useAppDispatch()
  const {transactions, transactionsIncome, transactionsSpending} = useSelector(transactionSelector)
  const {
    loadingAllCategories,
    categories
  } = useSelector(categorySelector)


  
  const getAmountTypeTransaction = (name: typeof CATEGORIES[number]) => {
    let data = transactionsSpending.filter(item => item.categoryId == categories.find(item => item.name == name)?.id)
    return _.sumBy(data, 'amount')
  }

  const getPercentTypeTransaction = (name: typeof CATEGORIES[number]) => {
    let data = transactionsSpending.filter(item => item.categoryId == categories.find(item => item.name == name)?.id)
    return _.sumBy(data, 'amount')*100/_.sumBy(transactionsSpending, 'amount')
  }

  return (
    <MainLayout pageActive={ROUTES.dashboard}>
      <section className="dashboard-wrapper">
        <article className="left-wrap">
          <div className="general">
            <h1>T???ng quan</h1>
            <div className="Surplus">
              <div className="surplus-img">
              <img className="offHover" src="./assets/img/icon-wallet-dark.svg" alt="" />
                <img className="onHover" src="./assets/img/icon-wallet-light.svg" alt="" />
              </div>
              <div className="detail">
                <p>S??? d??</p>
                <p className="surplus-value">
                  {(_.sumBy(transactionsIncome, 'amount') - _.sumBy(transactionsSpending, 'amount')).toLocaleString()}
                </p>
              </div>
            </div>
            <div className="Expense">
              <div className="expense-img">
                <img className="offHover" src="./assets/img/icon-wallet-dark.svg" alt="" />
                <img className="onHover" src="./assets/img/icon-wallet-light.svg" alt="" />
              </div>
              <div className="detail">
                <p>T???ng chi ti??u</p>
                <p className="expense-value">
                  {_.sumBy(transactionsSpending, 'amount').toLocaleString()}
                </p>
              </div>
            </div>
            <div className="Income">
              <div className="income-img">
              <img className="offHover" src="./assets/img/icon-wallet-dark.svg" alt="" />
                <img className="onHover" src="./assets/img/icon-wallet-light.svg" alt="" />
              </div>
              <div className="detail">
                <p>T???ng thu nh???p</p>
                <p className="income-value">
                {_.sumBy(transactionsIncome, 'amount').toLocaleString()}
                </p>
              </div>
            </div>
          </div>
          <div className="charts">
            <h1>D??ng ti???n l??u ?????ng</h1>
            <div className="chart" />
          </div>
          <div className="recent">
            <div className="recent-heading">
              <h1>Giao d???ch g???n ????y</h1>
              <Link href={ROUTES.transaction}>
              <span>Xem t???t c???</span>
              </Link>
            </div>
            <div className="tables">
              <TransactionTable textSearching='' limit={5} />
            </div>
          </div>
        </article>
        <div className="right-wrap">
          <div className="grid grid-cols-2 w-full px-8 gap-3">
            {loadingAllCategories && (
              <div className="col-span-full">
                <Skeleton active />
              </div>
            )}

            {!loadingAllCategories && categories.length > 0 && (
              <>
                <div className="col-span-1">
                  <div className='bg-[#f8f8f8] p-3 rounded-lg flex flex-col gap-3'>
                    <span className='w-10 h-10 block flex items-center justify-center text-[#00C3FE] text-lg bg-white shadow-[0px_2px_5px_#154f932b] rounded-full'>
                      <i className="fa-solid fa-heart-pulse"></i>
                    </span>
                    <h3 className='mb-0'>
                      S???c kho???
                    </h3>
                    <div className='flex items-center gap-2'>
                      <span className='text-[#ffb300]'>
                        <i className="fa-solid fa-coins"></i>
                      </span>
                      <span className='text-slate-500 text-sm'>{(getAmountTypeTransaction('S???c kho???')).toLocaleString()}??</span>
                    </div>
                    <div>
                      <Progress percent={getPercentTypeTransaction('S???c kho???')} showInfo={false} strokeColor="#363A3F" trailColor='#DFE4F3' style={{ margin: 0 }} />
                    </div>
                  </div>
                </div>

                <div className="col-span-1">
                  <div className='bg-[#f8f8f8] p-3 rounded-lg flex flex-col gap-3'>
                    <span className='w-10 h-10 block flex items-center justify-center text-[#6065D7] text-lg bg-white shadow-[0px_2px_5px_#154f932b] rounded-full'>
                      <i className="fa-solid fa-arrow-up-arrow-down"></i>
                    </span>
                    <h3 className='mb-0'>
                      Chuy???n ti???n
                    </h3>
                    <div className='flex items-center gap-2'>
                      <span className='text-[#ffb300]'>
                        <i className="fa-solid fa-coins"></i>
                      </span>
                      <span className='text-slate-500 text-sm'>{(getAmountTypeTransaction('Chuy???n ti???n')).toLocaleString()}??</span>
                    </div>
                    <div>
                    <Progress percent={getPercentTypeTransaction('Chuy???n ti???n')} showInfo={false} strokeColor="#363A3F" trailColor='#DFE4F3' style={{ margin: 0 }} />
                    </div>
                  </div>
                </div>

                <div className="col-span-1">
                  <div className='bg-[#f8f8f8] p-3 rounded-lg flex flex-col gap-3'>
                    <span className='w-10 h-10 block flex items-center justify-center text-[#FF985D] text-lg bg-white shadow-[0px_2px_5px_#154f932b] rounded-full'>
                      <i className="fa-sharp fa-solid fa-burger-soda"></i>
                    </span>
                    <h3 className='mb-0'>
                      ??n u???ng
                    </h3>
                    <div className='flex items-center gap-2'>
                      <span className='text-[#ffb300]'>
                        <i className="fa-solid fa-coins"></i>
                      </span>
                      <span className='text-slate-500 text-sm'>{(getAmountTypeTransaction('??n u???ng')).toLocaleString()}??</span>
                    </div>
                    <div>
                    <Progress percent={getPercentTypeTransaction('??n u???ng')} showInfo={false} strokeColor="#363A3F" trailColor='#DFE4F3' style={{ margin: 0 }} />
                    </div>
                  </div>
                </div>

                <div className="col-span-1">
                  <div className='bg-[#f8f8f8] p-3 rounded-lg flex flex-col gap-3'>
                    <span className='w-10 h-10 block flex items-center justify-center text-[#F4719A] text-lg bg-white shadow-[0px_2px_5px_#154f932b] rounded-full'>
                      <i className="fa-solid fa-bags-shopping"></i>
                    </span>
                    <h3 className='mb-0'>
                      Mua s???m
                    </h3>
                    <div className='flex items-center gap-2'>
                      <span className='text-[#ffb300]'>
                        <i className="fa-solid fa-coins"></i>
                      </span>
                      <span className='text-slate-500 text-sm'>{(getAmountTypeTransaction('Mua s???m')).toLocaleString()}??</span>
                    </div>
                    <div>
                    <Progress percent={getPercentTypeTransaction('Mua s???m')} showInfo={false} strokeColor="#363A3F" trailColor='#DFE4F3' style={{ margin: 0 }} />
                    </div>
                  </div>
                </div>

                <div className="col-span-1">
                  <div className='bg-[#f8f8f8] p-3 rounded-lg flex flex-col gap-3'>
                    <span className='w-10 h-10 block flex items-center justify-center text-[#29A073] text-lg bg-white shadow-[0px_2px_5px_#154f932b] rounded-full'>
                      <i className="fa-solid fa-graduation-cap"></i>
                    </span>
                    <h3 className='mb-0'>
                      Gi??o d???c
                    </h3>
                    <div className='flex items-center gap-2'>
                      <span className='text-[#ffb300]'>
                        <i className="fa-solid fa-coins"></i>
                      </span>
                      <span className='text-slate-500 text-sm'>{(getAmountTypeTransaction('Gi??o d???c')).toLocaleString()}??</span>
                    </div>
                    <div>
                    <Progress percent={getPercentTypeTransaction('Gi??o d???c')} showInfo={false} strokeColor="#363A3F" trailColor='#DFE4F3' style={{ margin: 0 }} />
                    </div>
                  </div>
                </div>

                <div className="col-span-1">
                  <div className='bg-[#f8f8f8] p-3 rounded-lg flex flex-col gap-3'>
                    <span className='w-10 h-10 block flex items-center justify-center text-[#454A55] text-lg bg-white shadow-[0px_2px_5px_#154f932b] rounded-full'>
                      <i className="fa-solid fa-layer-plus"></i>
                    </span>
                    <h3 className='mb-0'>
                      Kh??c
                    </h3>
                    <div className='flex items-center gap-2'>
                      <span className='text-[#ffb300]'>
                        <i className="fa-solid fa-coins"></i>
                      </span>
                      <span className='text-slate-500 text-sm'>{(getAmountTypeTransaction('Kh??c')).toLocaleString()}??</span>
                    </div>
                    <div>
                    <Progress percent={getPercentTypeTransaction('Kh??c')} showInfo={false} strokeColor="#363A3F" trailColor='#DFE4F3' style={{ margin: 0 }} />
                    </div>
                  </div>
                </div>
              </>
            )}

          </div>

        </div>
      </section>
    </MainLayout>
  )
}
