import { Injectable, inject } from '@angular/core';
import { Router, CanActivateFn } from '@angular/router';
import { AccountService } from '../services/account.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard {
  constructor(
        private router: Router,
        private accountService: AccountService
    ) { }

    canActivate() {
        const user = this.accountService.userValue;
        if (user) {
            // authorised so return true
            return true;
        }

        // not logged in so redirect to login page with the return url
        this.router.navigate(['/']);
        return false;
    }

}

export const canActivate: CanActivateFn = () => {
  return inject(AuthGuard).canActivate();
};
