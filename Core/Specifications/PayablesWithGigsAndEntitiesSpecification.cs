using Core.Entities;

namespace Core.Specifications
{
    public class PayablesWithGigsAndEntitiesSpecification : BaseSpecification<Payable>
    {
        public PayablesWithGigsAndEntitiesSpecification(PaymentSpecParams paymentParams) 
            : base(x => 
                (!paymentParams.GigId.HasValue || x.GigId == paymentParams.GigId) && 
                (!paymentParams.EntityId.HasValue || x.EntityId == paymentParams.EntityId)
            )
        {
            AddInclude(x => x.Gig);
            AddInclude(x => x.Entity);
            AddOrderByDescending(x => x.DatePaid);
            ApplyPaging(paymentParams.PageSize * (paymentParams.PageIndex - 1), paymentParams.PageSize);

            if(!string.IsNullOrEmpty(paymentParams.Sort))
            {
                switch(paymentParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Amount);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Amount);
                        break;
                    default:
                        AddOrderByDescending(n => n.DatePaid);
                        break;
                }
            }
        }

        // Pass the criteria of Product Id == id to the Base Specification 
        // method and include the product type and product brand
        public PayablesWithGigsAndEntitiesSpecification(int id) 
            : base(x => x.Id == id)
        {
            AddInclude(x => x.Gig);
            AddInclude(x => x.Entity);
        }
    }
}