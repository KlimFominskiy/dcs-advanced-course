using System.Reflection;
using Loans.Application.AppServices.Contracts.Mappers;

namespace Loans.Application.AppServices.Mappers;

public class Mapper<TSource, TDestination> : IMapper<TSource, TDestination>
{
    public TDestination Map(TSource source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        TDestination destination = Activator.CreateInstance<TDestination>();

        foreach (var sourceProperty in typeof(TSource).GetProperties())
        {
            PropertyInfo? destinationProperty = typeof(TDestination).GetProperty(sourceProperty.Name);
            if (destinationProperty != null && destinationProperty.CanWrite)
            {
                object? value = sourceProperty.GetValue(source);
                destinationProperty.SetValue(destination, value);
            }
        }

        return destination;
    }
}