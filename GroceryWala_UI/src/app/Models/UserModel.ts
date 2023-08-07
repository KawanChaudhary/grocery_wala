export class UserModel{
    id: string;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    email: string;
    isAdmin: boolean;

    constructor(id:string, firstName:string, lastName:string, phoneNumber:string, email:string, isAdmin:boolean){
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.phoneNumber = phoneNumber;
        this.email = email;
        this.isAdmin = isAdmin;
    }
}