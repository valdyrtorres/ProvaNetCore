using System;

namespace Prova.Teste.Models
{
    public class ContatoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool IsAtivo { get; set; }
        public string Sexo { get; set; }
        public int Idade { get; set; }

        public string MsgErro { get; set; }
        public bool Valido { get; set; }
    }
}
