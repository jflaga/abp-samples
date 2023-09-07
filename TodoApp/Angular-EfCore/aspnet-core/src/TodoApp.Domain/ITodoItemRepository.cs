using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace TodoApp
{
    public interface ITodoItemRepository
    {
        Task<List<TodoItem>> GetAllAsync();
        Task<TodoItem> GetByIdAsync(Guid id);
        Task<TodoItem> CreateAsync(TodoItem item);
        Task<TodoItem> UpdateAsync(TodoItem item);
        Task DeleteAsync(Guid id);
    }
}
