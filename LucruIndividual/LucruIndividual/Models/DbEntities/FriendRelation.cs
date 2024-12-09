using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LucruIndividual.Models.DbEntities
{
    public class FriendRelation
    {
        [Key]
        public int id { get; set; }

        public int user1Id { get; set; }
        public int user2Id { get; set; }

        public bool status { get; set; }

        public User user1 { get; set; }
        public User user2 { get; set; }
    }
}
