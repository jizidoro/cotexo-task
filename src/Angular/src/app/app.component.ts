import { Component } from '@angular/core';
import { StorageService } from './services/storage.service';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  isLoggedIn = false;

  constructor(private storageService: StorageService, private accountService : AccountService)
  {
  }
 ngOnInit(): void {
    this.isLoggedIn = this.storageService.isLoggedIn();
  }

  logout(): void {
    this.accountService.logout();
  }
}
