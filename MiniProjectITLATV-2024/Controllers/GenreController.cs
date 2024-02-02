using Application.Repositories;
using Application.Services;
using Application.ViewModels;
using DataBase;
using Microsoft.AspNetCore.Mvc;

namespace MiniProjectITLATV_2024.Controllers
{
    public class GenreController : Controller
    {
        public GenreService _genreService;
        public GenreController(ApplicationDbContext dbContext) 
        {
            _genreService = new GenreService(dbContext);
        }
        public async Task<IActionResult> Index()
        {
            return View( await _genreService.GetAllGenre());
        }

        public IActionResult Create()
        {
            return View("CreateEditGenre", new SaveGenreViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveGenreViewModel saveGenre)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEditGenre", saveGenre);
            }

            await _genreService.CreateGenre(saveGenre);
            return RedirectToRoute(new { controller = "Genre", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View("CreateEditGenre", await _genreService.GetGenreById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveGenreViewModel saveGenre)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEditGenre", saveGenre);
            }

            await _genreService.EditGenre(saveGenre);
            return RedirectToRoute(new { controller = "Genre", action = "Index" });
        }

        public async Task<IActionResult> DeleteGenre(int id)
        {
            return View(await _genreService.GetGenreById(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _genreService.DeleteGenre(id);
            return RedirectToRoute(new {controller= "Genre", action = "Index" }); ;
        }
    }
}
