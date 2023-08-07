export class SignUpUserModel{

    FirstName: string;
    LastName: string;
    PhoneNumber: string;
    Email: string;
    Password: string;
    ConfirmPassword: string;

    constructor(firstName: string, lastName: string, phoneNumber, email: string, password: string, confirmPassword: string){
        this.FirstName = firstName;
        this.LastName = lastName;
        this.PhoneNumber = phoneNumber;
        this.Email = email;
        this.Password = password;
        this.ConfirmPassword = confirmPassword;
    }

}