using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Biblioteca.Api.Controllers
{
    [ApiController]
    [Route("api/livros")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody] CreateBookRequest request,
            CancellationToken cancellationToken)
        {
            var book = await _bookService.CreateAsync(request, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = book.Id },
                book);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var book = await _bookService.GetByIdAsync(id, cancellationToken);

            return Ok(book);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<BookDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Listar(CancellationToken cancellationToken)
        {
            var books = await _bookService.ListAsync(cancellationToken);

            return Ok(books);
        }
    }
}