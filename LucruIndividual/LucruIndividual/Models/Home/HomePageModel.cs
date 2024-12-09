using LucruIndividual.Models.DbEntities;

namespace LucruIndividual.Models.Home
{
    public class HomePageModel
    {
        public PostInformation ? post { get; set; }
        public ICollection<Post> ? otherPosts { get; set; }
    }
}
