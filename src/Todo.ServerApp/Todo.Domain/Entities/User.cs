using Todo.Domain.Common.Entities;

namespace Todo.Domain.Entities;

public class User : IEntity
{
    public Guid Id { get; set; }
}