using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace TodoApp
{
    public class TodoAppService : ApplicationService, ITodoAppService
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoAppService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        // TODO: Implement the methods here...
        public async Task<List<TodoItemDto>> GetListAsync()
        {
            var items = await _todoItemRepository.GetListAsync();
            return items
                .Select(item => new TodoItemDto
                {
                    Id = item.Id,
                    Text = item.Text
                }).ToList();
        }

        public async Task<TodoItemDto> CreateAsync(TodoItemCreationDto dto)
        {
            var newId = Guid.NewGuid();
            var todo = new TodoItem(newId) { Text = dto.Text };
            foreach (var subItem in dto.SubItems)
                todo.AddSubItem(new TodoSubItem(Guid.NewGuid()) { Text = subItem.Text });

            await _todoItemRepository.CreateAsync(todo);
            await CurrentUnitOfWork.SaveChangesAsync();

            var item = await _todoItemRepository.GetByIdAsync(newId);

            return new TodoItemDto
            {
                Id = item.Id,
                Text = item.Text,
                SubItems = ObjectMapper.Map<List<TodoSubItem>, List<TodoSubItemDto>>(item.SubItems.ToList())
            };
        }

        public async Task<TodoItemDto> UpdateAsync(TodoItemDto dto)
        {
            try
            {
                var todo = ObjectMapper.Map<TodoItemDto, TodoItem>(dto);

                var todoItem = await _todoItemRepository.UpdateAsync(todo);

                return new TodoItemDto
                {
                    Id = todoItem.Id,
                    Text = todoItem.Text
                };
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            await _todoItemRepository.DeleteAsync(id);
        }
        
    }
}