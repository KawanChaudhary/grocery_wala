import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';
import { Router } from '@angular/router';
import { faEnvelope, faUser, faPhone, faLockOpen, faLock, faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { UserService } from 'src/app/services/account/user.service';
import { NotifyService } from 'src/app/services/notification/notify.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  // Icons
  emailIcon = faEnvelope;
  userIcon = faUser;
  phoneIcon = faPhone;
  passwordIcon = faLockOpen;
  confirmPasswordIcon = faLock;
  eyeIcon = faEye;
  eyeSlashIcon = faEyeSlash;

  // Hide or show Password Icon
  viewPassword:boolean = false;  

  // Form elements
  signUpForm: FormGroup;

  constructor(private userService: UserService,
              private notifyService: NotifyService,
              private router: Router,
              private formBuilder: FormBuilder
  ){}

  ngOnInit() {
    this.signUpForm = this.formBuilder.group({
      FirstName: ['', Validators.required],
      LastName: [ ],
      Email: ['', [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
      PhoneNumber: ['', [Validators.required, Validators.maxLength(10), Validators.minLength(10), Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]],
      Password: ['', [Validators.required]],
      ConfirmPassword: ['', [Validators.required]],
    }, { validators: this.Validator });

  }

  get f(){
    return this.signUpForm.controls;
  }

  // Check password 
  Validator(formGroup: FormGroup) {

    const firstName = formGroup.get('FirstName').value;
    const lastName = formGroup.get('LastName').value;

    const password = formGroup.get('Password').value;
    const confirmPassword = formGroup.get('ConfirmPassword').value;
    // Regular expressions to match special characters, numbers, and alphabets
    const specialCharRegex = /[!@#$%^&*(),.?":{}|<>]/;
    const numberRegex = /[0-9]/;
    const alphabetRegex = /[a-zA-Z]/;

    if((firstName + lastName).length > 50){
      formGroup.get('FirstName').setErrors({minLength: true});
    }
           
    // Password length check
    if (password.length < 8) {
      formGroup.get('Password').setErrors({minLength: true});
    }    
    // Password complexity check
    else if (!specialCharRegex.test(password)) {
      formGroup.get('Password').setErrors({specialCharacter: true});
    }
    
    else if (!numberRegex.test(password)) {
      formGroup.get('Password').setErrors({number: true});
    }
    
    else if (!alphabetRegex.test(password)) {
      formGroup.get('Password').setErrors({alphabet: true});
    }  
    else if (password !== confirmPassword) {
      formGroup.get('ConfirmPassword').setErrors({ mismatch: true });
    }
     else {
      formGroup.get('ConfirmPassword').setErrors(null);
    }
  }
  
  
  onSubmit() {
    // SignUp logic here
    //console.log(this.signUpForm.value);
    
    if (this.signUpForm.valid) {
      this.userService.signUp(this.signUpForm.value).subscribe(
        {
          next: (response: any) => {
            console.log(response);
            this.router.navigate(['/signin']);
            this.notifyService.showSuccess("Account created successfully.", "Success");
          },
          error: (error: HttpErrorResponse) => {
            this.notifyService.showError(error.error.error, "Failed to add user");
          }
        }
      );
    }
  }

  toggleFieldTextType() {
    if(this.signUpForm.get('Password').value.length > 0){
      this.viewPassword = !this.viewPassword;
    }
  }

}


