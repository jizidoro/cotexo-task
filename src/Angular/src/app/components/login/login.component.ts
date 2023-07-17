import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form: any = {
    email: null,
    password: null
  };
  isLogged = false;
  isLoginFailed = false;
  errorMessage = '';

  constructor(
    private accountService: AccountService,
    storageService : StorageService,
    private router : Router
    ) {
    this.isLogged = storageService.isLoggedIn();
  }

  ngOnInit(): void {
    if(this.isLogged){
      this.router.navigate(["/"]);
    }
  }

  onSubmit(): void {
    const { email, password } = this.form;

    this.accountService.login(email, password).subscribe({
      next: res => {
        console.log(res);
        window.location.reload();
      },
      error: err => {
        console.error(err);
        this.errorMessage = err.error.message;
        this.isLoginFailed = true;
      }
    });
  }
}
