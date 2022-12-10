

export class UserRegister {
    fullname: string;
    email: string;
    password: string;

    constructor()
    constructor(data?: UserRegister)
    constructor(data?: any) {
        this.fullname = data?.fullname || null;
        this.email = data?.email || null;
        this.password = data?.password || null;
    }
}

export class UserLogin {
    email: string;
    password: string;
    constructor()
    constructor(data?: UserLogin)
    constructor(data?: any) {
        this.email = data?.email || null;
        this.password = data?.password || null;
    }
}

export class UserForgotPassword {
    email: string;
    constructor()
    constructor(data?: UserForgotPassword)
    constructor(data?: any) {
        this.email = data?.email || null;
    }
}