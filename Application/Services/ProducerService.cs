using Application.Repositories;
using Application.ViewModels;
using DataBase;
using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProducerService
    {
        private readonly ProducerRepository _producerRepository;
        public ProducerService(ApplicationDbContext context)
        {
            _producerRepository = new ProducerRepository(context);
        }

        public async Task<List<ProducerViewModel>> GetAllProducer()
        {
            var producer = await _producerRepository.GetAllProducer();
            return producer.Select(p => new ProducerViewModel
            {
                Id = p.Id,
                Name = p.Name,

            }).ToList();
        }
        public async Task CreateProducer(SaveProducerViewModel saveProducer)
        {
            Producer producer = new Producer()
            {
                Id = saveProducer.Id,
                Name = saveProducer.Name,
            };

            await _producerRepository.CreateProducer(producer);
        }

        public async Task EditProducer(SaveProducerViewModel saveProducer)
        {
            Producer producerExist = await _producerRepository.GetProducerById(saveProducer.Id);
            producerExist.Id = saveProducer.Id;
            producerExist.Name = saveProducer.Name;

            await _producerRepository.UpdateProducer(producerExist);
        }

        public async Task DeleteProducer(int id)
        {
            var producer = await _producerRepository.GetProducerById(id);
            await _producerRepository.DeleteProducer(producer);
        }

        public async Task<SaveProducerViewModel> GetProducerById(int id)
        {
            var producer = await _producerRepository.GetProducerById(id);
            SaveProducerViewModel saveProducer = new SaveProducerViewModel();
            saveProducer.Id = id;
            saveProducer.Name = producer.Name;

            return saveProducer;
        }
    }
}
