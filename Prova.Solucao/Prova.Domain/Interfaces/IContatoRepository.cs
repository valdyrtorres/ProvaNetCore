using Prova.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.Domain.Interfaces
{
    public interface IContatoRepository : IRepositoryBase<Contato>
    {
        IEnumerable<Contato> GetContatos();
        IEnumerable<Contato> GetContatosAtivos();
        Contato GetContato(int id);
        void CreateContato(Contato contato);
        void UpdateContato(Contato contato);
        void DeleteContato(Contato contato);
    }
}
