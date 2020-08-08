import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../../Services/user.service';
import { environment } from '../../../environments/environment';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-usercreate',
  templateUrl: './usercreate.component.html',
  styleUrls: ['./usercreate.component.css']
})
export class UsercreateComponent implements OnInit {
  forma: FormGroup = new FormGroup({});
  alertErrorMessage: string;
  alertError: boolean;

  constructor(private router: Router, private _http: HttpClient, private userService: UserService) { }

  ngOnInit() {
    this.forma = new FormGroup({
      'username': new FormControl('', Validators.required),
      'firstName': new FormControl('', Validators.required),
      'lastName': new FormControl('', Validators.required),
      'rol': new FormControl('', Validators.required),
      'password': new FormControl('', Validators.required)
    });
  }

  Create(){
    if (!this.forma.invalid) {
      let request = {
        UserName: this.forma.value.username,
        FirstName: this.forma.value.firstName,
        LastName: this.forma.value.lastName,
        Role: this.forma.value.rol,
        Password: this.forma.value.password
      };
      const header = { 'Authorization': 'Bearer ' + this.userService.GetUser().token };
      const configuration = { headers: header };
      this._http.post(environment.baseUrl + 'api/Users/Register', request, configuration).subscribe((data:any) => {
        this.router.navigate(['/users']);
      },
        error => {
          this.alertErrorMessage = error.error.message;
          this.alertError = true;
        });
    }
  }
}
