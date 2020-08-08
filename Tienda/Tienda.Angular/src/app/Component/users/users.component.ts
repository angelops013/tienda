import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../../Services/user.service';
import { environment } from '../../../environments/environment';
import { User } from '../../Models/user';
import { Router } from '@angular/router';

declare var jQuery: any;
declare var $: any;

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  users: User[];
  idUserToDelete: string;
  constructor(private _http: HttpClient, private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.GetUsers();
  }

  GetUsers() {
    const header = { 'Authorization': 'Bearer ' + this.userService.GetUser().token };
    const configuration = { headers: header };
    this._http.get(environment.baseUrl + 'api/users', configuration).subscribe((data: User[]) => {
      this.users = data;
    },
      error => {
        console.log(error);
      });
  }

  EditUser(id: string) {
    this.router.navigate(["/user/" + id]);
  }

  DeleteUser(id: string) {
    this.idUserToDelete = id;
    $('#userDeleteModal').modal('show');
  }

  ConfirmDeleteUser() {
    const header = { 'Authorization': 'Bearer ' + this.userService.GetUser().token };
    const configuration = { headers: header };
    this._http.delete(environment.baseUrl + 'api/users/' + this.idUserToDelete, configuration).subscribe((data: any) => {
      this.GetUsers();
    },
      error => {
        console.log(error);
      });
      this.ClosedModal();
  }

  ClosedModal() {
    this.idUserToDelete = null;
    $('#userDeleteModal').modal('hide');
  }

  CreateUser(){
    this.router.navigate(["/user"]);
  }
}
