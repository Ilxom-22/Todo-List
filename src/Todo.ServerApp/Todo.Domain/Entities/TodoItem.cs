﻿using Todo.Domain.Common.Entities;

namespace Todo.Domain.Entities;

public class TodoItem : IEntity
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = default!;

    public bool IsDone { get; set; }

    public bool IsFavorite { get; set; }

    public DateTimeOffset DueTime { get; set; }

    public DateTimeOffset ReminderTime { get; set; }
    
    public DateTimeOffset CreatedTime { get; set; }

    public DateTimeOffset? ModifiedTime { get; set; }
}