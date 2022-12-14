import { ROUTES } from '@utils/routes';
import Link from 'next/link';
import { useRouter } from 'next/router';
import React, { ReactNode, useEffect } from 'react';
import { LogoutOutlined, HomeOutlined, TransactionOutlined, SettingOutlined } from '@ant-design/icons';
import cookie from 'react-cookies'
import { useSelector } from 'react-redux';
import { categorySelector, getAllCategories } from '@redux/category.redux';
import { useAppDispatch } from '@redux/index';
import { classifyTransactions, getAllTransactions, transactionSelector } from '@redux/transaction.redux';
import { getUserById, userSelector } from '@redux/user.redux';

interface IProps {
    children: ReactNode,
    pageActive: string
}


const MainLayout = (props: IProps) => {
    const dispatch = useAppDispatch()
    const {categories} = useSelector(categorySelector)
    const {
        transactions,
        loadingTransactions
    } = useSelector(transactionSelector)
    const {
        user
    } = useSelector(userSelector)

    const logout = ()=>{
        cookie.remove('userId')
    }

    useEffect(() => {
        if (categories.length == 0) {
            dispatch(getAllCategories())
        }
    }, [categories])

    useEffect(()=>{
        if (user?.id && transactions.length == 0 && categories.length != 0){
            dispatch(getAllTransactions()).unwrap()
            .then(date=>{
                dispatch(classifyTransactions())
            })
        }

        if (!user?.id){
            dispatch(getUserById())
        }
    },[user, categories])


    

    return (
        <>
            <div className="row">
                <nav>
                    <Link href={ROUTES.dashboard}>
                        <img className="logo" src="./assets/img/logo.svg" alt="monoleak-logo" />
                    </Link>
                    <ul>
                        <li className={ROUTES.dashboard == props.pageActive && 'active'}>
                            <HomeOutlined style={{color: "#1b212d"}} />
                            <Link href={ROUTES.dashboard}>
                                <span > T???ng quan </span>
                            </Link>
                        </li>
                        <li className={ROUTES.transaction == props.pageActive && 'active'}>
                        <TransactionOutlined style={{color: "#1b212d"}} />
                            <Link href={ROUTES.transaction}>
                                <span > Giao d???ch </span>
                            </Link>
                        </li>
                        <li className={ROUTES.setting == props.pageActive && 'active'}>
                        <SettingOutlined style={{color: "#1b212d"}} />
                            <Link href={ROUTES.setting}>
                                <span > C??i ?????t </span>
                            </Link>
                        </li>
                        <li>
                            <LogoutOutlined style={{color: "#1b212d"}} />
                            <Link href={ROUTES.login} onClick={logout}>
                                <span > ????ng xu???t </span>
                            </Link>
                        </li>
                    </ul>
                </nav>
                {props.children}
            </div>
        </>
    )

}

export default MainLayout;