using Data;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Services;
using Services.Exceptions;
using WebApi.Controllers;
using WebApi.ViewModels;

namespace WebApiTests
{
    public class BooksControllersTests
    {
        [Fact]
        public async void GetAll()
        {
            // Arrange
            var booksRepoMock = new Mock<IRepository<DbBook>>();
            booksRepoMock.Setup(x => x.GetAll()).ReturnsAsync(GetInitialBooksSet);
            var booksServiceMock = new Mock<BookService>(booksRepoMock.Object);
            var controller = new BooksController(booksServiceMock.Object);

            // Act
            var resultSet = (await controller.GetAll()).Value;

            // Assert
            Assert.Equal(2, resultSet.Count());
            var book = resultSet.First();
            Assert.Equal(1, book.Id);
            Assert.Equal("titleBook1", book.Title);
            Assert.Equal("DescTitleBook1", book.Description);
            Assert.Equal("book-1", book.Isbn);
            Assert.Equal(1, book.AuthorId);
        }

        [Fact]
        public async void Get()
        {
            // Arrange
            var booksRepoMock = new Mock<IRepository<DbBook>>();
            booksRepoMock.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(GetInitialBooksSet().First());
            var booksServiceMock = new Mock<BookService>(booksRepoMock.Object);
            var controller = new BooksController(booksServiceMock.Object);

            // Act
            var book = (await controller.Get(1)).Value;

            // Assert
            booksRepoMock.Verify(x => x.Get(1));
            Assert.Equal(1, book.Id);
            Assert.Equal("titleBook1", book.Title);
            Assert.Equal("DescTitleBook1", book.Description);
            Assert.Equal("book-1", book.Isbn);
            Assert.Equal(1, book.AuthorId);
        }

        [Fact]
        public async void Get_ThrowsException()
        {
            // Arrange
            var booksRepoMock = new Mock<IRepository<DbBook>>();
            booksRepoMock.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((DbBook)null);
            var booksServiceMock = new Mock<BookService>(booksRepoMock.Object);
            var controller = new BooksController(booksServiceMock.Object);

            // Assert
            await Assert.ThrowsAsync<ForUserException>(async () => await controller.Get(It.IsAny<int>()));
        }

        [Fact]
        public async void Add()
        {
            // Arrange
            var book = new BookCreateUpdateVm
            {
                Title = "Title1",
                Isbn = "book-1",
                Description = "DescBook1",
                AuthorId = 1
            };
            var booksRepoMock = new Mock<IRepository<DbBook>>();
            var booksServiceMock = new Mock<BookService>(booksRepoMock.Object);
            var controller = new BooksController(booksServiceMock.Object);

            // Act
            var result = await controller.Add(book);

            // Assert
            booksRepoMock.Verify(x => x.Add(It.Is<DbBook>(b => b.Title == book.Title && b.Description == book.Description && b.Isbn == book.Isbn && b.AuthorId == book.AuthorId)));
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void Update()
        {
            // Arrange
            var book = new BookCreateUpdateVm
            {
                Title = "Title1",
                Isbn = "book-1",
                Description = "DescBook1",
                AuthorId = 2
            };
            var bookId = 1;
            var booksRepoMock = new Mock<IRepository<DbBook>>();
            booksRepoMock.Setup(x => x.IsExist(It.IsAny<int>())).ReturnsAsync(true);
            var booksServiceMock = new Mock<BookService>(booksRepoMock.Object);
            var controller = new BooksController(booksServiceMock.Object);

            // Act
            var result = await controller.Update(bookId, book);

            // Assert
            booksRepoMock.Verify(x => x.Update(It.Is<DbBook>(b => b.Id == bookId && b.Title == book.Title && b.Description == book.Description && b.Isbn == book.Isbn && b.AuthorId == book.AuthorId)));
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void Update_ThrowsException()
        {
            // Arrange
            var book = new BookCreateUpdateVm
            {
                Title = "Title1",
                Isbn = "book-1",
                Description = "DescBook1",
                AuthorId = 2
            };
            var booksRepoMock = new Mock<IRepository<DbBook>>();
            booksRepoMock.Setup(x => x.IsExist(It.IsAny<int>())).ReturnsAsync(false);
            var booksServiceMock = new Mock<BookService>(booksRepoMock.Object);
            var controller = new BooksController(booksServiceMock.Object);

            // Assert
            await Assert.ThrowsAsync<ForUserException>(async () => await controller.Update(It.IsAny<int>(), book));
        }

        [Fact]
        public async void Delete()
        {
            // Arrange
            var bookId = 1;
            var booksRepoMock = new Mock<IRepository<DbBook>>();
            booksRepoMock.Setup(x => x.IsExist(It.IsAny<int>())).ReturnsAsync(true);
            var booksServiceMock = new Mock<BookService>(booksRepoMock.Object);
            var controller = new BooksController(booksServiceMock.Object);

            // Act
            var result = await controller.Delete(bookId);

            // Assert
            booksRepoMock.Verify(x => x.Delete(bookId));
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void Delete_ThrowsException()
        {
            // Arrange
            var booksRepoMock = new Mock<IRepository<DbBook>>();
            booksRepoMock.Setup(x => x.IsExist(It.IsAny<int>())).ReturnsAsync(false);
            var booksServiceMock = new Mock<BookService>(booksRepoMock.Object);
            var controller = new BooksController(booksServiceMock.Object);

            // Assert
            await Assert.ThrowsAsync<ForUserException>(async () => await controller.Delete(It.IsAny<int>()));
        }

        private IEnumerable<DbBook> GetInitialBooksSet()
        {
            return new List<DbBook>
            {
                new DbBook
                {
                    Id = 1,
                    Description = "DescTitleBook1",
                    AuthorId = 1,
                    Isbn = "book-1",
                    Title = "titleBook1",
                },
                new DbBook
                {
                    Id = 2,
                    Description = "DescTitleBook2",
                    AuthorId = 1,
                    Isbn = "book-2",
                    Title = "titleBook2",
                }
            };              
        }
    }
}