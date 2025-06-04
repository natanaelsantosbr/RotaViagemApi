using RotaViagem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaViagem.Infrastructure
{
    public class InMemoryRotaRepository : IRotaRepository
    {
        private readonly List<Rota> _rotas = new();

        public IEnumerable<Rota> GetAll() => _rotas;

        public void Add(Rota rota) => _rotas.Add(rota);

        public void Update(Rota rota)
        {
            var index = _rotas.FindIndex(r => r.Origem == rota.Origem && r.Destino == rota.Destino);
            if (index != -1) _rotas[index] = rota;
        }

        public void Delete(string origem, string destino)
        {
            _rotas.RemoveAll(r => r.Origem == origem && r.Destino == destino);
        }

        public void Upsert(Rota rota)
        {
            var index = _rotas.FindIndex(r => r.Origem == rota.Origem && r.Destino == rota.Destino);
            if (index != -1)
            {
                _rotas[index] = rota;
            }
            else
            {
                _rotas.Add(rota);
            }
        }
    }
}
