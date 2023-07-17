import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, map, startWith } from 'rxjs';
import { Book } from 'src/app/core/models/book.model';
import { BookRepository } from 'src/app/data/repositories/books/book.repository';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-list-books',
  templateUrl: './list-books.component.html',
  styleUrls: ['./list-books.component.css']
})
export class ListBooksComponent implements OnInit{
  isLoggedIn = false;

  hasDeleteFailMessage: boolean = false;
  failMessage: string = "";

	books$: Observable<Book[]> | undefined;
	books: Book[] = [];

	filter = new FormControl('', { nonNullable: true });

  constructor(private bookRepository: BookRepository, private storageService : StorageService) {
    this.isLoggedIn = storageService.isLoggedIn();
  }

  ngOnInit(): void {
    this.retrieveBooks();
  }

  retrieveBooks(): void {
    this.bookRepository.getAll()
      .subscribe({
        next: (res) => {
          if(res.success){
            console.log(res);
            this.books = res.data || [];
            this.loadObservableBooks();
          }else{
            console.error(res);
          }
        },
        error: (e) => console.error(e)
      });
  }

  loadObservableBooks():void{
    this.books$ = this.filter.valueChanges.pipe(
      startWith(''),
      map((text) => this.search(text)),
    );
  }

  removeBook(id?: number): void {
    this.bookRepository.delete(id)
      .subscribe({
        next: (res) => {
          console.log(res);
          this.books = this.books.filter(obj => {
            return obj.id != id
          });
          this.loadObservableBooks();
        },
        error: (e) => {
          console.error(e)
          this.failMessage = e.error.message;
          this.deleteFailMessageOn();
        }
      });
  }

  search(text: string): Book[] {
    return this.books.filter((book) => {
      const term = text.toLowerCase();
      return (
        book.title?.toLowerCase().includes(term) ||
        book.author?.toLowerCase().includes(term) ||
        book.categoryName?.toLowerCase().includes(term)
      );
    });
  }
  deleteFailMessageOn(): void{
    this.hasDeleteFailMessage = true;
  }
  deleteFailMessageOff(): void{
    this.hasDeleteFailMessage = false;
    this.failMessage = "";
  }
}
