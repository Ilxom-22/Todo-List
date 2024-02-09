using System.Linq.Expressions;
using Todo.Domain.Entities;
using Todo.Persistence.DataContexts;
using Todo.Persistence.Repositories.Interfaces;

namespace Todo.Persistence.Repositories;

public class TodoRepository(AppDbContext dbContext)
    : EntityRepositoryBase<TodoItem, AppDbContext>(dbContext), ITodoRepository
{
    public new IQueryable<TodoItem> Get(Expression<Func<TodoItem, bool>>? predicate = default, bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }

    public new ValueTask<TodoItem?> GetByIdAsync(Guid todoId, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(todoId, asNoTracking, cancellationToken);
    }

    public new ValueTask<TodoItem> CreateAsync(TodoItem todoItem, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(todoItem, saveChanges, cancellationToken);
    }

    public new ValueTask<TodoItem> UpdateAsync(TodoItem todoItem, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(todoItem, saveChanges, cancellationToken);
    }

    public new ValueTask<TodoItem?> DeleteByIdAsync(Guid todoId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(todoId, saveChanges, cancellationToken);
    }
}