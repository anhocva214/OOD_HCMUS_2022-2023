import { useAppDispatch } from "@redux/index";
import { registerUser, userSelector } from "@redux/user.redux";
import { message, Spin } from "antd";
import Head from "next/head";
import { ChangeEvent, useState } from "react";
import { useSelector } from "react-redux";
import { UserRegister } from "src/models/request/user";
import { LoadingOutlined } from '@ant-design/icons';
import { useRouter } from "next/router";

export default function RegisterPage() {
    const dispatch = useAppDispatch()
    const router = useRouter()
    const {
        loadingRegisterUser,
    } = useSelector(userSelector)
    const [form, setForm] = useState<UserRegister>(new UserRegister())
    

    const onChange = (e: ChangeEvent<HTMLInputElement>) => {
        let temp = { ...form }
        temp[e.target.name] = e.target.value
        setForm(temp)
    }

    const onSubmit = async (e: ChangeEvent<HTMLFormElement>) => {
        e.preventDefault()
        dispatch(registerUser(form)).unwrap().then(value => {
            // console.log(value)
            message.success("Đăng ký thành công")
            router.push('/dang-nhap')
        }).catch(err => {
            console.log(err)
            message.error(err?.response.data.errorMessage)
        })
    }

    return (
        <>
            <Head>
                <link href="./assets/css/sign-up.css" rel="stylesheet" />
            </Head>
            <section>
                <article className="left-content">
                    <div className="logo">
                        <img src="./assets/img/Logo.png" alt="logo" />
                    </div>
                    <div className="sign-up">
                        <form onSubmit={onSubmit} className="sign-up-form">
                            <h1>Tạo tài khoản mới</h1>
                            <p>Bạn cần điền đầy đủ thông tin để tiếp tục</p>
                            <div className="input-field">
                                <p>Họ và tên</p>
                                <input onChange={onChange} type="text" placeholder="Nhập Họ và tên" name="fullname" />
                            </div>
                            <div className="input-field">
                                <p>Email</p>
                                <input onChange={onChange} type="email" placeholder="Nhập Email" name="email" />
                            </div>
                            <div className="input-field">
                                <p>Mật khẩu</p>
                                <input onChange={onChange} type="password" placeholder="*******" name="password" />
                            </div>
                            <button type="submit" disabled={loadingRegisterUser} className="btn-sign-up flex gap-3">
                                <span>Đăng kí</span>
                                {loadingRegisterUser && (
                                    <Spin indicator={<LoadingOutlined style={{ fontSize: 20, color: '#fff' }} spin />} />
                                )}
                            </button>
                            <span className="sign-in-link">
                                Bạn đã có tài khoản?
                                <a href="./sign-in.html">&nbsp;Đăng nhập</a>
                            </span>
                        </form>
                    </div>
                </article>
                <article className="right-content">
                    <div className="Image">
                        <img src="./assets/img/Image.png" alt="image" />
                    </div>
                </article>
            </section>

        </>
    )
}