using Core.Entities;

namespace Core.Specifications
{
    public class GigsWithVenuesSpecification : BaseSpecification<Gig>
    {
        public GigsWithVenuesSpecification(GigSpecParams gigParams)
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
            AddInclude(x => x.Venue);
            AddInclude(x => x.Band);
            ApplyPaging(gigParams.PageSize * (gigParams.PageIndex - 1), gigParams.PageSize);

            if (!string.IsNullOrEmpty(gigParams.Sort))
            {
                switch (gigParams.Sort)
                {
                    case "dateAsc":
                        AddOrderBy(p => p.Date);
                        break;
                    case "dateDesc":
                        AddOrderByDescending(p => p.Date);
                        break;
                    case "payAsc":
                        AddOrderBy(p => p.Pay);
                        break;
                    case "payDesc":
                        AddOrderByDescending(p => p.Pay);
                        break;
                    case "venue":
                        AddOrderBy(p => p.Venue.Name);
                        break;
                    case "band":
                        AddOrderBy(p => p.Band.Name);
                        break;
                    default:
                        AddOrderByDescending(n => n.Date);
                        break;
                }
            }
            else
            {
                AddOrderByDescending(n => n.Date);
            }
        }

        // Pass the criteria of Product Id == id to the Base Specification 
        // method and include the product type and product brand
        public GigsWithVenuesSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.Venue);
            AddInclude(x => x.Band);
        }
    }
}