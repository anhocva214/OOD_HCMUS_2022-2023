import { ROUTES } from '@utils/routes';
import Link from 'next/link';
import { useRouter } from 'next/router';
import React, { ReactNode, useEffect } from 'react';
import { LogoutOutlined, HomeOutlined, TransactionOutlined, SettingOutlined } from '@ant-design/icons';
import cookie from 'react-cookies'

interface IProps {
    children: ReactNode,
    pageActive: string
}


const MainLayout = (props: IProps) => {

    const logout = ()=>{
        cookie.remove('userId')
    }

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
                                <span > Tổng quan </span>
                            </Link>
                        </li>
                        <li className={ROUTES.transaction == props.pageActive && 'active'}>
                        <TransactionOutlined style={{color: "#1b212d"}} />
                            <Link href={ROUTES.transaction}>
                                <span > Giao dịch </span>
                            </Link>
                        </li>
                        <li className={ROUTES.setting == props.pageActive && 'active'}>
                        <SettingOutlined style={{color: "#1b212d"}} />
                            <Link href={ROUTES.setting}>
                                <span > Cài đặt </span>
                            </Link>
                        </li>
                        <li>
                            <LogoutOutlined style={{color: "#1b212d"}} />
                            <Link href={ROUTES.login} onClick={logout}>
                                <span > Đăng xuất </span>
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