import { Book } from "src/app/core/models/book.model";
import { BookEntity } from "./book.entity";
import { Mapper } from "src/app/core/utils/base/mapper";

export class BookMapper extends Mapper<BookEntity, Book>
{
  mapFrom(param: BookEntity): Book {
    return {
      id: param.id,
      title: param.livro,
      author: param.autor,
      categoryId: param.categoryId,
      categoryName: param.categoryName,
      pageCount: param.totalPaginas,
      isActive: param.isActive
    };
  }

  mapTo(param: Book): BookEntity {
    return {
      id: param.id,
      livro: param.title,
      autor: param.author,
      categoryId: param.categoryId,
      categoryName: param.categoryName,
      totalPaginas: param.pageCount,
      isActive: param.isActive
    };
  }
}
