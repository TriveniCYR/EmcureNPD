using System.Collections.Generic;

namespace EmcureNPD.Business.Core.ModelMapper
{
    public interface IMapperFactory
    {
        TDestination Get<TSource, TDestination>(TSource source);

        IEnumerable<TDestination> GetList<TSource, TDestination>(IEnumerable<TSource> source);

        List<TDestination> GetList<TSource, TDestination>(List<TSource> source);
    }
}