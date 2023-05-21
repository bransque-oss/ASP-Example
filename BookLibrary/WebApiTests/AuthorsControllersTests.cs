using Data;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Services;
using Services.Exceptions;
using WebApi.Controllers;
using WebApi.ViewModels;

namespace WebApiTests
{
    public class AuthorsControllersTests
    {
        [Fact]
        public async void GetAll()
        {
            // Arrange
            var authorsRepoMock = new Mock<IAuthorRepository>();
            authorsRepoMock.Setup(x => x.GetAll()).ReturnsAsync(GetInitialAuthorsSet);
            var authorsServiceMock = new Mock<AuthorService>(authorsRepoMock.Object);
            var controller = new AuthorsController(authorsServiceMock.Object);

            // Act
            var resultSet = (await controller.GetAll()).Value;

            // Assert
            Assert.Equal(2, resultSet.Count());
            var author = resultSet.First();
            Assert.Equal(1, author.Id);
            Assert.Equal("DescAuthor1", author.Description);
            Assert.Equal("Author1", author.Name);
        }

        [Fact]
        public async void Get()
        {
            // Arrange
            var authorsRepoMock = new Mock<IAuthorRepository>();
            authorsRepoMock.Setup(x => x.GetDetailed(It.IsAny<int>())).ReturnsAsync(GetInitialAuthorsSet().First());
            var authorsServiceMock = new Mock<AuthorService>(authorsRepoMock.Object);
            var controller = new AuthorsController(authorsServiceMock.Object);

            // Act
            var author = (await controller.Get(1)).Value;

            // Assert
            authorsRepoMock.Verify(x => x.GetDetailed(1));
            Assert.Equal(1, author.Id);
            Assert.Equal("Author1", author.Name);
            Assert.Equal("DescAuthor1", author.Description);
            Assert.Equal(2, author.Books.Count());

            var book = author.Books.FirstOrDefault();
            Assert.Equal(1, book.Id);
            Assert.Equal("DescTitleBook1", book.Description);
            Assert.Equal("book-1", book.Isbn);
            Assert.Equal("Author1", author.Name);
            Assert.Equal(1, book.AuthorId);
            Assert.Equal("titleBook1", book.Title);
        }

        [Fact]
        public async void Get_ThrowsException()
        {
            // Arrange
            var authorsRepoMock = new Mock<IAuthorRepository>();
            authorsRepoMock.Setup(x => x.GetDetailed(It.IsAny<int>())).ReturnsAsync((DbAuthor)null);
            var authorsServiceMock = new Mock<AuthorService>(authorsRepoMock.Object);
            var controller = new AuthorsController(authorsServiceMock.Object);

            // Assert
            await Assert.ThrowsAsync<ForUserException>(async () => await controller.Get(It.IsAny<int>()));
        }

        [Fact]
        public async void Add()
        {
            // Arrange
            var author = new AuthorCreateUpdateVm
            {
                Name = "Author1",
                Description = "DescAuthor1"
            };
            var authorsRepoMock = new Mock<IAuthorRepository>();
            var authorsServiceMock = new Mock<AuthorService>(authorsRepoMock.Object);
            var controller = new AuthorsController(authorsServiceMock.Object);

            // Act
            var result = await controller.Add(author);

            // Assert
            authorsRepoMock.Verify(x => x.Add(It.Is<DbAuthor>(a => a.Name == author.Name && a.Description == author.Description)));
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void Update()
        {
            // Arrange
            var author = new AuthorCreateUpdateVm
            {
                Name = "Author1",
                Description = "DescAuthor1"
            };
            var authorId = 1;
            var authorsRepoMock = new Mock<IAuthorRepository>();
            authorsRepoMock.Setup(x => x.IsExist(It.IsAny<int>())).ReturnsAsync(true);
            var authorsServiceMock = new Mock<AuthorService>(authorsRepoMock.Object);
            var controller = new AuthorsController(authorsServiceMock.Object);

            // Act
            var result = await controller.Update(authorId, author);

            // Assert
            authorsRepoMock.Verify(x => x.Update(It.Is<DbAuthor>(a => a.Id == authorId && a.Name == author.Name && a.Description == author.Description)));
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void Update_ThrowsException()
        {
            // Arrange
            var author = new AuthorCreateUpdateVm
            {
                Name = "Author1",
                Description = "DescAuthor1"
            };
            var authorsRepoMock = new Mock<IAuthorRepository>();
            authorsRepoMock.Setup(x => x.IsExist(It.IsAny<int>())).ReturnsAsync(false);
            var authorsServiceMock = new Mock<AuthorService>(authorsRepoMock.Object);
            var controller = new AuthorsController(authorsServiceMock.Object);

            // Assert
            await Assert.ThrowsAsync<ForUserException>(async () => await controller.Update(It.IsAny<int>(), author));
        }

        [Fact]
        public async void Delete()
        {
            // Arrange
            var authorId = 1;
            var authorsRepoMock = new Mock<IAuthorRepository>();
            authorsRepoMock.Setup(x => x.IsExist(It.IsAny<int>())).ReturnsAsync(true);
            var authorsServiceMock = new Mock<AuthorService>(authorsRepoMock.Object);
            var controller = new AuthorsController(authorsServiceMock.Object);

            // Act
            var result = await controller.Delete(authorId);

            // Assert
            authorsRepoMock.Verify(x => x.Delete(authorId));
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void Delete_ThrowsException()
        {
            // Arrange
            var authorsRepoMock = new Mock<IAuthorRepository>();
            authorsRepoMock.Setup(x => x.IsExist(It.IsAny<int>())).ReturnsAsync(false);
            var authorsServiceMock = new Mock<AuthorService>(authorsRepoMock.Object);
            var controller = new AuthorsController(authorsServiceMock.Object);

            // Assert
            await Assert.ThrowsAsync<ForUserException>(async () => await controller.Delete(It.IsAny<int>()));
        }

        private IEnumerable<DbAuthor> GetInitialAuthorsSet()
        {
            return new List<DbAuthor>
            {
                new DbAuthor
                {
                    Id = 1,
                    Description = "DescAuthor1",
                    Name = "Author1",
                    Books = new List<DbBook> 
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
                    }
                },
                new DbAuthor
                {
                    Id = 2,
                    Description = "DescAuthor2",
                    Name = "Author2",
                    Books = new List<DbBook>
                    {
                        new DbBook
                        {
                            Id = 1,
                            Description = "descTitleBook3",
                            AuthorId = 2,
                            Isbn = "book-3",
                            Title = "titleBook3",
                        },
                        new DbBook
                        {
                            Id = 2,
                            Description = "descTitleBook4",
                            AuthorId = 2,
                            Isbn = "book-4",
                            Title = "titleBook4",
                        }
                    }
                }
            };
        }
    }
}