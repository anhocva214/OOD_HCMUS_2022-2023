import { CATEGORIES } from "@redux/category.redux";


export class Category{
    id: string;
    name: typeof CATEGORIES[number];
    constructor()
    constructor(data?: Category)
    constructor(data?: any){
        this.id = data?.id || null
        this.name = data?.name || null
    }
}