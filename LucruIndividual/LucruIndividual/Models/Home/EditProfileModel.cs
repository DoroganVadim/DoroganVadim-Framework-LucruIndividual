using LucruIndividual.Models.DbEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LucruIndividual.Models.Account;

namespace LucruIndividual.Models.Home
{
    public class PhoneNumberValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if(value == null)
            {
                return true;
            }
            if (value is string phoneNumber)
            {
                string pattern = @"^\+?\d{1,4}?[\s\-]?\(?\d{1,3}?\)?[\s\-]?\d{1,3}[\s\-]?\d{1,4}$";
                return Regex.IsMatch(phoneNumber, pattern);
            }
            return false;
        }
    }
    public class EditProfileModel
    {
        public ProfileImage? profileImage { get; set; }

        [Required(ErrorMessage = "Data de nastere este obligatorie")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [CustomDateValidation(ErrorMessage = "Data nu este valida")]
        public DateTime birthDay { get; set; }

        [StringLength(500, ErrorMessage = "Descrierea trebuie sa aiba maxim 500 caractere")]
        public string? aboutUser { get; set; }

        public string? nickname { get; set; }

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

        [PhoneNumberValidation(ErrorMessage = "Numarul introdus are forma incorecta")]
        public string? phoneNumber { get; set; }
    }
}
