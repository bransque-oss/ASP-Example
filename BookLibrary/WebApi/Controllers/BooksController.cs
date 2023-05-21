using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Models;
using WebApi.Filters;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [HandleServiceException]
    [ValidateModel]
    public class BooksController : Controller
    {
        private readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseBook>>> GetAll()
        {
            return (await _service.GetBooks()).ToArray();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseBook>> Get(int id)
        {
            var dbBook =  await _service.GetBook(id);
            return dbBook;
        }

        [HttpPost]
        [Authorize(Policy = AuthorizationEnums.ChangeClaim)]
        public async Task<IActionResult> Add(BookCreateUpdateVm inputBook)
        {
            var serviceModel = new RequestBook(inputBook.Title, inputBook.Description, inputBook.Isbn, inputBook.AuthorId.Value);
            await _service.CreateBook(serviceModel);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Policy = AuthorizationEnums.ChangeClaim)]
        public async Task<IActionResult> Update(int id, BookCreateUpdateVm inputBook)
        {
            var serviceModel = new RequestBook(inputBook.Title, inputBook.Description, inputBook.Isbn, inputBook.AuthorId.Value);
            await _service.UpdateBook(id, serviceModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = AuthorizationEnums.ChangeClaim)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteBook(id);
            return NoContent();
        }
    }
}
