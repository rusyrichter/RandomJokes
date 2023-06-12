using Microsoft.IdentityModel.Tokens;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReactRandomJokes.Data
{
    public class RJRepository
    {
        public string _connectionString { get; set; }

        public RJRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Joke GetJoke()
        {
            var client = new HttpClient();
            var json = client.GetStringAsync("https://jokesapi.lit-projects.com/jokes/programming/random").Result;
            var joke = JsonSerializer.Deserialize<List<Joke>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }).First();
            return joke;
        }
        public void AddLike(UserLikedJokes ulj, int userId)
        {
            using var context = new RJDataContext(_connectionString);

            var userjokelikes = context.UserLikedJokes.FirstOrDefault(u => u.UserId == userId && u.JokeId == ulj.JokeId);

            if (userjokelikes == null)
            {
                context.UserLikedJokes.Add(new UserLikedJokes
                {
                    UserId = userId,
                    JokeId = ulj.JokeId,
                    Liked = ulj.Liked,
                });
            }
            else
            {
                userjokelikes.Liked = ulj.Liked;
            }

            context.SaveChanges();
        }
        public void AddJokeToDB(Joke joke)
        {
            using var context = new RJDataContext(_connectionString);
            context.Jokes.Add(joke);
            context.SaveChanges();
        }
        public List<Joke> GetJokes()
        {
            using var context = new RJDataContext(_connectionString);
            return context.Jokes.ToList();
        }
        public Status GetStatus(int jokeId, int userId)
        {
            using var context = new RJDataContext(_connectionString);
            var ulj = context.UserLikedJokes.FirstOrDefault(u => u.JokeId == jokeId && u.UserId == userId);
            if (ulj == null)
            {
                return Status.NeverInteracted;
            }
            if (ulj.Liked == true)
            {
                return Status.Liked;
            }
            return Status.Disliked;
        }
      
        public Joke GetJokeForCount(int jokeId)
        {
            using var context = new RJDataContext(_connectionString);
            return context.Jokes.Include(j => j.UserLikedJokes)
                                .FirstOrDefault(j => j.OriginId == jokeId);
        }

    }
}
