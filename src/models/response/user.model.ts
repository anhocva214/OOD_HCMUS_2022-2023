export class User {
    id: string;
    fullName: string;
    email: string;
    gender: string;
    birthday: string;
    phoneNumber: string;
    password: string;

    constructor();
    constructor(obj?: User);
    constructor(obj?: any){
        this.id = obj?.id || null;
        this.fullName = obj?.fullName || null;
        this.email = obj?.email || null;
        this.gender = obj?.gender || "other";
        this.birthday = obj?.birthday || null;
        this.phoneNumber = obj?.phoneNumber || null;
        this.password = obj?.password || null;
    }
}

export class UserUpdate extends User{
    passwordComfirm: string;
    constructor();
    constructor(obj?: UserUpdate);
    constructor(obj?: any){
        super(obj);
        this.passwordComfirm = obj?.passwordComfirm || null;
    }
}