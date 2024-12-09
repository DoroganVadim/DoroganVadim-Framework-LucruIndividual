using LucruIndividual.Models.DbEntities;

namespace LucruIndividual.Models.Home
{
    public class FriendsInformationModel
    {
        public ICollection<Profile> profiles { get; set; }
        public bool isUser {  get; set; }
    }
}
