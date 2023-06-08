using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace GamesCollectionV3.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string Username { get; set; }

        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime Timestamp { get; set; }

        public string Description { get; set; }

        public int GameId { get; set; }
        public Game? Game { get; set; }
    }
}
