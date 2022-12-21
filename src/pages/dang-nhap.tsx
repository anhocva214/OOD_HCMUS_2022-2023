import { useAppDispatch } from "@redux/index";
import { loginUser, userSelector } from "@redux/user.redux";
import Head from "next/head";
import { useSelector } from "react-redux";
import { LoadingOutlined } from '@ant-design/icons';
import { message, Spin } from "antd";
import { ChangeEvent, useState } from "react";
import { UserLogin } from "src/models/request/user";
import { useRouter } from "next/router";
import Link from "next/link";
import cookie from 'react-cookies'


export default function LoginPage() {
    const router = useRouter()
    const dispatch = useAppDispatch()
    const { loadingLoginUser } = useSelector(userSelector)

    const [form, setForm] = useState(new UserLogin())

    const onChange = (e: ChangeEvent<HTMLInputElement>) => {
        e.preventDefault()
        let temp = { ...form }
        temp[e.target.name] = e.target.value
        setForm({ ...temp })
    }

    const onSubmit = (e: ChangeEvent<HTMLFormElement>) => {
        e.preventDefault()
        dispatch(loginUser(form))
            .unwrap()
            .then(value => {
                message.success("Đăng nhập thành công")
                cookie.save('userId', value.id, {path: '/', maxAge: 4*60*60})
                router.push('/')
            })
            .catch(err => {
                console.log("🚀 .~ file: dang-nhap.tsx:33 ~ onSubmit ~ err", err)
                return err?.response.data.errorMessage && message.error(err?.response.data.errorMessage)
            })
    }

    return (
        <>
            <Head>
                <link href="./assets/css/sign-in.css" rel="stylesheet" />
            </Head>
            <section>
                <article className="left-content">
                    <div className="logo">
                        <img src="./assets/img/Logo.png" alt="logo" />
                    </div>
                    <div className="sign-in">
                        <form onSubmit={onSubmit} className="sign-in-form">
                            <h1>MonoLeak</h1>
                            <p>Chào mừng bạn đến với chúng tôi</p>
                            <div className="input-field">
                                <p>Email</p>
                                <input onChange={onChange} type="text" placeholder="Nhập email của bạn" name="email" />
                            </div>
                            <div className="input-field">
                                <p>Mật khẩu</p>
                                <input onChange={onChange} type="password" placeholder="*******" name="password" />
                            </div>
                            <Link href={'/quen-mat-khau'} className="forget-pass">
                                <span>Quên mật khẩu</span>
                            </Link>
                            <button type="submit" className="btn-log-in">
                                <span>Đăng nhập</span>
                                {loadingLoginUser && (
                                    <Spin indicator={<LoadingOutlined style={{ fontSize: 20, color: '#fff' }} spin />} />
                                )}
                            </button>
                            <span className="sign-up-link">Bạn chưa có tài khoản?
                                <Link href={'/dang-ky'}>
                                    <span>&nbsp;Đăng kí miễn phí</span>
                                </Link>
                            </span>
                        </form>
                    </div>
                </article>
                <article className="right-content">
                    <div className="Image">
                        <img src="./assets/img/Image.png" alt="logo-img" />
                    </div>
                </article>
            </section>
        </>
    )
}