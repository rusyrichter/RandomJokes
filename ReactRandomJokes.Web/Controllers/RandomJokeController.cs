using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactRandomJokes.Data;
using ReactRandomJokes.Web.ViewModels;
using System.Text.Json;

namespace ReactRandomJokes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RandomJokeController : ControllerBase
    {

        private string _connectionString;

        public RandomJokeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getjoke")]
        public Joke GetJoke()
        {
            var repo = new RJRepository(_connectionString);
            var joke = repo.GetJoke();
            if (!repo.JokeExists(joke.OriginId))
            {
                repo.AddJokeToDB(joke);
                return joke;
            }
            return repo.GetByOriginId(joke.OriginId);
        }

        [HttpPost]
        [Route("addLike")]
        public void AddLike(UserLikedJokes ulj)
        {
            var repo = new UserRepository(_connectionString);
            var user = repo.GetByEmail(User.Identity.Name);
            var RJrepo = new RJRepository(_connectionString);
            RJrepo.AddLike(ulj, user.Id);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("viewall")]
        public List<Joke> GetAllJokes()
        {
            var repo = new RJRepository(_connectionString);
            var jokes = repo.GetJokes();
            return jokes;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getlikescount/{jokeid}")]
        public LikesAndDislikes GetLikes(int jokeId)
        {
            var repo = new RJRepository(_connectionString);
            var userLikedJokes = repo.GetUserLikedJokes(jokeId);
            return new LikesAndDislikes
            {
                Likes = userLikedJokes.Count(u => u.Liked == true),
                DisLikes = userLikedJokes.Count(u => u.Liked == false),
            };
        
        }
        [HttpGet]
        [Route("getstatus")]
        public Status GetStatus(int id)
        {
            var repo = new RJRepository(_connectionString);
            var repo2 = new UserRepository(_connectionString);
            var user = repo2.GetByEmail(User.Identity.Name);
            return repo.GetStatus(id, user.Id);
           
        }

        
    }
}

