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
            var items = await _todoItemRepository.GetAllAsync();
            return items
                .Select(item => new TodoItemDto
                {
                    Id = item.Id,
                    Text = item.Text,
                    SubItems = ObjectMapper.Map<List<TodoSubItem>, List<TodoSubItemDto>>(item.SubItems.ToList())
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

        public async Task<TodoItemDto> UpdateAsync(TodoItemUpdateDto dto)
        {
            try
            {
                var todoItem = await _todoItemRepository.GetByIdAsync(dto.Id);
                ObjectMapper.Map(dto, todoItem);

                var updatedTodoItem = await _todoItemRepository.UpdateAsync(todoItem);
                await CurrentUnitOfWork.SaveChangesAsync();

                return new TodoItemDto
                {
                    Id = updatedTodoItem.Id,
                    Text = updatedTodoItem.Text,
                    SubItems = ObjectMapper.Map<List<TodoSubItem>, List<TodoSubItemDto>>(updatedTodoItem.SubItems.ToList())
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