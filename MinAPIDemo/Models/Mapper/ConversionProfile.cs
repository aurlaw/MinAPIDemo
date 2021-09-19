using AutoMapper;

namespace MinAPIDemo.Models.Mapper
{
    public class ConversionProfile : Profile
    {
        public ConversionProfile()
        {
            CreateMap<string, Guid>().ConvertUsing<GuidConverter>();
        }
    }
}
