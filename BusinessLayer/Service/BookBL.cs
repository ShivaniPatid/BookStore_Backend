using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Inerface;
using CommonLayer.Models;
using RepositoryLayer.Interface;

namespace BusinessLayer.Service
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL bookRL;
        public BookBL (IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }
        public BookModel AddBook(BookModel bookModel)
        {
            try
            {
                return bookRL.AddBook(bookModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BookModel UpdateBook(BookModel bookModel)
        {
            try
            {
                return bookRL.UpdateBook(bookModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteBook(int bookId)
        {
            try
            {
                return bookRL.DeleteBook(bookId);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<BookModel> GetAllBooks()
        {
            try
            {
                return bookRL.GetAllBooks();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BookModel GetBookByBookId(int bookId)
        {
            try
            {
                return bookRL.GetBookByBookId(bookId);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
