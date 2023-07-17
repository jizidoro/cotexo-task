import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Book } from 'src/app/core/models/book.model';
import { BookCategory } from 'src/app/core/models/category.model';
import { BookCategoryRepository } from 'src/app/data/repositories/bookCategories/bookCategory.repository';
import { BookRepository } from 'src/app/data/repositories/books/book.repository';

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.component.html',
  styleUrls: ['./edit-book.component.css']
})
export class EditBookComponent {
  hasSuccessMessage: boolean = false;
  hasFailMessage: boolean = false;
  currentBook: Book = new Book();
  bookCategories: BookCategory[] = [];

  constructor(
    private bookRepository: BookRepository,
    private route: ActivatedRoute,
    private bookCategoryRepository: BookCategoryRepository)
    {
    }

  ngOnInit(): void {
    this.getBook(this.route.snapshot.params["id"]);
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

  getBook(id: string): void {
    this.bookRepository.get(id)
      .subscribe({
        next: (res) => {
          if(res.success && res.data){
            console.log(res);
            this.currentBook = res.data;
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

  updateBook(): void {
    this.bookRepository.update(this.currentBook)
      .subscribe({
        next: (res) => {
          this.successMessageOn();
        },
        error: (e) => {
          console.error(e)
          this.failMessageOn();
        }
      });
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
