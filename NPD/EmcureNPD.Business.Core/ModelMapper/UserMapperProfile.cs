using AutoMapper;
using System.Reflection;

namespace EmcureNPD.Business.Core.ModelPaper
{
    public class MapperProfile<TSource, TDestination> : Profile
    {
        public MapperProfile()
        {
            CreateMap<TSource, TDestination>().IgnoreAllNonExisting();
        }
    }

    public static class IMappingExpression
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>
(this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties(flags);

            foreach (var property in destinationProperties)
            {
                if (sourceType.GetProperty(property.Name, flags) == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;
        }
    }
}