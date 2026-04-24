using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Biblioteca.Api.Controllers
{
    [ApiController]
    [Route("api/emprestimos")]
    public class BookLoansController : ControllerBase
    {
        private readonly IBookLoanService _bookLoanService;

        public BookLoansController(IBookLoanService bookLoanService)
        {
            _bookLoanService = bookLoanService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BookLoanDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PickUp(
        [FromBody] BookLoanRequest request,
        CancellationToken cancellationToken)
        {
            var bookLoan = await _bookLoanService.PickUpAsync(
                request,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = bookLoan.Id },
                bookLoan);
        }

        [HttpPatch("{id:guid}/devolucao")]
        [ProducesResponseType(typeof(BookLoanDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Deliver(
            Guid id,
            CancellationToken cancellationToken)
        {
            var bookLoan = await _bookLoanService.DeliverAsync(
                id,
                cancellationToken);

            return Ok(bookLoan);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<BookLoanDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> List(CancellationToken cancellationToken)
        {
            var bookLoans = await _bookLoanService.ListAsync(cancellationToken);

            return Ok(bookLoans);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BookLoanDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var bookLoan = await _bookLoanService.GetByIdAsync(id, cancellationToken);

            return Ok(bookLoan);
        }
    }
}