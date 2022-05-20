using Core.Entities;

namespace Core.Specifications
{
    public class ReceivablesWithFiltersForCountSpecification : BaseSpecification<Receivable>
    {
        public ReceivablesWithFiltersForCountSpecification(PaymentSpecParams paymentParams)
            : base(x => 
                (!paymentParams.GigId.HasValue || x.GigId == paymentParams.GigId) && 
                (!paymentParams.EntityId.HasValue || x.EntityId == paymentParams.EntityId)
            )
        {
        }
    }
}