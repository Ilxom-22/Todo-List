using System.Linq.Expressions;
using Todo.Domain.Entities;

namespace Todo.Persistence.Repositories.Interfaces;

public interface ITodoRepository
{
    IQueryable<TodoItem> Get(Expression<Func<TodoItem, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<TodoItem?> GetByIdAsync(Guid todoId, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<TodoItem> CreateAsync(TodoItem todoItem, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<TodoItem> UpdateAsync(TodoItem todoItem, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<TodoItem?> DeleteByIdAsync(Guid todoId, bool saveChanges = true, CancellationToken cancellationToken = default);
}