namespace StandardApi.Options
{
    public class RedisCachingSettings
    {
        public bool Enabled { get; set; }

        public string ConnectionString { get; set; }
    }
}
