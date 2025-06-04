using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaViagem.Domain
{
    public class Rota
    {
        public string Origem { get; set; } = "";
        public string Destino { get; set; } = "";
        public decimal Valor { get; set; }
    }
}
