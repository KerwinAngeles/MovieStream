using Application.Services;
using Application.ViewModels;
using DataBase;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace MiniProjectITLATV_2024.Controllers
{
    public class HomeController : Controller
    {
        public SeriesService _seriesService { get; set; }
        public GenreService _genreService { get; set; }
        public ProducerService _producerService { get; set; }


        public HomeController(ApplicationDbContext dbContext)
        {
            _seriesService = new SeriesService(dbContext);
            _producerService = new ProducerService(dbContext);

            _genreService = new GenreService(dbContext);
        }
        public async Task<IActionResult> Index()
        {
            List<ProducerViewModel> producersList = await _producerService.GetAllProducer();
            ViewBag.producers = producersList;

            List<GenreViewModel> genreList = await _genreService.GetAllGenre();
            ViewBag.genres = genreList;
            return View(await _seriesService.GetAllSeries());
        }

        public async  Task<IActionResult> Reproductor(int id)
        {
            return View("Reproductor", await _seriesService.GetById(id));
        }

        public async Task<IActionResult> BuscarName(string name)
        {
            List<ProducerViewModel> producersList = await _producerService.GetAllProducer();
            ViewBag.producers = producersList;

            List<GenreViewModel> genreList = await _genreService.GetAllGenre();
            ViewBag.genres = genreList;

            var serieViewModel = await _seriesService.GetByName(name);
            List<SeriesViewModel> seriesViewModels = new List<SeriesViewModel> { serieViewModel };
          
            return View("Index", seriesViewModels);
        }

        public async Task<IActionResult> BuscarProducer(ProducerViewModel producer)
        {
            List<ProducerViewModel> producersList = await _producerService.GetAllProducer();
            ViewBag.producers = producersList;

            List<GenreViewModel> genreList = await _genreService.GetAllGenre();
            ViewBag.genres = genreList;

            var serieViewModel = await _seriesService.GetByProducer(producer);
          
            return View("Index", serieViewModel);
        }
        public async Task<IActionResult> BuscarGenre(GenreViewModel genreViewModel)
        {
            List<GenreViewModel> genreList = await _genreService.GetAllGenre();
            ViewBag.genres = genreList;

            List<ProducerViewModel> producersList = await _producerService.GetAllProducer();
            ViewBag.producers = producersList;

            var serieViewModel = await _seriesService.GetByGenre(genreViewModel);

            return View("Index", serieViewModel);
        }
    }
}

