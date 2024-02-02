using Application.Repositories;
using Application.ViewModels;
using DataBase;
using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GenreService
    {
        public readonly GenreRepository _genreRepository;
        public GenreService(ApplicationDbContext context)
        {
            _genreRepository = new GenreRepository(context);
        }

        public async Task<List<GenreViewModel>> GetAllGenre()
        {
            var genre = await _genreRepository.GetAllGenre();
            return genre.Select(g => new GenreViewModel
            {
                Id = g.Id,
                Name = g.Name

            }).ToList();
        }

        public async Task CreateGenre(SaveGenreViewModel saveGenre)
        {

            Genre genre = new Genre
            {
                Name = saveGenre.Name,
            };

            await _genreRepository.CreateGenre(genre);
        }

        public async Task EditGenre(SaveGenreViewModel saveGenre)
        {
            Genre genreExist = await _genreRepository.GetGenreById(saveGenre.Id);
            genreExist.Id = saveGenre.Id;
            genreExist.Name = saveGenre.Name;

            await _genreRepository.UpdateGenre(genreExist);
        }
        public async Task DeleteGenre(int id)
        {
            var serieExist = await _genreRepository.GetGenreById(id);
            await _genreRepository.DeleteGenre(serieExist);
        }

        public async Task<SaveGenreViewModel> GetGenreById(int id)
        {
            var genre = await _genreRepository.GetGenreById(id);

            SaveGenreViewModel saveGenre = new ()
            {
                Id = genre.Id,
                Name = genre.Name,
            };

            return saveGenre;
        }

        public async Task<SaveGenreViewModel> GetByName(string name)
        {
            var serie = await _genreRepository.GetGenreByName(name);

            SaveGenreViewModel saveGenreViewModel = new()
            {
                Id = serie.Id,
                Name = serie.Name,
            };
            return saveGenreViewModel;
        }
    }
}
