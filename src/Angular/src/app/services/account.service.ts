import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Token } from '../core/models/token.model';
import { StorageService } from './storage.service';

@Injectable({ providedIn: 'root' })
export class AccountService {
    private tokenSubject: BehaviorSubject<Token | null>;
    public token: Observable<Token | null>;

    constructor(
        private router: Router,
        private http: HttpClient,
        private storage : StorageService
    ) {
        this.tokenSubject = new BehaviorSubject(storage.getToken());
        this.token = this.tokenSubject.asObservable();
    }

    public get userValue() {
        return this.tokenSubject.value;
    }

    login(email: string, password: string) {
        return this.http.post<any>(`${environment.apiUrl}/token/generate-token`, { email, password })
            .pipe(map(res => {
                this.storage.saveToken(res.data);
                this.tokenSubject.next(res.data);
                return res.data;
            }));
    }

    logout() {
        this.storage.clean();
        this.tokenSubject.next(null);
        window.location.reload();
    }
}
