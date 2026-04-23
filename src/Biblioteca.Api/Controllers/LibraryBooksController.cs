using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Biblioteca.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibraryBooksController : ControllerBase
    {
        public LibraryBooksController() 
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetAllLoans()
        {
            try
            {
                return Ok("Get de Empréstimos de Livros");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar buscar todos os empréstimos de livros. Erro: {ex.Message}");
            }
        }
    }
}