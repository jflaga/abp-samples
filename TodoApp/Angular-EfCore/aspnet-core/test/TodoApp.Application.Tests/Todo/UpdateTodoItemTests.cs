//using Shouldly;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Volo.Abp.Identity;
//using Xunit;

//namespace TodoApp.Todo;

//public class UpdateTodoItemTests : TodoAppApplicationTestBase
//{
//    private readonly ITodoAppService _todoAppService;

//    public UpdateTodoItemTests()
//    {
//        _todoAppService = GetRequiredService<ITodoAppService>();
//    }

//    [Fact]
//    public async Task Success_on_happy_path()
//    {
//        // Arrange
//        var todoItemCreationDto = new TodoItemCreationDto
//        {
//            Text = "Todo item 1",
//            SubItems = new List<TodoSubItemCreationDto>
//            {
//                new TodoSubItemCreationDto { Text = "subitem 1"},
//                new TodoSubItemCreationDto { Text = "subitem 2"}
//            }
//        };
//        var createResult = await _todoAppService.CreateAsync(todoItemCreationDto);

//        // Act

//        // Assert
//        result.ShouldNotBeNull();
//        result.Text.ShouldBe("Todo item 1");
//        result.SubItems.Count().ShouldBe(2);
//        // TODO: more asserts
//    }
//}
