using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Volo.Abp.Domain.Entities;

namespace TodoApp
{
    public class TodoItem : BasicAggregateRoot<Guid>
    {
        protected TodoItem() { /* for EF use */ }
        public TodoItem(Guid Id)
            : base(Id) { }

        public string Text { get; set; }

        private readonly List<TodoSubItem> _subItems = new List<TodoSubItem>();
        public IReadOnlyCollection<TodoSubItem> SubItems => _subItems;
        public void AddSubItem(TodoSubItem subItem)
        {
            _subItems.Add(subItem);
        }

        public static class Mapper
        {
            // NOTE: AutoMapper.Collection is not working if I use IReadOnlyCollection
            // Solution: https://github.com/AutoMapper/AutoMapper.Collection/issues/132#issuecomment-539411397
            public static readonly Expression<Func<TodoItem, ICollection<TodoSubItem>>> SubItems = (tl) => tl._subItems;
        }
    }
}