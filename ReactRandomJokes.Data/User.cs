using System.Text.Json.Serialization;

namespace ReactRandomJokes.Data
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Joke> Jokes {get;set;}
        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}