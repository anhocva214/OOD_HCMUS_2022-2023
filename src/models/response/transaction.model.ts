export class Transaction {
    id: string;
    userId: string
    categoryId: string
    amount: number
    date: Date
    note: string
    modifiedAt: Date
    createdAt: Date

    constructor()
    constructor(data?: Transaction)
    constructor(data?: any){
        this.id = data?.id || null;
        this.userId = data?.userId || null;
        this.categoryId = data?.categoryId || null;
        this.amount = data?.amount || 0;
        this.date = data?.date ? new Date(data.date) : null;
        this.note = data?.note || null;
        this.modifiedAt = data?.modifiedAt ? new Date(data.modifiedAt) : null;
        this.createdAt =  data?.createdAt ? new Date(data.createdAt) : null;
    }
}