using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class SeriesGenre
    {
        public int SerieId { get; set; }
        public int GenreId { get; set; }
        public Serie Serie { get; set; } = null!;
        public Genre Genre { get; set; } = null!;

    }
}
