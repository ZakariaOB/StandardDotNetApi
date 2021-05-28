namespace StandardApi.Contracts
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Messages
        {
            public const string GetAll = Base + "/messages";

            public const string GetAllWithPrevilige = Base + "/messages/prev";

            public const string Create = Base + "/messages";

            public const string Get = Base + "/messages/{messageId}";

            public const string Update = Base + "/messages/{messageId}";

            public const string Delete = Base + "/messages/{messageId}";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";

            public const string Register = Base + "/identity/register";

            public const string Refresh = Base + "/identity/refresh";
        }

        public static class Tag
        {
            public const string GetAll = Base + "/tags";
        }
    }
}
