namespace HashBasedSearch;

public static class HashBasedSearchCallbackBuilder
{

    #region Without elementSelector, without comparer


    public static HashBasedSearchCallback<TKey, TSource?> BuildHashBasedSearchCallback<TSource, TKey>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        where TKey : notnull
    {
        Dictionary<TKey, TSource> dictionary = source.ToDictionary(keySelector);

        if (dictionary.Count is 0)
            return _ => default;

        TSource? GetValueOrDefault(TKey key)
        {
            dictionary.TryGetValue(key, out TSource? value);
            return value;
        }

        return GetValueOrDefault;
    }

    public static HashBasedSearchCallback<TKey, TSource?> BuildHashBasedSearchCallbackNullable<TSource, TKey>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) 
        where TSource : struct
        where TKey : notnull
    {
        Dictionary<TKey, TSource> dictionary = source.ToDictionary(keySelector);

        if (dictionary.Count is 0)
            return _ => null;

        TSource? GetValueOrDefault(TKey key)
        {
            bool success = dictionary.TryGetValue(key, out TSource value);
            return success ? value : null;
        }

        return GetValueOrDefault;
    }

    public static HashBasedSearchCallback<TKey, TSource> BuildHashBasedSearchCallback<TSource, TKey>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, TSource defaultValue)
        where TKey : notnull
    {
        Dictionary<TKey, TSource> dictionary = source.ToDictionary(keySelector);

        if (dictionary.Count is 0)
            return _ => defaultValue;

        TSource GetValueOrDefault(TKey key)
        {
            bool found = dictionary.TryGetValue(key, out TSource? value);
            if (found)
                return value!;

            return defaultValue;
        }

        return GetValueOrDefault;
    }

    #endregion


    #region With elementSelector, without comparer


    public static HashBasedSearchCallback<TKey, TElement?> BuildHashBasedSearchCallback<TSource, TKey, TElement>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        where TKey : notnull
    {
        Dictionary<TKey, TElement> dictionary = source.ToDictionary(keySelector, elementSelector);

        if (dictionary.Count is 0)
            return _ => default;

        TElement? GetValueOrDefault(TKey key)
        {
            dictionary.TryGetValue(key, out TElement? value);
            return value;
        }

        return GetValueOrDefault;
    }

    public static HashBasedSearchCallback<TKey, TElement?> BuildHashBasedSearchCallbackNullable<TSource, TKey, TElement>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        where TKey : notnull
        where TElement : struct
    {
        Dictionary<TKey, TElement> dictionary = source.ToDictionary(keySelector, elementSelector);

        if (dictionary.Count is 0)
            return _ => null;

        TElement? GetValueOrDefault(TKey key)
        {
            bool success = dictionary.TryGetValue(key, out TElement value);
            return success ? value : null;
        }

        return GetValueOrDefault;
    }


    public static HashBasedSearchCallback<TKey, TElement> BuildHashBasedSearchCallback<TSource, TKey, TElement>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector,
        TElement defaultValue)
        where TKey : notnull
    {
        Dictionary<TKey, TElement> dictionary = source.ToDictionary(keySelector, elementSelector);

        if (dictionary.Count is 0)
            return _ => defaultValue;

        TElement? GetValueOrDefault(TKey key)
        {
            bool found = dictionary.TryGetValue(key, out TElement? value);
            if (found)
                return value!;

            return defaultValue;
        }

        return GetValueOrDefault;
    }

    #endregion


    #region Without elementSelector, with comparer

    public static HashBasedSearchCallback<TKey, TSource?> BuildHashBasedSearchCallback<TSource, TKey>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer)
        where TKey : notnull
    {
        Dictionary<TKey, TSource> dictionary = source.ToDictionary(keySelector, comparer);

        if (dictionary.Count is 0)
            return _ => default;

        TSource? GetValueOrDefault(TKey key)
        {
            dictionary.TryGetValue(key, out TSource? value);
            return value;
        }

        return GetValueOrDefault;
    }

    public static HashBasedSearchCallback<TKey, TSource?> BuildHashBasedSearchCallbackNullable<TSource, TKey>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer)
        where TKey : notnull
        where TSource : struct
    {
        Dictionary<TKey, TSource> dictionary = source.ToDictionary(keySelector, comparer);

        if (dictionary.Count is 0)
            return _ => null;

        TSource? GetValueOrDefault(TKey key)
        {
            bool success = dictionary.TryGetValue(key, out TSource value);
            return success ? value : null;
        }

        return GetValueOrDefault;
    }


    public static HashBasedSearchCallback<TKey, TSource> BuildHashBasedSearchCallback<TSource, TKey>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer,
        TSource defaultValue)
        where TKey : notnull
    {
        Dictionary<TKey, TSource> dictionary = source.ToDictionary(keySelector, comparer);

        if (dictionary.Count is 0)
            return _ => defaultValue;

        TSource GetValueOrDefault(TKey key)
        {
            bool found = dictionary.TryGetValue(key, out TSource? value);
            if (found)
                return value!;

            return defaultValue;
        }

        return GetValueOrDefault;
    }

    #endregion


    #region With elementSelector, with comparer


    public static HashBasedSearchCallback<TKey, TElement?> BuildHashBasedSearchCallback<TSource, TKey, TElement>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector,
        IEqualityComparer<TKey>? comparer)
        where TKey : notnull
    {
        Dictionary<TKey, TElement> dictionary = source.ToDictionary(keySelector, elementSelector, comparer);

        if (dictionary.Count is 0)
            return _ => default;

        TElement? GetValueOrDefault(TKey key)
        {
            dictionary.TryGetValue(key, out TElement? value);
            return value;
        }

        return GetValueOrDefault;
    }

    public static HashBasedSearchCallback<TKey, TElement?> BuildHashBasedSearchCallbackNullable<TSource, TKey, TElement>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector,
            IEqualityComparer<TKey>? comparer)
        where TKey : notnull
        where TElement : struct
    {
        Dictionary<TKey, TElement> dictionary = source.ToDictionary(keySelector, elementSelector, comparer);

        if (dictionary.Count is 0)
            return _ => null;

        TElement? GetValueOrDefault(TKey key)
        {
            bool success = dictionary.TryGetValue(key, out TElement value);
            return success ? value : null;
        }

        return GetValueOrDefault;
    }


    public static HashBasedSearchCallback<TKey, TElement> BuildHashBasedSearchCallback<TSource, TKey, TElement>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector,
        IEqualityComparer<TKey>? comparer, TElement defaultValue)
        where TKey : notnull
    {
        Dictionary<TKey, TElement> dictionary = source.ToDictionary(keySelector, elementSelector, comparer);

        if (dictionary.Count is 0)
            return _ => defaultValue;

        TElement? GetValueOrDefault(TKey key)
        {
            bool found = dictionary.TryGetValue(key, out TElement? value);
            if (found)
                return value!;

            return defaultValue;
        }

        return GetValueOrDefault;
    }

    #endregion

}