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
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _service;

        public AuthorsController(IAuthorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorVm>>> GetAll()
        {
            var dbAuthors = await _service.GetAuthors();
            return dbAuthors.Select(x => new AuthorVm(x.Id, x.Name, x.Description))
                .ToArray();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseAuthor>> Get(int id)
        {
            var dbAuthor =  await _service.GetAuthorDetails(id);
            return dbAuthor;
        }

        [HttpPost]
        [Authorize(Policy = AuthorizationEnums.ChangeClaim)]
        public async Task<IActionResult> Add(AuthorCreateUpdateVm inputAuthor)
        {
            var serviceModel = new RequestAuthor(inputAuthor.Name, inputAuthor.Description);
            await _service.CreateAuthor(serviceModel);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Policy = AuthorizationEnums.ChangeClaim)]
        public async Task<IActionResult> Update(int id, AuthorCreateUpdateVm inputAuthor)
        {
            var serviceModel = new RequestAuthor(inputAuthor.Name, inputAuthor.Description);
            await _service.UpdateAuthor(id, serviceModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = AuthorizationEnums.ChangeClaim)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAuthor(id);
            return NoContent();
        }
    }
}
