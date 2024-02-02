using Application.ViewModels;
using DataBase;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public class ProducerRepository
    {
        private readonly ApplicationDbContext _context;
        public ProducerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Producer>> GetAllProducer()
        {
            return await _context.Set<Producer>().ToListAsync();
        }

        public async Task CreateProducer(Producer producer)
        {
            await _context.producers.AddAsync(producer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProducer(Producer producer)
        {
            _context.Entry(producer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProducer(Producer producer)
        {
            _context.Set<Producer>().Remove(producer);
            await _context.SaveChangesAsync();
        }

        public async Task<Producer> GetProducerById(int id)
        {
            var producerId = await _context.producers.FirstOrDefaultAsync(p => p.Id == id);
            return producerId;
        }
    }
}
