using System.Collections.Generic;
using System.Threading.Tasks;
using Prova.Teste.Models;
using Refit;

namespace Prova.Teste.HttpClients
{
    public interface IContatosAPI
    {
        [Get("/api/Contatos/{id}")]
        Task<ApiResponse<ContatoDTO>> Get(int id);

        [Get("/api/Contatos")]
        Task<ApiResponse<IList<ContatoDTO>>> Get();

        [Post("/api/Contatos")]
        Task<ApiResponse<ContatoDTO>> Post([Body] ContatoDTO contatoDTO);

        [Put("/api/Contatos/{id}")]
        Task<ApiResponse<ContatoDTO>> Put(int id, [Body] ContatoDTO contatoDTO);

        [Delete("/api/Contatos/{id}")]
        Task<ApiResponse<ContatoDTO>> Delete(int id);
    }
}
