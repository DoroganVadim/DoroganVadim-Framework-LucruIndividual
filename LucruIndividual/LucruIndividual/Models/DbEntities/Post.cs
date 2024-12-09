using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LucruIndividual.Models.DbEntities
{
    public class Post
    {
        [Key]
        public int id { get; set; }
        
        [ForeignKey("idImage")]
        public PostImage? image { get; set; }
        
        [ForeignKey("idProfile")]
        public Profile profile { get; set; }
        
        public bool status { get; set; }

        public string postText { get; set; }
        public DateTime createdTime { get; set; }

        public Post()
        {
            createdTime = DateTime.Now;
        }
    }
}
