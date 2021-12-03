using Prova.Application.DTOs;
using Prova.Domain.Models;

namespace Prova.Application.Extensions
{
    public static class Extensions
    {
        public static ContatoDTO AsDto(this Contato contato)
        {
            return new ContatoDTO
            {
                Id = contato.Id,
                Nome = contato.Nome,
                DataNascimento = contato.DataNascimento,
                IsAtivo = contato.IsAtivo,
                Sexo = contato.Sexo,
                Idade = contato.Idade
            };
        }

        public static Contato AsContato(this ContatoDTO contatoDTO)
        {
            return new Contato
            {
                //Id = contatoDTO.Id,
                Nome = contatoDTO.Nome,
                DataNascimento = contatoDTO.DataNascimento,
                IsAtivo = contatoDTO.IsAtivo,
                Sexo = contatoDTO.Sexo,
                Idade = contatoDTO.Idade
            };
        }

        public static Contato AsContatoUpdate(this ContatoDTO contatoDTO)
        {
            return new Contato
            {
                Id = contatoDTO.Id,
                Nome = contatoDTO.Nome,
                DataNascimento = contatoDTO.DataNascimento,
                IsAtivo = contatoDTO.IsAtivo,
                Sexo = contatoDTO.Sexo,
                Idade = contatoDTO.Idade
            };
        }
    }
}
