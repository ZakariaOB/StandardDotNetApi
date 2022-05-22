﻿namespace StandardApi.Contracts
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Messages
        {
            public const string GetAll = Base + "/messages";

            public const string Create = Base + "/messages";

            public const string Get = Base + "/messages/{messageId}";

            public const string Update = Base + "/messages/{messageId}";

            public const string UpdateUsingQueryParam = Base + "/messages";

            public const string Delete = Base + "/messages/{messageId}";
        }
    }
}
