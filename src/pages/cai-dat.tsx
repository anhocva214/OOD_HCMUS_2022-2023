import MainLayout from "@layouts/main-layout";
import { ROUTES } from "@utils/routes";


export default function SettingPage() {

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
                        <a href="#">Chỉnh Sửa</a>
                    </div>
                    <form className="input-field">
                        <div className="first-item">
                            <div className="name">
                                <p>Họ và tên</p>
                                <input type="text" placeholder="Mafulzul Islam" readOnly />
                            </div>
                            <div className="sex">
                                <p>Giới tính</p>
                                <select name="cars" id="cars">
                                    <option value="nam" selected>Nam</option>
                                    <option value="nu" disabled>Nữ</option>
                                </select>
                            </div>
                        </div>
                        <div className="second-item">
                            <div className="birthday">
                                <p>Ngày sinh nhật</p>
                                <input type="date" placeholder="27/8/2998" readOnly />
                            </div>
                            <div className="phone">
                                <p>Số điện thoại</p>
                                <input type="text" placeholder={'+123456789'} readOnly />
                            </div>
                        </div>
                        <div className="third-item">
                            <p>Email</p>
                            <input type="email" placeholder="abc@gmail.com" readOnly />
                        </div>
                        <div className="fourth-item">
                            <div className="new-pass">
                                <p>Mật khẩu mới</p>
                                <input type="password" placeholder="..." readOnly />
                            </div>
                            <div className="new-pass-confirm">
                                <p>Nhập lại mật khẩu mới</p>
                                <input type="password" placeholder="..." readOnly />
                            </div>
                        </div>
                    </form>
                </div>
            </section>
        </MainLayout>
    )
}