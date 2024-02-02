using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class SaveSerieViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "La portada es requerida")]
        public string ImageUrl { get; set; } = null!;

        [Required(ErrorMessage ="El enlace del video es requerido")]
        public string VideoUrl { get; set; } = null!;

        [Required(ErrorMessage = "La productora es requerida")]
        public int ProducerId { get; set; }

        [MinLength(1, ErrorMessage ="El genero primario es requerido")]
        public List<int> GenresPrimary { get; set; } = new List<int>();

        public List<int> GenresSecondary { get; set; } = new List<int>();
    }
}
