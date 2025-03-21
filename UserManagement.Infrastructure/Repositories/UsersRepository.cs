using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;
using UserManagement.Persistence.DbContexts;

namespace UserManagement.Infrastructure.Repositories;

public class UsersRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UsersRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(User entity)
    {
        _context.Users.Add(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await GetByIdAsync(id);
        if (user is not null)
        {
            _context.Users.Remove(user!);
            return await _context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<bool> UpdateAsync(User entity)
    {
        _context.Users.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}