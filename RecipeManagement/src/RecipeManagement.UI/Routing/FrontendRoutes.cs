using Flurl;

namespace RecipeManagement.UI.Routing;

public static class FrontendRoutes
{
    public static string GetRouteWithParameter(string route, object parameterValue)
    {
        return string.Join('/', route.Split('/').SkipLast(1)).AppendPathSegment(parameterValue.ToString());
    }
    
    public const string Home = "/";

    public static class Recipes
    {
        private const string Prefix = "/recipes/";
        public const string Index = Prefix;
        public const string New = Prefix + "new";
        public const string Edit = Prefix + "edit/{Id}";
    }

    public static class Ingredients
    {
        private const string Prefix = "/ingredients/";
        public const string Index = Prefix;
        public const string New = Prefix + "new";
        public const string Edit = Prefix + "edit/{Id}";
    }
}