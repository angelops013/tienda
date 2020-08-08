import { Injectable } from '@angular/core';
import { UserService } from './user.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class GuardService {

  constructor(private userService: UserService, private router: Router) {
  }

  async canActivate() {
    if (this.userService.ValidateUser()) {
      this.router.navigate(['/login']);
      return false;
    } else {
      try {
        const data = await this.userService.GetJWTSessionValidation()
          .toPromise();
        if (Object.values(data)[0] === true) {
          return true;
        } else {
          this.router.navigate(['/login']);
          return false;
        }
      } catch (e) {
        this.router.navigate(['/login']);
        return false;
      }

    }
  }
}
