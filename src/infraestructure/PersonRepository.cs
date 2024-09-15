using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using StoreApi.src.domain;
using StoreApi.src.infraestructure.data;

namespace StoreApi.src.infraestructure
{
    public class PersonRepository(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddPersonAsync(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task<Person?> GetPersonByIdAsync(int id)
        {
            return await _context.Persons
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Person?> GetPersonByUserAsync(User user)
        {
            return await _context.Persons
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.User == user);
        }

        public async Task UpdatePersonAsync(Person person)
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
            }
        }
    }
}