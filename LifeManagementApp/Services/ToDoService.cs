using LifeManagementApp.Data;
using LifeManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LifeManagementApp.Services;

public class TodoService
{
    private readonly AppDbContext _db;
    public TodoService(AppDbContext db) => _db = db;

    public Task<List<TodoItem>> GetAllAsync() => _db.Todos.ToListAsync();

    public Task<List<TodoItem>> GetTopAsync(int count) =>
        _db.Todos.OrderBy(t => t.Priority).ThenBy(t => t.DueDate).Take(count).ToListAsync();

    public async Task AddAsync(TodoItem item)
    {
        _db.Todos.Add(item);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(TodoItem item)
    {
        _db.Todos.Remove(item);
        await _db.SaveChangesAsync();
    }
}