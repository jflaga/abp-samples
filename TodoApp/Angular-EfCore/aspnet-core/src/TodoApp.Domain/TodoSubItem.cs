using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace TodoApp
{
    public class TodoSubItem : Entity<Guid>
    {
        protected TodoSubItem() { }
        public TodoSubItem(Guid Id)
            : base(Id) { }

        public string Text { get; set; }
    }
}
