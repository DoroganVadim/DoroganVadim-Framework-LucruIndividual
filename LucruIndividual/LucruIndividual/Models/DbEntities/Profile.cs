using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace LucruIndividual.Models.DbEntities
{
    public class Profile
    {
        [Key]
        public int userId { get; set; }

        [ForeignKey("userId")]
        public User user { get; set; }

        public DateTime birthDay { get; set; }
        public string? aboutUser { get; set; }
        public ProfileImage? profileImage { get; set; }
        public string? nickname { get; set; }
        public string name { get; set; }
        public string surrname { get; set; }
        public int sex { get; set; }
        public string? phoneNumber { get; set; }
    }
}
