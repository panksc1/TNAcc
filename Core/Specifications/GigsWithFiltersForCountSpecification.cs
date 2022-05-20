using Core.Entities;

namespace Core.Specifications
{
    public class GigsWithFiltersForCountSpecification : BaseSpecification<Gig>
    {
        public GigsWithFiltersForCountSpecification(GigSpecParams gigParams) 
            : base(x => 
                (!gigParams.VenueId.HasValue || x.VenueId == gigParams.VenueId)
            )
        {

        }
    }
}