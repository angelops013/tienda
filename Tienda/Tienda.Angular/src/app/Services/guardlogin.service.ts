import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})

export class GuardloginService implements CanActivate {

  // variables de configuración de empresa
  companyConfiguration;
  urlServer;
  companyIdentifier;

  constructor(private router: Router, private userService: UserService) {
  }

  async canActivate() {
    if (this.userService.ValidateUser()) {
      return true;
    } else {
      try {
        const data = await this.userService.GetJWTSessionValidation()
          .toPromise();
        if (Object.values(data)[0] == true) {
          this.router.navigate(['']);
          return false;
        } else {
          return true;
        }
      } catch (e) {
        return true;
      }
    }
  }
}

