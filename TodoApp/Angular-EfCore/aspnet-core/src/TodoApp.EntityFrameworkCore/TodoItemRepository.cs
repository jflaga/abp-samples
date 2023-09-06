using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TodoApp
{
    public class TodoItemRepository : EfCoreRepository<TodoAppDbContext, TodoItem, Guid>, ITodoItemRepository
    {
        public TodoItemRepository(IDbContextProvider<TodoAppDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task<TodoItem> CreateAsync(TodoItem item)
        {
            return await InsertAsync(item);
        }

        public async Task DeleteAsync(Guid id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            return (await GetQueryableAsync()).Include(x => x.SubItems).ToList();
        }

        public async Task<TodoItem> GetByIdAsync(Guid id)
        {
            return (await GetQueryableAsync()).Include(x => x.SubItems).Single(x => x.Id == id);
        }

        public async Task<TodoItem> UpdateAsync(TodoItem item)
        {
            return await UpdateAsync(item, autoSave: true);
        }
    }
}
