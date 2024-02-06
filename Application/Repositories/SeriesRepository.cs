using DataBase;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public class SeriesRepository
    {
        private readonly ApplicationDbContext _context;
        public SeriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddSeries(Serie serie)
        {
            await _context.series.AddAsync(serie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSeries(Serie serie)
        {
            _context.series.Entry(serie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSeries(Serie serie)
        {
            _context.Set<Serie>().Remove(serie);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Serie>> GetAllSeries()
        {
            return await _context.Set<Serie>()
                .Include(x => x.Producer)
                .Include(x => x.SeriesGenresList)
                .ThenInclude(genre => genre.Genre)
                .ToListAsync();
        }

        public async Task<Serie> GetSerieByName(string name)
        {
            var serie = await _context.Set<Serie>()
                .Include(x => x.Producer)
                .Include(x => x.SeriesGenresList).ThenInclude(genre => genre.Genre)
                .ToListAsync();
                //.FirstOrDefaultAsync(s => s.Name == name);
            return serie.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        }

       public async Task<Serie> GetSerieById(int id)
       {
            var serieId = await _context.series.Include(x => x.SeriesGenresList).FirstOrDefaultAsync(i => i.Id == id);
            return serieId;
       }

        public async Task<List<Serie>> GetAllSeriesByProducer(Producer producer)
        {
            return await _context.series
                .Include(x => x.SeriesGenresList).ThenInclude(genre => genre.Genre)
                .Where(s => s.ProducerId == producer.Id).ToListAsync();  
        }

        public async Task<List<SeriesGenre>> GetAllSeriesByGenre(Genre genre)
        {
            return await _context.seriesGenres
                .Include(x => x.Serie)
                .Include(x => x.Serie.Producer)
                .Where(s => s.GenreId == genre.Id).ToListAsync();
        }
    }
}
