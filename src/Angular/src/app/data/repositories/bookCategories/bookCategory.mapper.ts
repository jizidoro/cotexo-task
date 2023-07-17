import { BookCategory } from "src/app/core/models/category.model";
import { BookCategoryEntity } from "./bookCategory.entity";
import { Mapper } from "src/app/core/utils/base/mapper";

export class BookCategoryMapper extends Mapper<BookCategoryEntity, BookCategory> {
  mapFrom(param: BookCategoryEntity): BookCategory {
    return {
      id: param.id,
      name: param.name
    };
  }

  mapTo(param: BookCategory): BookCategoryEntity {
    return {
      id: param.id,
      name: param.name
    };
  }
}
