namespace Linkify.Application.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Checks if a list is not null and contains at least one item.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to check.</param>
        /// <returns>True if the list is not null and contains at least one item; otherwise, false.</returns>
        public static bool IsNotNullAndAny<T>(this IEnumerable<T> list)
        {
            return list != null && list.Any();
        }

        /// <summary>
        /// Checks if a list is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to check.</param>
        /// <returns>True if the list is not null and contains at least one item; otherwise, false.</returns>
        public static bool IsNullAndEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || !list.Any();
        }
    }
}
