using LucruIndividual.Models.DbEntities;

namespace LucruIndividual.Models.Admin
{
    public class AdminPanelModel
    {
        public ICollection<Profile> profiles { get; set; }
        public int pageNumber { get;set; }
        public int totalPages { get; set; }
        public int pageSize { get; set; }
    }
}
