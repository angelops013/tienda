import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../../Services/user.service';
import { User } from '../../Models/user';
import { environment } from '../../../environments/environment';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-useredit',
  templateUrl: './useredit.component.html',
  styleUrls: ['./useredit.component.css']
})
export class UsereditComponent implements OnInit {
  user: User;
  forma: FormGroup = new FormGroup({});
  alertErrorMessage: string;
  alertError: boolean;

  constructor(private router: Router, private _http: HttpClient, private userService: UserService, private route: ActivatedRoute) { }

  ngOnInit() {
    let id = this.route.snapshot.params["id"];
    const header = { 'Authorization': 'Bearer ' + this.userService.GetUser().token };
    const configuration = { headers: header };
    this._http.get(environment.baseUrl + 'api/users/' + id, configuration).subscribe((data: User) => {
      this.user = data;
      console.log(this.user);
      this.forma = new FormGroup({
        'username': new FormControl(this.user.username, Validators.required),
        'firstName': new FormControl(this.user.firstName, Validators.required),
        'lastName': new FormControl(this.user.lastName, Validators.required),
        'rol': new FormControl(this.user.rol, Validators.required)
      });
    },
      error => {
        console.log(error);
      });
  }

  Redirect(viewName: string) {
    this.router.navigate(["/" + viewName]);
  }

  Edit() {
    if (!this.forma.invalid) {
      let request = {
        UserName: this.forma.value.username,
        FirstName: this.forma.value.firstName,
        LastName: this.forma.value.lastName,
        Rol: this.forma.value.rol
      };
      const header = { 'Authorization': 'Bearer ' + this.userService.GetUser().token };
      const configuration = { headers: header };
      this._http.put(environment.baseUrl + 'api/Users/' + this.user.id, request, configuration).subscribe((data:any) => {
        this.router.navigate(['/users']);
      },
        error => {
          this.alertErrorMessage = error.error.message;
          this.alertError = true;
        });
    }
  }
}
