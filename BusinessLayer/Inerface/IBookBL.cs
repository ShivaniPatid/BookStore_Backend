using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;

namespace BusinessLayer.Inerface
{
    public interface IBookBL
    {
        public BookModel AddBook(BookModel bookModel);
        public BookModel UpdateBook(BookModel bookModel);
        public bool DeleteBook(int bookId);
        public List<BookModel> GetAllBooks();
        public BookModel GetBookByBookId(int bookId);

    }
}
