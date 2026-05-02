using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using OleleLibraryNowAPI.Models;
using System.Security.Cryptography.X509Certificates;
using static System.Net.WebRequestMethods;

namespace OleleLibraryNowAPI.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksContoller : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book
            {
           Id= 1,
        Title = "The Gentle Reminder",
        Author = "Bianca Sparacino",
        Genre = "Poetic Self help",
        Available = true,
        PublishedYear = 2021
            },

        new Book
        {
            Id= 1,
        Title = "let Go and Let God",
        Author = "Albert E. Cliffe",
        Genre = "Spritual Self Help",
        Available = true,
        PublishedYear = 1954
        }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new
            {
                status = "success",
                data = books,
                message = "books retrieved"
            });
        } 
        [HttpGet("[id]")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {

                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "book not found"
                });
            }
            return Ok(new
            {
                status = "success",
                data = books,
                message = "book retrieved"
            });
        }
        [HttpPut("[id]")]
        public IActionResult Create([FromBody] Book newBook)
        {
            newBook.Id = books.Count + 1;
            books.Add(newBook);

            return CreatedAtAction(nameof(GetById),
                new 
                {
                    Id = newBook.Id
                },
                new
                {
                    status = "success",
                    data = newBook,
                    message = "Book created. "
                });
        }

        public IActionResult Update(int id,
            [FromBody] Book updateBook)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (Object?)null,
                    message = "Book not found."
                });

            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Genre = updateBook.Genre;
            book.Available = updateBook.Available;
            book.PublishedYear = updateBook.PublishedYear;

            return Ok(new
            {
                status = "success",
                data = book,
                message = "Book updated"
            });
        }

        [HttpDelete("[id]")]
        public IActionResult Delete(int id)
        {
            {
                var book = books.FirstOrDefault(x => x.Id == id);
                if (book == null)
                    return NotFound(new
                    {
                        status = "error",
                        data = (object?)null,
                        message = "Book not found."
                    });
                books.Remove(book);
                return Ok(new
                {
                    status = "success",
                    data = (object?)null,
                    message = "Book deleted."
                });


            }
        }
    }
}

