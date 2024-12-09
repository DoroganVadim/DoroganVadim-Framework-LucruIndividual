using LucruIndividual.Models.DbEntities;

namespace LucruIndividual.Models.Home
{
    public class ProfileInformation
    {
        public Profile profile {  get; set; }
        public int friendCheck {  get; set; }
        public bool isTheUser { get; set; }
        public ICollection<Post> posts;
    }
}
