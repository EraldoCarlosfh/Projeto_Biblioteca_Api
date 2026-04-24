using Biblioteca.Api;
using Biblioteca.Application.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Biblioteca.IntegrationTests
{
    public class CompleteFluxTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CompleteFluxTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task FluxoCompleto_Deve_CriarLivro_Emprestar_E_Devolver()
        {
            var createBookRequest = new CreateBookRequest
            {
                Titulo = "Clean Code",
                Autor = "Robert C. Martin",
                AnoPublicacao = 2008,
                QuantidadeDisponivel = 2
            };

            var createBookResponse = await _client.PostAsJsonAsync(
                "/api/livros",
                createBookRequest);

            createBookResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var bookCreated = await createBookResponse.Content.ReadFromJsonAsync<BookDto>();

            bookCreated.Should().NotBeNull();
            bookCreated!.Id.Should().NotBeEmpty();
            bookCreated.QuantidadeDisponivel.Should().Be(2);

            var bookLoanRequest = new BookLoanRequest
            {
                LivroId = bookCreated.Id
            };

            var bookLoanResponse = await _client.PostAsJsonAsync(
                "/api/emprestimos",
                bookLoanRequest);

            bookLoanResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var bookLoanCreated = await bookLoanResponse.Content.ReadFromJsonAsync<BookLoanDto>();

            bookLoanCreated.Should().NotBeNull();
            bookLoanCreated!.Id.Should().NotBeEmpty();
            bookLoanCreated.LivroId.Should().Be(bookCreated.Id);
            bookLoanCreated.Status.Should().Be("Ativo");

            var BookAfterLoanResponse = await _client.GetAsync(
                $"/api/livros/{bookCreated.Id}");

            BookAfterLoanResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var BookAfterLoan = await BookAfterLoanResponse.Content
                .ReadFromJsonAsync<BookDto>();

            BookAfterLoan.Should().NotBeNull();
            BookAfterLoan!.QuantidadeDisponivel.Should().Be(1);

            var returnResponse = await _client.PatchAsync(
                $"/api/emprestimos/{bookLoanCreated.Id}/devolucao",
                null);

            returnResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var bookLoanReturn = await returnResponse.Content
                .ReadFromJsonAsync<BookLoanDto>();

            bookLoanReturn.Should().NotBeNull();
            bookLoanReturn!.Status.Should().Be("Devolvido");
            bookLoanReturn.DataDevolucao.Should().NotBeNull();

            var bookAfterReturnResponse = await _client.GetAsync(
                $"/api/livros/{bookCreated.Id}");

            bookAfterReturnResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var bookAfterReturn = await bookAfterReturnResponse.Content
                .ReadFromJsonAsync<BookDto>();

            bookAfterReturn.Should().NotBeNull();
            bookAfterReturn!.QuantidadeDisponivel.Should().Be(2);
        }
    }
}
