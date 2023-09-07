using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Xunit;

namespace TodoApp.Todo;

public class CreateTodoItemTests : TodoAppApplicationTestBase
{
    private readonly ITodoAppService _todoAppService;

    public CreateTodoItemTests()
    {
        _todoAppService = GetRequiredService<ITodoAppService>();
    }

    [Fact]
    public async Task Success_on_happy_path()
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

        // Act
        var result = await _todoAppService.CreateAsync(todoItemCreationDto);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(Guid.Empty);
        result.Text.ShouldBe("Todo item 1");
        result.SubItems.Count().ShouldBe(2);
        result.SubItems.ShouldContain(x => x.Text == "subitem 1");
        result.SubItems.ShouldContain(x => x.Text == "subitem 2");
    }
}
