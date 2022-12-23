import { userApi } from "@apis/user.api";
import TransactionForm from "@components/transaction/form";
import MainLayout from "@layouts/main-layout";
import { useAppDispatch } from "@redux/index";
import { classifyTransactions, createTransaction } from "@redux/transaction.redux";
import { ROUTES } from "@utils/routes";
import { message } from "antd";
import { GetServerSideProps } from "next";
import dynamic from "next/dynamic";
import { useState } from "react";
import { TransactionCreate } from "src/models/request/transaction.model";
import { User } from "src/models/response/user.model";


const TransactionTable = dynamic(() => import('@components/transaction/table'), {
    ssr: false,
})


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

export default function TransactionPage({ user }: IProps) {
    const dispatch = useAppDispatch()
    const [open, setOpen] = useState(false)
    const [textSearching, setTextSearching] = useState('')

    const onCreate = (values: TransactionCreate) => {
        values.userId = user.id
        dispatch(createTransaction(values)).unwrap()
            .then(data => {
                message.success("Thành công")
                dispatch(classifyTransactions())
                setOpen(false)
            })
            .catch(error => {
                console.log("🚀 ~ file: giao-dich.tsx:44 ~ onCreate ~ error", error)
                message.error(error.response.data.errorMessage)
            })
    }

    return (
        <MainLayout pageActive={ROUTES.transaction}>
            <section className="transactions-wrapper">
                <h1>Giao dịch</h1>
                <article className="search-box">
                    <input onChange={(e) => setTextSearching(e.target.value)} value={textSearching} type="text" placeholder="Tìm kiếm giao dịch của bạn" />
                    <button onClick={() => setOpen(true)} className="justify-center"><img src="./assets/img/add.png" alt="" />Tạo giao dịch</button>
                </article>

                <TransactionForm
                    open={open}
                    onFinish={onCreate}
                    onCancel={() => {
                        setOpen(false)
                    }}
                />

                <article className="all-recent-trans">
                    <TransactionTable 
                        textSearching={textSearching}
                    />
                </article>
            </section>
        </MainLayout>
    )
}