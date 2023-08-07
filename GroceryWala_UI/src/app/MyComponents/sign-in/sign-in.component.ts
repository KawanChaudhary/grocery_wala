import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { faEnvelope, faEye, faEyeSlash, faLock } from '@fortawesome/free-solid-svg-icons';
import { UserService } from 'src/app/services/account/user.service';
import { NotifyService } from 'src/app/services/notification/notify.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {

  // Icons
  emailIcon = faEnvelope;
  passwordIcon = faLock
  eyeIcon = faEye;
  eyeSlashIcon = faEyeSlash;
  
  // Hide or show Password Icon
  viewPassword:boolean = false;  

  // Form elements
  signInForm: FormGroup;

  constructor(private userService: UserService,
    private notifyService: NotifyService,
    private router: Router,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    if(this.userService.isUserAuthenticated()){
      this.router.navigate(['/']); 
    }
      this.signInForm = this.formBuilder.group({
        Email: ['', [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
        Password: ['', [Validators.required, Validators.minLength(5)]],
      });

  }

  get f(){
    return this.signInForm.controls;
  }

  onSubmit() {
    // SignIn logic here
    //console.log(this.signInForm.value);

    if (this.signInForm.valid) {
      this.userService.signIn(this.signInForm.value).subscribe(
        {
          next: (response: any) => {
            const token = (<any>response).token;
            // console.log(token);
            
            localStorage.setItem('jwt', token);
            this.router.navigate(['/']);
            this.notifyService.showSuccess("Sign In successfully.", "Success");
            window.location.reload();
          },
          error: (error: HttpErrorResponse ) => {
            var msg = error.error ? error.error : "Something went wrong. Please try again"
            this.notifyService.showError(msg, "Failed");
            //console.log(error);
          }
        }
      );
    }
  }

  toggleFieldTextType() {
    if(this.signInForm.get('Password').value.length > 0){
      this.viewPassword = !this.viewPassword;
    }
  }

}
