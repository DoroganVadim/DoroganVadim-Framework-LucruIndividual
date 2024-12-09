using System.ComponentModel.DataAnnotations;

namespace LucruIndividual.Models.DbEntities
{
    public class ProfileImage
    {
        [Key]
        public int id { get; set; }
        public string extension { get; set; }
        public string category { get; set; }

        public ProfileImage()
        {
            ;
        }
        public ProfileImage(string extension, string category)
        {
            this.extension = extension;
            this.category = category;
        }
    }
}
