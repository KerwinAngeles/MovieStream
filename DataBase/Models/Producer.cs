using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Serie> SerieProducerList { get; set; } = null!;
    }
}
