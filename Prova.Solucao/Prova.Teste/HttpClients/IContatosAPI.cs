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

        [Get("/api/Contatos/ativos")]
        Task<ApiResponse<IList<ContatoDTO>>> Get();

        [Post("/api/Contatos")]
        Task<ApiResponse<ContatoDTO>> Post([Body] ContatoDTO contatoDTO);

        [Patch("/api/Contatos/{id}")]
        Task<ApiResponse<ContatoDTO>> Patch(int id);

        [Delete("/api/Contatos/{id}")]
        Task<ApiResponse<ContatoDTO>> Delete(int id);
    }
}
