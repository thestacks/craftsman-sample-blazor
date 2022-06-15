namespace RecipeManagement.FunctionalTests.TestUtilities;
public class ApiRoutes
{
    public const string Base = "api";
    public const string Health = Base + "/health";

    // new api route marker - do not delete

public static class Ingredients
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/ingredients";
        public const string GetRecord = $"{Base}/ingredients/{Id}";
        public const string Create = $"{Base}/ingredients";
        public const string Delete = $"{Base}/ingredients/{Id}";
        public const string Put = $"{Base}/ingredients/{Id}";
        public const string Patch = $"{Base}/ingredients/{Id}";
        public const string CreateBatch = $"{Base}/ingredients/batch";
    }

public static class Recipes
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/recipes";
        public const string GetRecord = $"{Base}/recipes/{Id}";
        public const string Create = $"{Base}/recipes";
        public const string Delete = $"{Base}/recipes/{Id}";
        public const string Put = $"{Base}/recipes/{Id}";
        public const string Patch = $"{Base}/recipes/{Id}";
        public const string CreateBatch = $"{Base}/recipes/batch";
    }
}
