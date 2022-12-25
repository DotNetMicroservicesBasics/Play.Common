namespace Play.Common.Settings
{
    public class MongoDbSettings
    {

        private string _connectionString;
        public string Host { get; init; }
        public int Port { get; init; }

        public string DbName { get; init; }

        //public string ConnectionString => $"mongodb://{Host}:{Port}";


        public string ConnectionString
        {
            get
            {
                return string.IsNullOrWhiteSpace(_connectionString) ? $"mongodb://{Host}:{Port}" : _connectionString;
            }
            init { _connectionString = value; }
        }


    }
}