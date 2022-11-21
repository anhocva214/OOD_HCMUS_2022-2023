import Head from "next/head";


export default function LoginPage() {

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
                    <form action="#" className="sign-in-form">
                        <h1>MonoLeak</h1>
                        <p>Chào mừng bạn đến với chúng tôi</p>
                        <div className="input-field">
                            <p>Email</p>
                            <input type="text" placeholder="Nhập email của bạn" />
                        </div>
                        <div className="input-field">
                            <p>Mật khẩu</p>
                            <input type="password" placeholder="*******" />
                        </div>
                        <a href="#" className="forget-pass">Quên mật khẩu</a>
                        <button type="submit" className="btn-log-in">Đăng nhập</button>
                        <span className="sign-up-link">Bạn chưa có tài khoản?
                            <a href="./sign-up.html">&nbsp;Đăng kí miễn phí</a></span>
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