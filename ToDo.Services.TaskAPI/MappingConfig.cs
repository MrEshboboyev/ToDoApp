using AutoMapper;
using ToDo.Services.TaskAPI.Models.Dto;
using Task = ToDo.Services.TaskAPI.Models.Task;

namespace Mango.Services.TaskAPI
{
    public static class MappingConfig 
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Task, TaskDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
