import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../../Services/user.service';
import { User } from '../../../Models/user';


declare var jQuery: any;
declare var $: any;

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  user: User;
  constructor(private router: Router, private userService: UserService) { }

  ngOnInit() {
    this.user = this.userService.GetUser();
  }

  Redirect(viewName: string) {
    this.router.navigate(["/" + viewName]);
  }

  OutOfSesion() {
    this.userService.RemoveUser();
    this.router.navigate(["/login"]);
  }
}
