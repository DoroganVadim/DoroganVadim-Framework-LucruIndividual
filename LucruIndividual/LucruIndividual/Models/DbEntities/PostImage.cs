using System.ComponentModel.DataAnnotations;

namespace LucruIndividual.Models.DbEntities
{
    public class PostImage
    {
        [Key]
        public int id { get; set; }
        public string extension { get; set; }
        public string category {  get; set; }

        public PostImage() {
            ;
        }
        public PostImage(string extension, string category)
        {
            this.extension = extension;
            this.category = category;
        }
    }
}
