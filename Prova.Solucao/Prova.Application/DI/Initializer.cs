using Prova.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Prova.Domain.Interfaces;
using Prova.Infra.Repositories;

namespace Prova.Application.DI
{
    public class Initializer
    {
        public static void Configure(IServiceCollection services, string connection)
        {
            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connection));
            services.AddScoped<IContatoRepository, ContatoRepository>();
        }
    }
}
