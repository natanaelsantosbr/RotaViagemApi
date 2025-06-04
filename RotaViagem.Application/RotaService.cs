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
            var grafo = _repository.GetAll()
                .GroupBy(r => r.Origem)
                .ToDictionary(g => g.Key, g => g.ToList());

            var fila = new PriorityQueue<(string atual, List<string> caminho, decimal custo), decimal>();
            fila.Enqueue((origem, new List<string> { origem }, 0), 0);

            var visitados = new HashSet<string>();

            while (fila.TryDequeue(out var atual, out _))
            {
                if (atual.atual == destino)
                    return (atual.caminho, atual.custo);

                if (visitados.Contains(atual.atual)) continue;
                visitados.Add(atual.atual);

                if (!grafo.ContainsKey(atual.atual)) continue;

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
