using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TodoApp.Todo;

public class UpdateTodoItemTests : TodoAppApplicationTestBase
{
    private readonly ITodoAppService _todoAppService;

    public UpdateTodoItemTests()
    {
        _todoAppService = GetRequiredService<ITodoAppService>();
    }

    [Fact]
    public async Task Success_on_adding_new_subitem()
    {
        // Arrange
        var todoItemCreationDto = new TodoItemCreationDto
        {
            Text = "Todo item 1",
            SubItems = new List<TodoSubItemCreationDto>
            {
                new TodoSubItemCreationDto { Text = "subitem 1"},
                new TodoSubItemCreationDto { Text = "subitem 2"}
            }
        };
        var createResult = await _todoAppService.CreateAsync(todoItemCreationDto);

        var subitems = new List<TodoSubItemUpdateDto>(
            (from subItem in createResult.SubItems
             select new TodoSubItemUpdateDto
             {
                 Id = subItem.Id,
                 Text = subItem.Text + " updated",
             }).ToList());
        subitems.Add(new TodoSubItemUpdateDto { Text = "new subitem" });

        var todoItemUpdateDto = new TodoItemUpdateDto
        {
            Id = createResult.Id,
            Text = createResult.Text + " updated",
            SubItems = subitems
        };

        // Act
        var result = await _todoAppService.UpdateAsync(todoItemUpdateDto);

        // Assert
        result.ShouldNotBeNull();
        result.Text.ShouldBe("Todo item 1 updated");
        result.SubItems.Count().ShouldBe(3);
        result.SubItems.ShouldContain(x => x.Text == "subitem 1 updated");
        result.SubItems.ShouldContain(x => x.Text == "subitem 2 updated");
        result.SubItems.ShouldContain(x => x.Text == "new subitem");
    }

    [Fact]
    public async Task Success_on_deleting_subitem()
    {
        // Arrange
        var todoItemCreationDto = new TodoItemCreationDto
        {
            Text = "Todo item 1",
            SubItems = new List<TodoSubItemCreationDto>
            {
                new TodoSubItemCreationDto { Text = "subitem 1"},
                new TodoSubItemCreationDto { Text = "subitem 2"}
            }
        };
        var createResult = await _todoAppService.CreateAsync(todoItemCreationDto);

        var todoItemUpdateDto = new TodoItemUpdateDto
        {
            Id = createResult.Id,
            Text = createResult.Text + " updated",
            SubItems = new List<TodoSubItemUpdateDto>
            {
                new TodoSubItemUpdateDto
                {
                    Id = createResult.SubItems.First().Id,
                    Text = createResult.SubItems.First().Text + " updated",
                }
            }
        };

        // Act
        var result = await _todoAppService.UpdateAsync(todoItemUpdateDto);

        // Assert
        result.ShouldNotBeNull();
        result.Text.ShouldBe("Todo item 1 updated");
        result.SubItems.Count().ShouldBe(1);
        result.SubItems.ShouldContain(x => x.Text == "subitem 1 updated");
    }

    [Fact]
    public async Task Success_on_deletingand_adding_subitems()
    {
        // Arrange
        var todoItemCreationDto = new TodoItemCreationDto
        {
            Text = "Todo item 1",
            SubItems = new List<TodoSubItemCreationDto>
            {
                new TodoSubItemCreationDto { Text = "subitem 1"},
                new TodoSubItemCreationDto { Text = "subitem 2"}
            }
        };
        var createResult = await _todoAppService.CreateAsync(todoItemCreationDto);

        var subitems = new List<TodoSubItemUpdateDto>();
        subitems.Add(new TodoSubItemUpdateDto
        {
            Id = createResult.SubItems.First().Id,
            Text = createResult.SubItems.First().Text + " updated",
        });
        subitems.Add(new TodoSubItemUpdateDto { Text = "new subitem" });

        var todoItemUpdateDto = new TodoItemUpdateDto
        {
            Id = createResult.Id,
            Text = createResult.Text + " updated",
            SubItems = subitems
        };

        // Act
        var result = await _todoAppService.UpdateAsync(todoItemUpdateDto);

        // Assert
        result.ShouldNotBeNull();
        result.Text.ShouldBe("Todo item 1 updated");
        result.SubItems.Count().ShouldBe(2);
        result.SubItems.ShouldContain(x => x.Text == "subitem 1 updated");
        result.SubItems.ShouldContain(x => x.Text == "new subitem");
    }
}
