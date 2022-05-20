using Core.Entities;

namespace Core.Specifications
{
    public class GigsWithVenuesSpecification : BaseSpecification<Gig>
    {
        public GigsWithVenuesSpecification(GigSpecParams gigParams)
            : base(x =>
                (!gigParams.VenueId.HasValue || x.VenueId == gigParams.VenueId) 
            )
        {
            AddInclude(x => x.Venue);
            AddOrderByDescending(x => x.Date);
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
                    default:
                        AddOrderByDescending(n => n.Date);
                        break;
                }
            }
        }

        // Pass the criteria of Product Id == id to the Base Specification 
        // method and include the product type and product brand
        public GigsWithVenuesSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.Venue);
        }
    }
}