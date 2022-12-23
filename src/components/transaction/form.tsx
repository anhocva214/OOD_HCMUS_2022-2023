import { categorySelector } from "@redux/category.redux";
import { transactionSelector } from "@redux/transaction.redux";
import { DatePicker, Form, Input, InputNumber, Modal, Select } from "antd";
import dayjs from "dayjs";
import { useEffect } from "react";
import { useSelector } from "react-redux";
import { TransactionCreate, TransactionUpdate } from "src/models/request/transaction.model";
import { Transaction } from "src/models/response/transaction.model";


interface IProps {
    open: boolean;
    onFinish: (value: TransactionCreate | TransactionUpdate) => void,
    onCancel: () => void,
    defaultValue?: Transaction
}

export default function TransactionForm({
    open,
    onFinish,
    onCancel,
    defaultValue
}: IProps) {
    const [form] = Form.useForm<Transaction>();
    const { categories, loadingAllCategories } = useSelector(categorySelector)
    const {
        loadingUpdateTransaction
    } = useSelector(transactionSelector)

    useEffect(()=>{
        if (defaultValue){
            form.setFieldsValue({
                categoryId: defaultValue.categoryId,
                amount: defaultValue.amount,
                date: dayjs(defaultValue.date),
                note: defaultValue.note
            })
        }
    },[defaultValue])

    return (
        <Modal
            open={open}
            title={defaultValue ? "Cập nhật giao dịch":"Tạo giao dịch mới"}
            okText="Xác nhận"
            cancelText="Huỷ"
            onCancel={onCancel}
            confirmLoading={loadingUpdateTransaction}
            onOk={() => {
                form
                    .validateFields()
                    .then((values) => {
                        form.resetFields();
                        let data = new TransactionCreate({ ...values as any})
                        data.date = new Date(values.date || null).toISOString()
                        onFinish(data)
                    })
                    .catch((info) => {
                        console.log('Validate Failed:', info);
                    });
            }}
        >
            <Form
                form={form}
                layout="vertical"
            >
                <Form.Item
                    name="categoryId"
                    label="Loại giao dịch"
                    rules={[{ required: true, message: 'Không được để trống!' }]}
                >
                    <Select
                        loading={loadingAllCategories}
                        options={categories?.map(item => {
                            return {
                                label: item.name,
                                value: item.id
                            }
                        })}
                    />
                </Form.Item>

                <Form.Item
                    name="amount"
                    label="Số tiền"
                    rules={[{ required: true, message: 'Không được để trống!' }]}
                >
                    <InputNumber min={0} style={{ width: '100%' }} />
                </Form.Item>

                <Form.Item
                    name="note"
                    label="Ghi chú"
                    rules={[{ required: true, message: 'Không được để trống!' }]}
                >
                    <Input />
                </Form.Item>

                <Form.Item
                    name="date"
                    label="Thời gian"
                    rules={[{ required: true, message: 'Không được để trống!' }]}
                >
                    <DatePicker  style={{ width: '100%' }} format='DD/MM/YYYY' placeholder="" />
                </Form.Item>
            </Form>
        </Modal>
    );
};