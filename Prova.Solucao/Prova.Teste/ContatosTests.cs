using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xunit;
using FluentAssertions;
using Refit;
using Prova.Teste.HttpClients;
using Prova.Teste.Models;

namespace Prova.Teste
{
    public class ContatosTests
    {
        private readonly IContatosAPI _apiContatos;

        public ContatosTests()
        {
            _apiContatos = RestService.For<IContatosAPI>("https://localhost:5001");
        }

        [Theory]
        [InlineData(1)]
        public async Task TestarGetContatoPorId(int idContato)
        {
            var response = await _apiContatos.Get(idContato);
            response.StatusCode.Should().Be(HttpStatusCode.OK,
                $"* Ocorreu uma falha: Status Code esperado (200, OK) diferente do resultado gerado *");

        }

        [Fact]
        public async Task TestarGetContatos()
        {
            var response = await _apiContatos.Get();
            response.StatusCode.Should().Be(HttpStatusCode.OK,
                $"* Ocorreu uma falha: Status Code esperado (200, OK) diferente do resultado gerado *");

        }

        [Theory]
        [InlineData(9)]
        public async Task TestarDeleteContato(int idContato)
        {
            var response = await _apiContatos.Delete(idContato);
            response.StatusCode.Should().Be(HttpStatusCode.NoContent,
                $"* Ocorreu uma falha: Status Code esperado (204, NoContent) diferente do resultado gerado *");

        }

        [Theory]
        [InlineData("Luffy D Monkey", "1970-06-05 21:11", true, "Masculino")]
        public async Task TestarCreateContato(string Nome,
            string iDtString,
            bool flagAtivo,
            string sexo)
        {
            ContatoDTO corpo = new ContatoDTO();

            corpo.Nome = Nome;
            string[] dataNasc = iDtString.Split(' ');
            string[] dataSomente = dataNasc[0].Split('-');
            string[] horaSomente = dataNasc[1].Split(':');
            var oDate = new DateTime(int.Parse(dataSomente[0]), int.Parse(dataSomente[1]), int.Parse(dataSomente[2]),
                int.Parse(horaSomente[0]), int.Parse(horaSomente[0]), int.Parse(horaSomente[0]));
            corpo.DataNascimento = oDate;
            corpo.IsAtivo = flagAtivo;
            corpo.Sexo = sexo;

            var response = await _apiContatos.Post(corpo);
            response.StatusCode.Should().Be(HttpStatusCode.OK,
                $"* Ocorreu uma falha: Status Code esperado (200, OK) diferente do resultado gerado *");

        }

        [Theory]
        [InlineData(3, "Valdir Bigode", "1969-06-11 19:12:45", true, "Masculino")]
        public async Task TestarUpdateContato(
            int idContato,
            string Nome,
            string iDtString,
            bool flagAtivo,
            string sexo)
        {
            ContatoDTO corpo = new ContatoDTO();

            corpo.Id = idContato;
            corpo.Nome = Nome;
            string[] dataNasc = iDtString.Split(' ');
            string[] dataSomente = dataNasc[0].Split('-');
            string[] horaSomente = dataNasc[1].Split(':');
            var oDate = new DateTime(int.Parse(dataSomente[0]), int.Parse(dataSomente[1]), int.Parse(dataSomente[2]), 
                int.Parse(horaSomente[0]), int.Parse(horaSomente[0]), int.Parse(horaSomente[0]));
            corpo.DataNascimento = oDate;
            corpo.IsAtivo = flagAtivo;
            corpo.Sexo = sexo;

            var response = await _apiContatos.Put(idContato, corpo);
            response.StatusCode.Should().Be(HttpStatusCode.OK,
                $"* Ocorreu uma falha: Status Code esperado (200, OK) diferente do resultado gerado *");

        }

        [Theory]
        [InlineData("Luke Lucky", true, "Masculino")]
        public async Task TestarCreateContatoDiaNascimentoComoDataDeHoje(
            string Nome,
            bool flagAtivo,
            string sexo)
        {
            ContatoDTO corpo = new ContatoDTO();

            corpo.Nome = Nome;
            corpo.DataNascimento = DateTime.Now;
            corpo.IsAtivo = flagAtivo;
            corpo.Sexo = sexo;

            var response = await _apiContatos.Post(corpo);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest,
                $"* Ocorreu uma falha: Status Code esperado (400, OK) diferente do resultado gerado *");
            // Faz o Teste quebrar
            //response.StatusCode.Should().Be(HttpStatusCode.OK,
            //    $"* Ocorreu uma falha: Status Code esperado (200, OK) diferente do resultado gerado *");

        }

        [Theory]
        [InlineData("Ana Neri", "2010-06-05 21:11", true, "Feminino")]
        public async Task TestarCreateContatoIdadeMenorQue18(string Nome,
            string iDtString,
            bool flagAtivo,
            string sexo)
        {
            ContatoDTO corpo = new ContatoDTO();

            corpo.Nome = Nome;
            string[] dataNasc = iDtString.Split(' ');
            string[] dataSomente = dataNasc[0].Split('-');
            string[] horaSomente = dataNasc[1].Split(':');
            var oDate = new DateTime(int.Parse(dataSomente[0]), int.Parse(dataSomente[1]), int.Parse(dataSomente[2]),
                int.Parse(horaSomente[0]), int.Parse(horaSomente[0]), int.Parse(horaSomente[0]));
            corpo.DataNascimento = oDate;
            corpo.IsAtivo = flagAtivo;
            corpo.Sexo = sexo;

            var response = await _apiContatos.Post(corpo);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest,
                $"* Ocorreu uma falha: Status Code esperado (400, OK) diferente do resultado gerado *");
            // Faz o teste quebrar
            //response.StatusCode.Should().Be(HttpStatusCode.OK,
            //    $"* Ocorreu uma falha: Status Code esperado (200, OK) diferente do resultado gerado *");

        }
    }
}
