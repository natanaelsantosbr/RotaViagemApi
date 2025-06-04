using RotaViagem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaViagem.Application
{
    public class RotaService
    {
        private readonly IRotaRepository _repository;

        public RotaService(IRotaRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Rota> GetAll() => _repository.GetAll();
        public void Add(Rota rota) => _repository.Add(rota);
        public void Update(Rota rota) => _repository.Update(rota);
        public void Delete(string origem, string destino) => _repository.Delete(origem, destino);

        public (List<string> caminho, decimal custo) BuscarMelhorRota(string origem, string destino)
        {
            origem = origem.ToUpperInvariant();
            destino = destino.ToUpperInvariant();

            var rotas = _repository.GetAll()
                .Select(r => new Rota
                {
                    Origem = r.Origem.ToUpperInvariant(),
                    Destino = r.Destino.ToUpperInvariant(),
                    Valor = r.Valor
                })
                .ToList();

            var grafo = rotas
                .GroupBy(r => r.Origem)
                .ToDictionary(g => g.Key, g => g.ToList());

            var fila = new PriorityQueue<(string atual, List<string> caminho, decimal custo), decimal>();
            fila.Enqueue((origem, new List<string> { origem }, 0), 0);

            var visitados = new Dictionary<string, decimal>();

            while (fila.TryDequeue(out var atual, out _))
            {
                if (visitados.ContainsKey(atual.atual) && atual.custo >= visitados[atual.atual])
                    continue;

                visitados[atual.atual] = atual.custo;

                if (atual.atual == destino)
                    return (atual.caminho, atual.custo);

                if (!grafo.ContainsKey(atual.atual))
                    continue;

                foreach (var vizinho in grafo[atual.atual])
                {
                    var novoCaminho = new List<string>(atual.caminho) { vizinho.Destino };
                    var novoCusto = atual.custo + vizinho.Valor;
                    fila.Enqueue((vizinho.Destino, novoCaminho, novoCusto), novoCusto);
                }
            }

            return (new List<string>(), -1);
        }
    }
}
