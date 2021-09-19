using AutoMapper;

namespace MinAPIDemo.Models.Mapper
{
    public class GuidConverter : ITypeConverter<string, Guid>
    {
        public Guid Convert(string source, Guid destination, ResolutionContext context)
        {
            return Guid.Parse(source);
        }
    }
}