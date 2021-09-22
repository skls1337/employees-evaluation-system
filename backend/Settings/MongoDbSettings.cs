namespace Backend.Settings
{   
    public class MongoDbSettings
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string DatabaseName{get;set;}
        public string ConnectionString
        {
            get 
            {
                return $"mongodb+srv://{User}:{Password}@{Host}/{DatabaseName}?retryWrites=true&w=majority";
            }
        }
    }
}