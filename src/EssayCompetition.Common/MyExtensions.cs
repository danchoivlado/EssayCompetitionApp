namespace EssayCompetition.Common
{
    using System.Linq;

    public static class MyExtensions
    {
        public static IQueryable<T> ToQueryable<T>(this T instance)
        {
            return new[] { instance }.AsQueryable();
        }
    }
}
