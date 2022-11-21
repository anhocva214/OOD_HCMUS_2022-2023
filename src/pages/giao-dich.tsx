import MainLayout from "@layouts/main-layout";
import { ROUTES } from "@utils/routes";


export default function transactionPage() {

    return (
        <MainLayout pageActive={ROUTES.transaction}>
                <section className="transactions-wrapper">
                    <h1>Giao dịch</h1>
                    <article className="search-box">
                        <input type="text" placeholder="Tìm kiếm giao dịch của bạn" />
                        <button><img src="./assets/img/add.png" alt="" />Tạo giao dịch</button>
                    </article>
                    <article className="all-recent-trans">
                        <div className="tables">
                            <table>
                                <tbody><tr>
                                    <th>Tên hạng mục</th>
                                    <th>Loại</th>
                                    <th>Số tiền</th>
                                    <th>Thời gian</th>
                                    <th>Action</th>
                                </tr>
                                    <tr>
                                        <td>Gửi cho Nhi</td>
                                        <td>Chuyển tiền</td>
                                        <td>100.000đ</td>
                                        <td>1/6/2021</td>
                                        <td><button>Xóa</button></td>
                                    </tr>
                                    <tr>
                                        <td>Gửi cho Nhi</td>
                                        <td>Chuyển tiền</td>
                                        <td>100.000đ</td>
                                        <td>1/6/2021</td>
                                        <td><button>Xóa</button></td>
                                    </tr>
                                    <tr>
                                        <td>Gửi cho Nhi</td>
                                        <td>Chuyển tiền</td>
                                        <td>100.000đ</td>
                                        <td>1/6/2021</td>
                                        <td><button>Xóa</button></td>
                                    </tr>
                                </tbody></table>
                        </div>
                    </article>
                </section>
        </MainLayout>
    )
}