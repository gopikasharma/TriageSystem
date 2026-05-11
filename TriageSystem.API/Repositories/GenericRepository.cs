using System;
using Microsoft.EntityFrameworkCore;
using TriageSystem.API.Data;
using TriageSystem.API.Entities;
using TriageSystem.API.Interfaces;

namespace TriageSystem.API.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext _context;
    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<T?> GetByIdAsync(Guid id)
    {
        // Automatically ensures we never return soft-deleted records
        return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
    }
    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(T entity)
    {
        
        entity.IsDeleted = true;
        entity.DeletedAt = DateTimeOffset.UtcNow;
        await UpdateAsync(entity);
    }

}
