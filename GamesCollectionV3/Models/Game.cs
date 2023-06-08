using System.ComponentModel.DataAnnotations;


namespace GamesCollectionV3.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public string Developer { get; set; }

        public string Platforms { get; set; }

        public string Genre { get; set; }

        public string Description { get; set; }

        public List<Review>? Reviews { get; set; }
    }
}
