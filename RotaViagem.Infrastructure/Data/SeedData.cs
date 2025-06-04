using RotaViagem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaViagem.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Popular(IRotaRepository repo)
        {
            var rotasIniciais = new List<Rota>
        {
            new() { Origem = "GRU", Destino = "BRC", Valor = 10 },
            new() { Origem = "BRC", Destino = "SCL", Valor = 5 },
            new() { Origem = "GRU", Destino = "CDG", Valor = 75 },
            new() { Origem = "GRU", Destino = "SCL", Valor = 20 },
            new() { Origem = "GRU", Destino = "ORL", Valor = 56 },
            new() { Origem = "ORL", Destino = "CDG", Valor = 5 },
            new() { Origem = "SCL", Destino = "ORL", Valor = 20 }
        };

            var rotasExistentes = repo.GetAll()
                .Select(r => (r.Origem, r.Destino))
                .ToHashSet();

            foreach (var rota in rotasIniciais)
            {
                repo.Upsert(rota);
            }
        }
    }
}
