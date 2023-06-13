using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReactRandomJokes.Data
{

    public class Joke
    {
        public int Id { get; set; }
        [JsonPropertyName("JokeId")]
        public int OriginId { get; set; }
        public string Setup { get; set; }
        public string Punchline { get; set; }

        public List<UserLikedJokes> UserLikedJokes { get; set; }
    }
}


