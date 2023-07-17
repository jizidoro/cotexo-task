import { Component, OnInit } from '@angular/core';
import { Book } from 'src/app/core/models/book.model';
import { BookCategory } from 'src/app/core/models/category.model';
import { BookCategoryRepository } from 'src/app/data/repositories/bookCategories/bookCategory.repository';
import { BookRepository } from 'src/app/data/repositories/books/book.repository';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.css']
})
export class AddBookComponent implements OnInit{
  hasSuccessMessage: boolean = false;
  hasFailMessage: boolean = false;
  currentBook: Book = new Book();
  bookCategories: BookCategory[] = [];

  constructor(
    private bookRepository: BookRepository,
    private bookCategoryRepository: BookCategoryRepository
  ) {
    this.newBook();
  }

  ngOnInit(): void {
    this.loadBookCategories();
  }

  loadBookCategories(){
    this.bookCategoryRepository.getAllDropDown()
      .subscribe({
        next: (res) => {
          if(res.success){
            console.log(res);
            this.bookCategories = res.data || [];
          }else{
            console.error(res)
            this.failMessageOn();
          }
        },
        error: (e) => {
          console.error(e)
          this.failMessageOn();
        }
      });
  }

  saveBook(): void {
    const data = {...this.currentBook};

    this.bookRepository.create(data)
      .subscribe({
        next: (res) => {
          console.log(res);
          this.newBook();
          this.successMessageOn();
        },
        error: (e) => {
          console.error(e)
          this.failMessageOn();
        }
      });
  }

  newBook(): void {
    this.currentBook = {
      title: '',
      author: '',
      categoryId: 0,
      pageCount: 0,
      isActive: false
    };
  }

  successMessageOn(): void{
    this.hasSuccessMessage = true;
  }
  successMessageOff(): void{
    this.hasSuccessMessage = false;
  }
  failMessageOn(): void{
    this.hasFailMessage = true;
  }
  failMessageOff(): void{
    this.hasFailMessage = false;
  }
}
