import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { BookMapper } from './book.mapper';
import { map } from 'rxjs/internal/operators/map';
import { Book } from 'src/app/core/models/book.model';
import { SingleResultModel } from 'src/app/core/utils/response/single-result.model';
import { BookEntity } from './book.entity';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BookRepository {

  mapper : BookMapper = new BookMapper();

  constructor(private http: HttpClient) { }

  getAll(): Observable<SingleResultModel<Book[]>> {
    return this.http.get<SingleResultModel<BookEntity[]>>(`${environment.apiUrl}/book/get-all`)
      .pipe(map((x) => this.mapper.responseWebMapFromList(x)));
  }

  get(id: any): Observable<SingleResultModel<Book>> {
    return this.http.get<SingleResultModel<BookEntity>>(`${environment.apiUrl}/book/get-by-id/${id}`)
      .pipe(map((x) => this.mapper.responseWebMapFrom(x)));
  }

  create(data: Book): Observable<any> {
    return this.http.post(`${environment.apiUrl}/book/create`, this.mapper.mapTo(data));
  }

  update(data: Book): Observable<any> {
    return this.http.put(`${environment.apiUrl}/book/edit`, this.mapper.mapTo(data));
  }

  delete(id: any): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/book/delete/${id}`);
  }
}
