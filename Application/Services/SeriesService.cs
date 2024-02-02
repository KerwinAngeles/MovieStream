using Application.Repositories;
using Application.ViewModels;
using DataBase;
using DataBase.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SeriesService
    {
        private readonly SeriesRepository _seriesRepository;
        public SeriesService(ApplicationDbContext dbContext)
        {
            _seriesRepository = new SeriesRepository(dbContext);
        }

        public async Task<List<SeriesViewModel>> GetAllSeries()
        {
            var serie = await _seriesRepository.GetAllSeries();

            return serie.Select(series => new SeriesViewModel
            {
                Id = series.Id,
                Name = series.Name,
                ImageUrl = series.ImageUrl,
                VideoUrl = series.VideoUrl,
                ProducerName = series.Producer.Name,
                Genres = series.SeriesGenresList.Select(s => s.Genre.Name).ToList(),
            }).ToList();

        }

        public async Task CreateSerie(SaveSerieViewModel saveSerie)
        {
            List<SeriesGenre> genresPrimary = saveSerie.GenresPrimary
           .Select(genreId => new SeriesGenre { GenreId = genreId })
           .ToList();

            List<SeriesGenre> genresSecondary = saveSerie.GenresSecondary
          .Select(genreId => new SeriesGenre { GenreId = genreId })
          .ToList();

            Serie serie = new Serie()
            {
                Name = saveSerie.Name,
                ImageUrl = saveSerie.ImageUrl,
                VideoUrl = saveSerie.VideoUrl,
                ProducerId = saveSerie.ProducerId,
                SeriesGenresList = genresPrimary.Concat(genresSecondary).ToList()
            };

            await _seriesRepository.AddSeries(serie);
        }

        public async Task EditSerie(SaveSerieViewModel saveSerie)
        {
            Serie existSerie = await _seriesRepository.GetSerieById(saveSerie.Id);
            existSerie.Name = saveSerie.Name;
            existSerie.ImageUrl = saveSerie.ImageUrl;
            existSerie.VideoUrl = saveSerie.VideoUrl;
            existSerie.ProducerId = saveSerie.ProducerId;
            existSerie.SeriesGenresList = saveSerie.GenresPrimary
            .Select(genreId => new SeriesGenre { GenreId = genreId, SerieId = saveSerie.Id })
            .ToList();

            await _seriesRepository.UpdateSeries(existSerie);
        }

        public async Task DeleteSerie(int id)
        {
            var serie = await _seriesRepository.GetSerieById(id);
            await _seriesRepository.DeleteSeries(serie);
        }

        public async Task<SaveSerieViewModel> GetById(int id)
        {
            var serie = await _seriesRepository.GetSerieById(id);

            SaveSerieViewModel saveSerieViewModel = new()
            {
                Id = serie.Id,
                Name = serie.Name,
                ImageUrl = serie.ImageUrl,
                VideoUrl = serie.VideoUrl,
                ProducerId = serie.ProducerId,
                GenresPrimary = serie.SeriesGenresList.Select(s => s.GenreId).ToList()

            };
            return saveSerieViewModel;
        }

        public async Task<SeriesViewModel> GetByName(string name)
        {
            var serie = await _seriesRepository.GetSerieByName(name);

            SeriesViewModel saveSerieViewModel = new()
            {
                Id = serie.Id,
                Name = serie.Name,
                ImageUrl = serie.ImageUrl,
                VideoUrl = serie.VideoUrl,
                ProducerName= serie.Producer.Name,
                Genres = serie.SeriesGenresList.Select(g => g.Genre.Name).ToList()

            };
            return saveSerieViewModel;
        }

        public async Task<List<SeriesViewModel>> GetByProducer(ProducerViewModel producerViewModel)
        {
            Producer producer = new Producer();
            producer.Id = producerViewModel.Id;
            producer.Name = producerViewModel.Name;

            var serie = await _seriesRepository.GetAllSeriesByProducer(producer);
            if (serie == null)
            {
                return new List<SeriesViewModel>();
            }

            return serie.Select(series => new SeriesViewModel
            {
                Id = series.Id,
                Name = series.Name,
                ImageUrl = series.ImageUrl,
                ProducerName = series.Producer.Name,
                Genres = series.SeriesGenresList.Select(s => s.Genre.Name).ToList()
            }).ToList();
        }
        public async Task<List<SeriesViewModel>> GetByGenre(GenreViewModel genreViewModel)
        {
            Genre genre = new Genre();
            genre.Id = genreViewModel.Id;
            genre.Name = genreViewModel.Name;

            var serie = await _seriesRepository.GetAllSeriesByGenre(genre);
            if (serie == null)
            {
                return new List<SeriesViewModel>();
            }

            return serie.Select(series => new SeriesViewModel
            {
                Id = series.Serie.Id,
                Name = series.Serie.Name,
                ImageUrl = series.Serie.ImageUrl,
                ProducerName = series.Serie.Producer.Name,
                Genres = series.Serie.SeriesGenresList.Select(s => s.Genre.Name).ToList(),
            }).ToList();
        }
    }
}
