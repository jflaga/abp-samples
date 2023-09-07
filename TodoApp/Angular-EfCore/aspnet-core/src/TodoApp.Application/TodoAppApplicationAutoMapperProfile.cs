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

        CreateMap<TodoSubItemDto, TodoSubItem>()
            .ReverseMap();

        CreateMap<TodoItemCreationDto, TodoItem>()
            .Ignore(x => x.SubItems);
        CreateMap<TodoSubItemCreationDto, TodoSubItem>();

        CreateMap<TodoItemUpdateDto, TodoItem>()
            .ForMember(TodoItem.Mapper.SubItems, opt => opt.MapFrom(dto => dto.SubItems))
            .ForMember(e => e.SubItems, opt => opt.Ignore())
            .Ignore(x => x.Id);
        CreateMap<TodoSubItemUpdateDto, TodoSubItem>()
            .EqualityComparison((a, b) => a.Id == b.Id);
    }
}
