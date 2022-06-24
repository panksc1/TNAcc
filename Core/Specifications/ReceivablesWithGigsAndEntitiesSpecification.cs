using Core.Entities;

namespace Core.Specifications
{
public class ReceivablesWithGigsAndEntitiesSpecification : BaseSpecification<Receivable>
    {
        public ReceivablesWithGigsAndEntitiesSpecification(PaymentSpecParams paymentParams) 
            : base(x => 
                (string.IsNullOrEmpty(paymentParams.Search) || 
                x.Entity.Name.ToLower().Contains(paymentParams.Search) ||
                x.Gig.Venue.Name.ToLower().Contains(paymentParams.Search) ||
                x.InvoiceNumber.ToLower().Contains(paymentParams.Search)) &&
                (!paymentParams.PaymentStatus.HasValue || 
                (paymentParams.PaymentStatus == 1 && (x.AmountDue <= x.AmountPaid)) ||
                (paymentParams.PaymentStatus == 2 && (x.AmountDue > x.AmountPaid))) &&
                (!paymentParams.GigId.HasValue || x.GigId == paymentParams.GigId) && 
                (!paymentParams.EntityId.HasValue || x.EntityId == paymentParams.EntityId) &&
                (!paymentParams.Month.HasValue || 
                (x.DateReceived.Month == paymentParams.Month) && (x.DateReceived != DateTime.MinValue)) &&
                (!paymentParams.Year.HasValue || 
                (x.DateReceived.Year == paymentParams.Year ) && (x.DateReceived != DateTime.MinValue))
            )
        {
            AddInclude("Gig.Venue");
            AddInclude(x => x.Entity);
            
            ApplyPaging(paymentParams.PageSize * (paymentParams.PageIndex - 1), paymentParams.PageSize);

            if(!string.IsNullOrEmpty(paymentParams.Sort))
            {
                switch(paymentParams.Sort)
                {
                    case "payAsc":
                        AddOrderBy(p => p.AmountDue);
                        break;
                    case "payDesc":
                        AddOrderByDescending(p => p.AmountDue);
                        break;
                    case "dateAsc":
                        AddOrderBy(p => p.DateReceived);
                        break;
                    case "dateDesc":
                        AddOrderByDescending(p => p.DateReceived);
                        break;
                    default:
                        AddOrderByDescending(n => n.DateReceived);
                        break;
                }
            } else {
                AddOrderByDescending(x => x.DateReceived);
            }
        }

        // Pass the criteria of Product Id == id to the Base Specification 
        // method and include the product type and product brand
        public ReceivablesWithGigsAndEntitiesSpecification(int id) 
            : base(x => x.Id == id)
        {
            AddInclude(x => x.Gig);
            AddInclude(x => x.Entity);
        }
    }
}