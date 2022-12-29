import { userApi } from "@apis/user.api";
import MainLayout from "@layouts/main-layout";
import { useAppDispatch } from "@redux/index";
import { updateUser } from "@redux/user.redux";
import { ROUTES } from "@utils/routes";
import { DatePicker, message } from "antd";
import { GetServerSideProps } from "next";
import { ChangeEvent, useEffect, useState } from "react";
import { User, UserUpdate } from "src/models/response/user.model";
import dayjs from 'dayjs'


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

export default function SettingPage({
    user
}: IProps) {
    const dispatch = useAppDispatch()
    const [form, setForm] = useState(new User(user))
    const [confirmNewPassword, setConfirmNewPassword] = useState('')

    useEffect(() => {
        setConfirmNewPassword(user.password)
    }, [])

    const onChange = (e: ChangeEvent<HTMLInputElement | HTMLSelectElement>)=>{
        let temp = {...form}
        temp[e.target.name] = e.target.value
        setForm(temp)
    }

    const onSave = () => {
        if (confirmNewPassword != form.password) {
            message.error("Mật khẩu không trùng khớp")
            return;
        }

        dispatch(updateUser(new UserUpdate({ ...form, passwordComfirm: confirmNewPassword }))).unwrap()
            .then(data => {
                setForm({...user, ...data})
                setConfirmNewPassword(confirmNewPassword)
                message.success("Thành công")
            })
            .catch(error => {
                console.log("🚀 ~ file: cai-dat.tsx:52 ~ onSave ~ error", error)
                message.error("Thất bại")
            })
    }

    return (
        <MainLayout pageActive={ROUTES.setting}>
            <section className="setting-wrapper">
                <h1 className="title">Cài đặt</h1>
                <div className="content">
                    <div className="acc-info">
                        <h3>Thông tin tài khoản</h3>
                        <p>Cập nhập thông tin của bạn</p>
                    </div>
                    <div className="change">
                        <h3>Thông tin cá nhân</h3>
                        <a onClick={onSave} role='button' >Lưu</a>
                    </div>
                    <form className="input-field">
                        <div className="first-item">
                            <div className="name">
                                <p>Họ và tên</p>
                                <input
                                    type="text"
                                    value={form?.fullName}
                                    name="fullname"
                                    onChange={onChange}
                                    className="border outline-none"
                                />
                            </div>
                            <div className="sex">
                                <p>Giới tính</p>
                                <select name="gender" value={form?.gender} onChange={onChange} className="border outline-none">
                                    <option value="male" >Nam</option>
                                    <option value="female" >Nữ</option>
                                    <option value="other" >Chưa xác định</option>
                                </select>
                            </div>
                        </div>
                        <div className="second-item">
                            <div className="birthday">
                                <p>Ngày sinh nhật</p>
                                <DatePicker className="h-[50px]" style={{width: '100%'}} format={'DD/MM/YYYY'} value={dayjs(form?.birthday)} onChange={(value, dateString) => {
                                    setForm({...form, birthday: value.toISOString()})
                                }} />
                            </div>
                            <div className="phone">
                                <p>Số điện thoại</p>
                                <input onChange={onChange} name="phoneNumber" type="text" value={form?.phoneNumber} className="border outline-none" />
                            </div>
                        </div>
                        <div className="third-item">
                            <p>Email</p>
                            <input onChange={onChange} name="email" type="email" value={form?.email} className="border outline-none" />
                        </div>
                        <div className="fourth-item">
                            <div className="new-pass">
                                <p>Mật khẩu mới</p>
                                <input onChange={onChange} name="password" type="password" value={form?.password} className="border outline-none" />
                            </div>
                            <div className="new-pass-confirm">
                                <p>Nhập lại mật khẩu mới</p>
                                <input onChange={e => setConfirmNewPassword(e.target.value)} type="password" value={confirmNewPassword} className="border outline-none" />
                            </div>
                        </div>
                    </form>
                </div>
            </section>
        </MainLayout>
    )
}