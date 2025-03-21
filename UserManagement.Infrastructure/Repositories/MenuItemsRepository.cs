using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;
using UserManagement.Persistence.DbContexts;

namespace UserManagement.Infrastructure.Repositories;

public class MenuItemsRepository : IMenuItemRepository
{
    private readonly AppDbContext _context;

    public MenuItemsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(MenuItem entity)
    {
        _context.MenuItems.Add(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var menuItem = await GetByIdAsync(id);
        if (menuItem is null) return false;

        _context.MenuItems.Remove(menuItem);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<MenuItem>> GetAllAsync()
    {
        return await _context.MenuItems.AsNoTracking().ToListAsync();
    }

    public async Task<MenuItem?> GetByIdAsync(int id)
    {
        return await _context.MenuItems.FindAsync(id);
    }

    public async Task<bool> UpdateAsync(MenuItem entity)
    {
        _context.MenuItems.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}