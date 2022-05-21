namespace StandardApi.Contracts
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string VersionOne = "v1";

        public const string VersionTwo = "v2";


        public static class Messages
        {
            public const string GetAllVOne = Root + "/" + VersionOne + "/messages";

            public const string GetAllVTwo = Root + "/" + VersionTwo + "/messages";


        }
    }
}
