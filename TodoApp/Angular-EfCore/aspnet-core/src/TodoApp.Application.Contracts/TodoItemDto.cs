using System;
using System.Collections.Generic;

namespace TodoApp
{
    public class TodoItemDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public IEnumerable<TodoSubItemDto> SubItems { get; set; }
    }
}