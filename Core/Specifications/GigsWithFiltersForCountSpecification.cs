using Core.Entities;

namespace Core.Specifications
{
    public class GigsWithFiltersForCountSpecification : BaseSpecification<Gig>
    {
        public GigsWithFiltersForCountSpecification(GigSpecParams gigParams) 
            : base(x => 
                (string.IsNullOrEmpty(gigParams.Search) || 
                x.Venue.Name.ToLower().Contains(gigParams.Search) ||
                x.Band.Name.ToLower().Contains(gigParams.Search)) &&
                (!gigParams.VenueId.HasValue || x.VenueId == gigParams.VenueId) &&
                (!gigParams.BandId.HasValue || x.BandId == gigParams.BandId) &&
                (!gigParams.Month.HasValue || x.Date.Month == gigParams.Month) &&
                (!gigParams.Year.HasValue || x.Date.Year == gigParams.Year)
            )
        {

        }
    }
}