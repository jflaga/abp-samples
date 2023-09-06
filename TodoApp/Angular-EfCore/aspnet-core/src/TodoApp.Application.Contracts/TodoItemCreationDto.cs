using System;
using System.Collections.Generic;

namespace TodoApp
{
    public class TodoItemCreationDto
    {
        public string Text { get; set; }
        public IEnumerable<TodoSubItemCreationDto> SubItems { get; set; }
    }

    public class TodoSubItemCreationDto
    {
        public string Text { get; set; }
    }
}