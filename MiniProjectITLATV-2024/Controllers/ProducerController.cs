using Application.Services;
using Application.ViewModels;
using DataBase;
using Microsoft.AspNetCore.Mvc;

namespace MiniProjectITLATV_2024.Controllers
{
    public class ProducerController : Controller
    {
        public ProducerService _producerService;
        public ProducerController(ApplicationDbContext dbContext)
        {
            _producerService = new ProducerService(dbContext);
        }
        public async Task<IActionResult> Index()
        {
            return View(await _producerService.GetAllProducer());
        }

        public IActionResult Create()
        {
            return View("CreateEditProducer", new SaveProducerViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveProducerViewModel saveProducer)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEditProducer", saveProducer);
            }

            await _producerService.CreateProducer(saveProducer);
            return RedirectToRoute(new { controller = "Producer", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View("CreateEditProducer", await _producerService.GetProducerById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveProducerViewModel saveProducer)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEditProducer", saveProducer);
            }

            await _producerService.EditProducer(saveProducer);
            return RedirectToRoute(new { controller = "Producer", action = "Index" });
        }

        public async Task<IActionResult> DeleteProducer(int id)
        {
            return View(await _producerService.GetProducerById(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _producerService.DeleteProducer(id);
            return RedirectToRoute(new { controller = "Producer", action = "Index" }); ;
        }
    }
}
