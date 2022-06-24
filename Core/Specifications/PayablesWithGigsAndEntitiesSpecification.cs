using Core.Entities;

namespace Core.Specifications
{
    public class PayablesWithGigsAndEntitiesSpecification : BaseSpecification<Payable>
    {
        public PayablesWithGigsAndEntitiesSpecification(PaymentSpecParams paymentParams) 
            : base(x => 
                (string.IsNullOrEmpty(paymentParams.Search) || 
                x.Entity.Name.ToLower().Contains(paymentParams.Search) ||
                (x.Gig.Venue.Name.ToLower().Contains(paymentParams.Search))) &&
                (!paymentParams.PaymentStatus.HasValue || 
                (paymentParams.PaymentStatus == 1 && (x.AmountDue <= x.AmountPaid)) ||
                (paymentParams.PaymentStatus == 2 && (x.AmountDue > x.AmountPaid))) &&
                (!paymentParams.GigId.HasValue || x.GigId == paymentParams.GigId) && 
                (!paymentParams.EntityId.HasValue || x.EntityId == paymentParams.EntityId) &&
                (!paymentParams.Month.HasValue || 
                (x.DatePaid.Month == paymentParams.Month) && (x.DatePaid != DateTime.MinValue)) &&
                (!paymentParams.Year.HasValue || 
                (x.DatePaid.Year == paymentParams.Year ) && (x.DatePaid != DateTime.MinValue))
            )
        {
            AddInclude("Gig.Venue");
            AddInclude(x => x.Entity);
            
            ApplyPaging(paymentParams.PageSize * (paymentParams.PageIndex - 1), paymentParams.PageSize);

            if(!string.IsNullOrEmpty(paymentParams.Sort))
            {
                switch(paymentParams.Sort)
                {
                    case "entity":
                        AddOrderBy(p => p.Entity.Name);
                        break;
                    case "payDesc":
                        AddOrderByDescending(p => p.AmountDue);
                        break;
                    case "dateAsc":
                        AddOrderBy(p => p.DatePaid);
                        break;
                    case "dateDesc":
                        AddOrderByDescending(p => p.DatePaid);
                        break;
                    default:
                        AddOrderByDescending(n => n.DatePaid);
                        break;
                }
            } else {
                AddOrderByDescending(x => x.DatePaid);
            }
            
        }

        // Pass the criteria of Product Id == id to the Base Specification 
        // method and include the product type and product brand
        public PayablesWithGigsAndEntitiesSpecification(int id) 
            : base(x => x.Id == id)
        {
            AddInclude("Gig.Venue");
            AddInclude(x => x.Entity);
        }
    }
}