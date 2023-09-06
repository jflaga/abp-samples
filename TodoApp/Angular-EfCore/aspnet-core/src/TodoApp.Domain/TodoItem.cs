using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace TodoApp
{
    public class TodoItem : BasicAggregateRoot<Guid>
    {
        protected TodoItem() { }
        public TodoItem(Guid Id)
            : base(Id) { }

        public string Text { get; set; }

        private readonly List<TodoSubItem> _subItems = new List<TodoSubItem>();
        public IReadOnlyCollection<TodoSubItem> SubItems => _subItems;
        public void AddSubItem(TodoSubItem subItem)
        {
            _subItems.Add(subItem);
        }
    }
}