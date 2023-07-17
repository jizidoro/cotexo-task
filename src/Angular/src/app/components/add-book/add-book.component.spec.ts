import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBookComponent } from './add-book.component';
import { BookCategory } from 'src/app/models/book-category.enum';

describe('AddBookComponent', () => {
  let component: AddBookComponent;
  let fixture: ComponentFixture<AddBookComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddBookComponent]
    });
    fixture = TestBed.createComponent(AddBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it(`should start with empty book`, () => {
    const fixture = TestBed.createComponent(AddBookComponent);
    const app = fixture.componentInstance;
    expect(app.book.title).toEqual("");
    expect(app.book.author).toEqual("");
    expect(app.book.category).toEqual(BookCategory.Romance);
    expect(app.book.pageCount).toEqual(0);
    expect(app.book.isActive).toEqual(false);
  });
});
