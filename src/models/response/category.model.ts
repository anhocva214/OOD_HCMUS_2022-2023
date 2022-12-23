

export class Category{
    id: string;
    name: string;
    constructor()
    constructor(data?: Category)
    constructor(data?: any){
        this.id = data?.id || null
        this.name = data?.name || null
    }
}