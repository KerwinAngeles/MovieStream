using Application.Services;
using Application.ViewModels;
using DataBase;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace MiniProjectITLATV_2024.Controllers
{
    public class SeriesController : Controller
    {
        public SeriesService _serieService { get; set; }
        public ProducerService _producerService { get; set; }
        public GenreService _genreService { get; set; }
        public SeriesController(ApplicationDbContext dbContext) 
        {
            _serieService = new SeriesService(dbContext);
            _producerService = new ProducerService(dbContext);
            _genreService = new GenreService(dbContext);
        }
        public async Task<IActionResult> Index()
        {
           
            return View(await _serieService.GetAllSeries());
        }

        public async Task<IActionResult> Create()
        {
            List<ProducerViewModel> producersList =  await _producerService.GetAllProducer();
            List<GenreViewModel> genreList = await _genreService.GetAllGenre();

            ViewBag.producers = producersList;
            ViewBag.genres = genreList;
            return  View("SaveSerie", new SaveSerieViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveSerieViewModel saveSerie)
        {
            List<ProducerViewModel> producersList = await _producerService.GetAllProducer();
            List<GenreViewModel> genreList = await _genreService.GetAllGenre();

            ViewBag.producers = producersList;
            ViewBag.genres = genreList;

            if (!ModelState.IsValid)
            {
                return View("SaveSerie", saveSerie);
            }

            await _serieService.CreateSerie(saveSerie);
            return RedirectToRoute(new { controller = "Series", action = "Index" });
        }


        public async Task<IActionResult> Edit(int id)
        {
            List<ProducerViewModel> producersList = await _producerService.GetAllProducer();
            List<GenreViewModel> genreList = await _genreService.GetAllGenre();

            ViewBag.producers = producersList;
            ViewBag.genres = genreList;
            return View("SaveSerie", await _serieService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveSerieViewModel sv)
        {
            List<ProducerViewModel> producersList = await _producerService.GetAllProducer();
            List<GenreViewModel> genreList = await _genreService.GetAllGenre();

            ViewBag.producers = producersList;
            ViewBag.genres = genreList;

            if (!ModelState.IsValid)
            {
                return View("SaveSerie", sv);
            }


            await _serieService.EditSerie(sv);
            return RedirectToRoute(new { controller = "Series", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _serieService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult>DeletePost(int id)
        {
            await _serieService.DeleteSerie(id);
            return RedirectToRoute(new { controller = "Series", action = "Index" });
        }
    }
}
