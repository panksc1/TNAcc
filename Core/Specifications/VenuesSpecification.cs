using Core.Entities;

namespace Core.Specifications
{
    public class VenuesSpecification : BaseSpecification<Venue>
    {
        public VenuesSpecification(VenueSpecParams venueParams)
            : base()
        {
            ApplyPaging(venueParams.PageSize * (venueParams.PageIndex - 1), venueParams.PageSize);

            if (!string.IsNullOrEmpty(venueParams.Sort))
            {
                switch (venueParams.Sort)
                {
                    case "name":
                        AddOrderBy(v => v.Name);
                        break;
                    case "at":
                        AddOrderBy(v => v.At);
                        break;
                    default:
                        AddOrderBy(v => v.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(v => v.Name);
            }
        }

        // Pass the criteria of Product Id == id to the Base Specification 
        // method and include the product type and product brand
        public VenuesSpecification(int id)
            : base(x => x.Id == id)
        {
        }
    }
}