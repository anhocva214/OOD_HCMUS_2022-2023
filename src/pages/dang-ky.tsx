import Head from "next/head";


export default function RegisterPage() {
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
                        <form action="#" className="sign-up-form">
                            <h1>Tạo tài khoản mới</h1>
                            <p>Bạn cần điền đầy đủ thông tin để tiếp tục</p>
                            <div className="input-field">
                                <p>Họ và tên</p>
                                <input type="text" placeholder="Nhập Họ và tên" />
                            </div>
                            <div className="input-field">
                                <p>Email</p>
                                <input type="email" placeholder="Nhập Email" />
                            </div>
                            <div className="input-field">
                                <p>Mật khẩu</p>
                                <input type="password" placeholder="*******" />
                            </div>
                            <button type="submit" className="btn-sign-up">Đăng kí</button>
                            <span className="sign-in-link">Bạn đã có tài khoản?
                                <a href="./sign-in.html">&nbsp;Đăng nhập</a></span>
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