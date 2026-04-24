using Biblioteca.Domain.Enums;
using Biblioteca.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Entities
{ 
    public class BookLoan
    {
        public Guid Id { get; private set; }
        public Guid LivroId { get; private set; }
        public DateTime DataEmprestimo { get; private set; }
        public DateTime? DataDevolucao { get; private set; }
        public StatusBookEnum Status { get; private set; }

        private BookLoan() { }

        public BookLoan(Guid livroId)
        {
            if (livroId == Guid.Empty)
                throw new DomainException("Id do Livro é obrigatório.");

            Id = Guid.NewGuid();
            LivroId = livroId;
            DataEmprestimo = DateTime.UtcNow;
            Status = StatusBookEnum.Ativo;
        }

        public void Deliver()
        {
            if (Status == StatusBookEnum.Devolvido)
                throw new DomainException("Não é possível devolver um empréstimo já devolvido.");

            Status = StatusBookEnum.Devolvido;
            DataDevolucao = DateTime.UtcNow;
        }
    }
}
