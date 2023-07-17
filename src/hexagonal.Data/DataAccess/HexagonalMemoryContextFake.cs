using System.Reflection;
using hexagonal.Data.Extensions;
using hexagonal.Domain;

namespace hexagonal.Data.DataAccess;

public static class HexagonalMemoryContextFake
{
    private const string JsonPath = "hexagonal.Data.SeedData";
    private static readonly object SyncLock = new();

    /// <summary>
    ///     To reset memory database use -> context.Database.EnsureDeleted().
    /// </summary>
    public static void AddDataFakeContext(HexagonalContext? context)
    {
        var assembly = Assembly.GetAssembly(typeof(JsonUtilities));

        if (context == null || assembly is null) return;
        if (context.SystemUsers.Any()) return;

        lock (SyncLock)
        {
            var systemUsers = JsonUtilities.GetListFromJson<SystemUser>(
                assembly.GetManifestResourceStream($"{JsonPath}.system-user.json"));

            systemUsers?.ForEach(entity =>
            {
                var isRegistred = context.SystemUsers.Any(x => x.Id == entity.Id);
                if (!isRegistred)
                    context.SystemUsers.Add(entity);
            });

            var categories = JsonUtilities.GetListFromJson<Category>(
                assembly.GetManifestResourceStream($"{JsonPath}.category.json"));

            categories?.ForEach(entity =>
            {
                var isRegistred = context.Categories.Any(x => x.Id == entity.Id);
                if (!isRegistred)
                    context.Categories.Add(entity);
            });

            var books = JsonUtilities.GetListFromJson<Book>(
                assembly.GetManifestResourceStream($"{JsonPath}.book.json"));

            books?.ForEach(entity =>
            {
                var isRegistred = context.Books.Any(x => x.Id == entity.Id);
                if (!isRegistred)
                    context.Books.Add(entity);
            });

            context.SaveChanges();
        }
    }
}