

export class TransactionCreate {
    userId: string
    categoryId: string
    amount: number
    date: string
    note: string

    constructor()
    constructor(data?: TransactionCreate)
    constructor(data?: any){
        this.userId = data?.userId || null;
        this.categoryId = data?.categoryId || null;
        this.amount = data?.amount || 0;
        this.date = data?.date || null;
        this.note = data?.note || null;
    }
}

export class TransactionUpdate {
    transactionId: string
    categoryId: string
    amount: number
    date: string
    note: string

    constructor()
    constructor(data?: TransactionCreate)
    constructor(data?: any){
        this.transactionId = data?.transactionId || null;
        this.categoryId = data?.categoryId || null;
        this.amount = data?.amount || 0;
        this.date = data?.date || null;
        this.note = data?.note || null;
    }
}