namespace ReactRandomJokes.Data
{
    public class UserLikedJokes
    {
        public int UserId { get; set; }
        public int JokeId { get; set; }
        public bool Liked { get; set; }
    }
}
