using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LucruIndividual.Models.Account
{
    public class NameValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is string name)
            {
                string pattern = @"^[a-zA-Z]+$";
                return Regex.IsMatch(name, pattern);
            }
            return false;
        }
    }
    public class CustomDateValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }
            if (value is DateTime date)
            {
                return date.Date < DateTime.Today && date.Date > new DateTime(1900, 1, 1);
            }
            return false;
        }
    }
    public class RegisterModel
    {
        [Required(ErrorMessage = "Nu a fost introdus login")]
        [EmailAddress(ErrorMessage = "Adresa de email nu este valida")]
        public string email { get; set; }

        [Required(ErrorMessage = "Nu a fost introdusa parola")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Parola trebuie sa aiba cel putin 8 caractere")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[a-z])(?=.*[\W_]).{8,}$", ErrorMessage = "Parola trebuie sa contina cel putin o litera mare, un numar si un caracter special.")]
        public string password { get; set; }

        [Required(ErrorMessage = "Nu a fost introdusa confirmarea parola")]
        [Compare("password", ErrorMessage = "Nu coincide parola")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }

        [Required(ErrorMessage = "Data de nastere este obligatorie")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [CustomDateValidation(ErrorMessage = "Data nu este valida")]
        public DateTime birthDay { get; set; }

        [Required(ErrorMessage = "Prenumele este obligatoriu")]
        [StringLength(100, ErrorMessage = "Numele nu poate depasi 100 de caractere.")]
        [NameValidation(ErrorMessage = "Prenumele trebuie sa contina doar litere")]
        public string name { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu")]
        [StringLength(100, ErrorMessage = "Prenumele nu poate depasi 100 de caractere.")]
        [NameValidation(ErrorMessage = "Numele trebuie sa contina doar litere")]
        public string surrname { get; set; }

        [Required(ErrorMessage = "Nu a fost introdus sexul")]
        public string sex { get; set; }
    }
}
