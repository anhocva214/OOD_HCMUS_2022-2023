import { categorySelector } from "@redux/category.redux";
import { useAppDispatch } from "@redux/index";
import { removeTransaction, transactionSelector, updateTransaction } from "@redux/transaction.redux";
import { slugify } from "@utils/string";
import { Button, message, Popconfirm } from "antd";
import Table, { ColumnsType } from "antd/es/table";
import moment from "moment";
import { useState } from "react";
import { useSelector } from "react-redux";
import { TransactionUpdate } from "src/models/request/transaction.model";
import { Transaction } from "src/models/response/transaction.model";
import TransactionForm from "./form";


interface DataType extends Transaction {
    index: number;
}

interface IProps {
    textSearching: string,
    limit?: number
}

export default function TransactionTable(props: IProps) {
    const dispatch = useAppDispatch()
    const {
        transactions,
        loadingRemoveTransaction,
        loadingUpdateTransaction
    } = useSelector(transactionSelector)
    const {
        categories
    } = useSelector(categorySelector)

    const [transactionSelected, setTransactionSelected] = useState<Transaction>(new Transaction())
    const [open, setOpen] = useState(false)


    const columns: ColumnsType<DataType> = [
        {
            title: '#',
            dataIndex: 'index',
            render: (text) => <span className="w-full flex ml-4">{text}</span>,
        },
        {
            title: 'Tên giao dịch',
            dataIndex: 'note',
        },
        {
            title: 'Loại',
            dataIndex: 'categoryId',
            render: (text: string) => (
                <span>{categories.find(item => item.id == text)?.name}</span>
            )
        },
        {
            title: 'Số tiền',
            dataIndex: 'amount',
            render: (text: number) => (
                <span>{text.toLocaleString()}đ</span>
            )
        },
        {
            title: 'Thời gian',
            dataIndex: 'date',
            render: (text: Date) => (
                <span>{moment(text).format('DD/MM/YYYY')}</span>
            )
        },

        !props.limit ? {
            title: 'Hành động',
            key: 'action',
            render: (_, record) => (
                <div className="flex gap-3">
                    <Button
                        onClick={() => {
                            setTransactionSelected(record)
                            setOpen(true)
                        }}
                    >
                        Sửa
                    </Button>
                    <Popconfirm
                        title="Bạn chắc chắn muốn xoá?"
                        onConfirm={() => {
                            setTransactionSelected(record)
                            dispatch(removeTransaction(record.id))
                        }}
                        okText="Có"
                        cancelText="Không"
                    >
                        <Button
                            danger
                            type='dashed'
                            loading={loadingRemoveTransaction && transactionSelected.id == record.id}
                        >
                            Xoá
                        </Button>
                    </Popconfirm>

                </div>
            ),
        }: {},
    ];


    return (
        <>
            <TransactionForm
                open={open}
                onFinish={(values) => {
                    dispatch(updateTransaction(new TransactionUpdate({ ...values, transactionId: transactionSelected.id } as any))).unwrap()
                        .then(date => {
                            message.success("Thành công")
                            setOpen(false)
                        })
                        .catch(error => {
                            message.error("Thất bại")
                        })
                }}
                onCancel={() => {
                    setOpen(false)
                }}
                defaultValue={transactionSelected}
            />
            <Table
                columns={columns}
                dataSource={
                    [...transactions]
                        .reverse()
                        .slice(0, props.limit)
                        .filter(item => slugify(item.note).includes(slugify(props.textSearching)))
                        .map((item, index) => { return { ...item, index: index + 1 } })
                }
                pagination={props.limit && false}
            />
        </>
    )
}
