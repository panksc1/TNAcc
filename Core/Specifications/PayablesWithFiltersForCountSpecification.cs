using Core.Entities;

namespace Core.Specifications
{
    public class PayablesWithFiltersForCountSpecification : BaseSpecification<Payable>
    {
        public PayablesWithFiltersForCountSpecification(PaymentSpecParams paymentParams) 
            : base(x => 
                (!paymentParams.GigId.HasValue || x.GigId == paymentParams.GigId) && 
                (!paymentParams.EntityId.HasValue || x.EntityId == paymentParams.EntityId)
            )
        {

        }
    }
}