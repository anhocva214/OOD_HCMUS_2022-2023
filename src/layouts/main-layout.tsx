import { ROUTES } from '@utils/routes';
import Link from 'next/link';
import { useRouter } from 'next/router';
import React, { ReactNode, useEffect } from 'react';

interface IProps {
    children: ReactNode,
    pageActive: string
}


const MainLayout = (props: IProps) => {

    return (
        <>
            <div className="row">
                <nav>
                    <Link href={ROUTES.dashboard}>
                        <img className="logo" src="./assets/img/Logo.png" alt="monoleak-logo" />
                    </Link>
                    <ul>
                        <li className={ROUTES.dashboard == props.pageActive && 'active'}>
                            <img src="./assets/img/dashboard-icon.png" alt="dashboard-icon" />
                            <Link href={ROUTES.dashboard}>
                                <span > Tổng quan </span>
                            </Link>
                        </li>
                        <li className={ROUTES.transaction == props.pageActive && 'active'}>
                            <img src="./assets/img/giaodich-icon.png" alt="giaodich-icon" />
                            <Link href={ROUTES.transaction}>
                                <span > Giao dịch </span>
                            </Link>
                        </li>
                        <li className={ROUTES.setting == props.pageActive && 'active'}>
                            <img src="./assets/img/setting.png" alt="caidat-icon" />
                            <Link href={ROUTES.setting}>
                                <span > Cài đặt </span>
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