export class User {
    id: string;
    fullname: string;
    email: string;
    gender: string;
    birthday: string;
    phoneNumber: string;

    constructor();
    constructor(obj?: User);
    constructor(obj?: any){
        this.id = obj?.id || null;
        this.fullname = obj?.fullname || null;
        this.email = obj?.email || null;
        this.gender = obj?.gender || null;
        this.birthday = obj?.birthday || null;
        this.phoneNumber = obj?.phoneNumber || null;
    }
}