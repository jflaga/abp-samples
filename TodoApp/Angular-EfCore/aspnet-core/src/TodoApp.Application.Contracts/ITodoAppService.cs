using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace TodoApp
{
    public interface ITodoAppService : IApplicationService
    {
        Task<List<TodoItemDto>> GetListAsync();
        Task<TodoItemDto> CreateAsync(TodoItemCreationDto dto);
        Task<TodoItemDto> UpdateAsync(TodoItemDto dto);
        Task DeleteAsync(Guid id);
    }
}