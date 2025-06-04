using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaViagem.Domain
{
    public interface IRotaRepository
    {
        IEnumerable<Rota> GetAll();
        void Add(Rota rota);
        void Update(Rota rota);
        void Delete(string origem, string destino);
        void Upsert(Rota rota);
    }
}
