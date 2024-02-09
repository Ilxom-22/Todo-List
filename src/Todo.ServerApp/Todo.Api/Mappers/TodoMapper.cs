using AutoMapper;
using Todo.Api.Dtos;
using Todo.Domain.Entities;

namespace Todo.Api.Mappers;

public class TodoMapper : Profile
{
    public TodoMapper()
    {
        CreateMap<TodoItem, TodoDto>().ReverseMap();
    }
}