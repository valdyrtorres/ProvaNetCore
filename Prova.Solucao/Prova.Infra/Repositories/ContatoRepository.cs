using Prova.Domain.Interfaces;
using Prova.Domain.Models;
using Prova.Infra.Context;
using System.Collections.Generic;
using System.Linq;

namespace Prova.Infra.Repositories
{
    public class ContatoRepository : RepositoryBase<Contato>, IContatoRepository
    {
        public ContatoRepository(AppDbContext context) : base(context)
        {

        }

        public void CreateContato(Contato contato)
        {
            Create(contato);
        }

        public void DeleteContato(Contato contato)
        {
            Delete(contato);
        }
        public void UpdateContato(Contato contato)
        {
            Update(contato);
        }

        public Contato GetContato(int id)
        {
            return FindByCondition(e=> e.Id==id).FirstOrDefault();
        }

        public IQueryable<Contato> GetAll()
        {
            return FindAll();
        }

        public IEnumerable<Contato> GetContatos()
        {
            var query = _context.Set<Contato>();

            return query.Any() ? query.ToList() : new List<Contato>();
        }

        public IEnumerable<Contato> GetContatosAtivos()
        {
            return FindByCondition(e => e.IsAtivo == true);
        }
    }
}
