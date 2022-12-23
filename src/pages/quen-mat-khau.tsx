import { useAppDispatch } from "@redux/index";
import { forgotPasswordUser, userSelector } from "@redux/user.redux";
import { message, Spin } from "antd";
import Head from "next/head";
import Link from "next/link";
import { useRouter } from "next/router";
import { useSelector } from "react-redux";
import { LoadingOutlined } from '@ant-design/icons';
import { UserForgotPassword } from "src/models/request/user";
import { ChangeEvent, useState } from "react";


export default function ForgetPassword(){
    const router = useRouter()
    const dispatch = useAppDispatch()
    const {
        loadingForgotPasswordUser
    } = useSelector(userSelector)

    const [form, setForm] = useState(new UserForgotPassword())

    const onChange = (e: ChangeEvent<HTMLInputElement>)=>{
        let temp = { ...form}
        temp[e.target.name] = e.target.value
        setForm(temp)
    }

    const onSubmit = (e: ChangeEvent<HTMLFormElement>)=>{
        e.preventDefault()
        dispatch(forgotPasswordUser(form))
        .unwrap()
        .then(value =>{
            message.success("Mật khẩu đã được gửi về email của bạn")
            router.push('/dang-nhap')
        })
        .catch(err => {
            console.log(err)
            err?.response.data.errorMessage && message.error(err?.response.data.errorMessage)
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
                        <img src="./assets/img/logo.svg" alt="logo" />
                    </div>
                    <div className="sign-in">
                        <form onSubmit={onSubmit} className="sign-in-form">
                            <h1>Cấp mật khẩu mới</h1>
                            <p>Mật khẩu mới sẽ được gửi về email của bạn</p>
                            <div className="input-field">
                                <p>Email</p>
                                <input className="outline-0" onChange={onChange} type="text" placeholder="Nhập email của bạn" name="email" />
                            </div>
                            <Link href={'/dang-nhap'} className="forget-pass">
                                <span>Đăng nhập</span>
                            </Link>
                            <button type="submit" className="btn-log-in flex gap-3">
                                <span>Cấp mật khẩu mới</span>
                                {loadingForgotPasswordUser && (
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