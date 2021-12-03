using Prova.Application.DTOs;
using Prova.Application.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.Application.Validadores
{
    public class HelperContato
    {
        public static ErrorMessage ValidarDados(ContatoDTO obj)
        {
            var validacao = new ErrorMessage() { Valido = true };

            if (obj.DataNascimento > DateTime.Now)
                validacao = new ErrorMessage() { Valido = false, Erro = "Data de nascimento é maior que a data atual" };

            if (obj.Idade < 18)
                validacao = new ErrorMessage() { Valido = false, Erro = "O contato tem que ser maior de idade" };

            if (!obj.IsAtivo)
                validacao = new ErrorMessage() { Valido = false, Erro = "O contato está inativo" };

            return validacao;
        }

        public static int CalcularIdade(ContatoDTO obj)
        {
            var dataNascimento = obj.DataNascimento;
            int idade = DateTime.Now.Year - dataNascimento.Year;
            if (DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
            {
                idade = idade - 1;
            }
            return idade;
        }
    }
}
