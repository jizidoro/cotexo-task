import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/internal/operators/map';
import { BookCategory } from 'src/app/core/models/category.model';
import { BookCategoryMapper } from './bookCategory.mapper';
import { BookCategoryEntity } from './bookCategory.entity';
import { SingleResultModel } from 'src/app/core/utils/response/single-result.model';

const baseUrl = 'https://localhost:44304/api/v1/category';

@Injectable({
  providedIn: 'root'
})
export class BookCategoryRepository {

  mapper : BookCategoryMapper = new BookCategoryMapper();

  constructor(private http: HttpClient) { }

  get(id: any): Observable<SingleResultModel<BookCategory>> {
    return this.http.get<SingleResultModel<BookCategoryEntity>>(`${baseUrl}/${id}`)
      .pipe(map((x) => this.mapper.responseWebMapFrom(x)));
  }
  getAllDropDown(): Observable<SingleResultModel<BookCategory[]>> {
    return this.http.get<SingleResultModel<BookCategoryEntity[]>>(`${baseUrl}/get-all-dropdown`)
      .pipe(map((x) => this.mapper.responseWebMapFromList(x)));
  }
}
