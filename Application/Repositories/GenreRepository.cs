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
    public class GenreRepository
    {
        private readonly ApplicationDbContext _context;
        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }           

        public async Task<List<Genre>> GetAllGenre()
        {
            return await _context.Set<Genre>().ToListAsync();
        }

        public async Task CreateGenre(Genre genre)
        {
            await _context.genres.AddAsync(genre);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGenre(Genre genre)
        {
            _context.Entry(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteGenre(Genre genre)
        {
            var genreSeries = _context.seriesGenres
            .Where(sg => sg.GenreId == genre.Id)
            .Select(sg => sg.Serie)
            .ToList();

            _context.series.RemoveRange(genreSeries);
            _context.Set<Genre>().Remove(genre);
            
            await _context.SaveChangesAsync();
        }

        public async Task<Genre> GetGenreById(int id)
        {
           var serieId = await _context.genres.FirstOrDefaultAsync(i => i.Id == id);
           return serieId;
        }
        public async Task<Genre>GetGenreByName(string name)
        {
            var genreName = await _context.genres.FirstOrDefaultAsync(n => n.Name == name);
            return genreName;
        }
    }
}
