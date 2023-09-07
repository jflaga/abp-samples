using System;
using System.Collections.Generic;

namespace TodoApp
{
    public class TodoItemUpdateDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public IEnumerable<TodoSubItemUpdateDto> SubItems { get; set; }
    }

    public class TodoSubItemUpdateDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}