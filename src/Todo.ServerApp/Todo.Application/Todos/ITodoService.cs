using Todo.Domain.Entities;

namespace Todo.Application.Todos;

public interface ITodoService
{
    ValueTask<IList<TodoItem>> GetAsync(bool asNoTracking = false);

    ValueTask<TodoItem?> GetByIdAsync(Guid todoId, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<TodoItem> CreateAsync(TodoItem todoItem, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<TodoItem> UpdateAsync(TodoItem todoItem, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<TodoItem?> DeleteByIdAsync(Guid todoId, bool saveChanges = true, CancellationToken cancellationToken = default);
}