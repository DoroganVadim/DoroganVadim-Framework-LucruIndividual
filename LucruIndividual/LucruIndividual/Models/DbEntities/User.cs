using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LucruIndividual.Models.DbEntities
{
    public class User
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Nu a fost introdus login")]
        [EmailAddress(ErrorMessage = "Adresa de email nu este valida")]
        public string email { get; set; }

        [Required(ErrorMessage = "Nu a fost introdusa parola")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Parola trebuie sa aiba cel putin 8 caractere")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[a-z])(?=.*[\W_]).{8,}$", ErrorMessage = "Parola trebuie sa contina cel putin o litera mare, un numar si un caracter special.")]
        public string password { get; set; }

        public int role { get; set; }

        public bool status { get; set; } = true;
    }
}
