using LucruIndividual.Models.DbEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LucruIndividual.Models.Home
{
    public class PostInformation
    {
        public bool status { get; set; }

        [Required(ErrorMessage = "Nu a fost introdus textul postarii")]
        [StringLength(500, ErrorMessage = "Textul postarii trebuie sa aiba maxim 500 caractere")]
        public string postText { get; set; }

        public string image;
    }
}
