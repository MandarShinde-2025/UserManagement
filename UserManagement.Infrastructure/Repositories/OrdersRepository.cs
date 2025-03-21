using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;
using UserManagement.Persistence.DbContexts;

namespace UserManagement.Infrastructure.Repositories;

public class OrdersRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrdersRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Order entity)
    {
        _context.Orders.Add(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await GetByIdAsync(id);
        if (order is null) return false;

        _context.Orders.Remove(order);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders.Include((o) => o.OrderItems).AsNoTracking().ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders
            .Include((o) => o.OrderItems)
            .FirstOrDefaultAsync((o) => o.Id == id);
    }

    public async Task<bool> UpdateAsync(Order entity)
    {
        _context.Orders.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}