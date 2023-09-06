using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Volo.Abp.AutoMapper;

namespace TodoApp;

public class TodoAppApplicationAutoMapperProfile : Profile
{
    public TodoAppApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        //CreateMap<TodoItemDto, TodoItem>().ReverseMap().EqualityComparison((a, b) => a.Id == b.Id);
        //CreateMap<TodoSubItemDto, TodoSubItem>().ReverseMap().EqualityComparison((a, b) => a.Id == b.Id);

        CreateMap<TodoSubItemDto, TodoSubItem>().ReverseMap();

        CreateMap<TodoItemCreationDto, TodoItem>()
            .Ignore(x => x.SubItems);
        CreateMap<TodoSubItemCreationDto, TodoSubItem>();

    }
}
