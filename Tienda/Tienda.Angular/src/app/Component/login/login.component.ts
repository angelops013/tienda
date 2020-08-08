import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { User } from '../../Models/user';
import { UserService } from '../../Services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  forma: FormGroup = new FormGroup({});
  disableLoginButton: boolean;
  showLoginMessages: boolean;
  alertErrorMessage: string;
  alertError: boolean;

  constructor(private _http: HttpClient, private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.forma = new FormGroup({
      'user': new FormControl('', Validators.required),
      'password': new FormControl('', Validators.required)
    });
  }

  Login() {
    this.disableLoginButton = true;

    if (this.forma.invalid) {
      this.showLoginMessages = true;
      this.disableLoginButton = false;
    } else {
      const request = {
        UserName: this.forma.value.user,
        Password: this.forma.value.password
      };

      this._http.post(environment.baseUrl + 'api/Users/Login', request).subscribe((data: User) => {
        this.userService.SaveUser(data);
        this.router.navigate(['/shows']);
      },
        error => {
          this.alertErrorMessage = error.error.message;
          this.alertError = true;
          this.disableLoginButton = false;
        });
    }
  }

}

