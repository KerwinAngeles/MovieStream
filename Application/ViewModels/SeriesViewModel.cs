using DataBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class SeriesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string VideoUrl { get; set; } = null!;
        public string ProducerName { get; set; } = null!;
        public List<string> Genres { get; set; } = new List<string>();

        public List<string> GenresSecondary { get; set; } = new List<string>();
    }
}
