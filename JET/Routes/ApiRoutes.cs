namespace JET.Routes
{
    public static class ApiRoutes
    {
        public const string Domain = "api";
        public const string Version = "v{version:apiVersion}";
        public const string Base = Domain;

        public static class Product
        {
            public const string Create = Base + "/products";
            public const string Get = Base + "/products/{id}";
            public const string GetAll = Base + "/products";
            public const string Patch = Base + "/products/{id}";
            public const string Delete = Base + "/products/{id}";
        }
    }
}
