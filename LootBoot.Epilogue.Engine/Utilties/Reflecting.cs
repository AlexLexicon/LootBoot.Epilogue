namespace LootBoot.Epilogue.Engine.Utilties;
public class Reflecting
{
    public static bool IsEnumerable(object obj) => obj is not null && IsEnumerable(obj.GetType());
    public static bool IsEnumerable<T>() => IsEnumerable(typeof(T));
    public static bool IsEnumerable(Type type) => type is not null && type != typeof(string) && typeof(IEnumerable).IsAssignableFrom(type);
    public static bool IsOrDerivedFrom(Type derivedType, Type baseType) => derivedType is not null && baseType is not null && (derivedType == baseType || derivedType.IsSubclassOf(baseType));
    public static Type GetDerivedTypeOfEnumerable(IEnumerable enumerable)
    {
        if (enumerable == null)
        {
            return null;
        }
        Type genericEnumerableInterface = enumerable.GetType().GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        if (genericEnumerableInterface == null)
        {
            IEnumerable<Type> commonTypes = GetCommonTypesOfEnumerable(enumerable);
            return commonTypes is null || commonTypes.Count() <= 0 ? null : commonTypes.First();
        }
        Type elementType = genericEnumerableInterface.GetGenericArguments()[0];
        return elementType.IsGenericType && elementType.GetGenericTypeDefinition() == typeof(Nullable<>)
            ? elementType.GetGenericArguments()[0]
            : elementType;
    }
    private static IEnumerable<Type> GetCommonTypesOfEnumerable(IEnumerable enumerable)
    {
        HashSet<Type> types = new HashSet<Type>();
        foreach (object item in enumerable)
        {
            types.Add(item.GetType());
        }
        return types.Select(t => GetBaseTypes(t)).Aggregate((a, b) => a.Intersect(b));
    }
    private static IEnumerable<Type> GetBaseTypes(Type type)
    {
        yield return type;
        Type baseType = type.BaseType;
        while (baseType != null)
        {
            yield return baseType;
            baseType = baseType.BaseType;
        }
    }
}
