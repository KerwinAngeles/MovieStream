using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Serie
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string VideoUrl { get; set; } = null!;
        public int ProducerId { get; set; }
        public Producer Producer { get; set; } = null!;
        public ICollection<SeriesGenre> SeriesGenresList { get; set; } = null!;
    }
}
