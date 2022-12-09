

export class UserRegister{
    fullname: string;
    email: string;
    password: string;

    constructor()
    constructor(data?: UserRegister)
    constructor(data?: any){
        this.fullname = data?.fullname || null;
        this.email = data?.email || null;
        this.password = data?.password || null;
    }
}