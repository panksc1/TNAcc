using Core.Entities;

namespace Core.Specifications
{
    public class PayablesWithFiltersForCountSpecification : BaseSpecification<Payable>
    {
        public PayablesWithFiltersForCountSpecification(PaymentSpecParams paymentParams) 
            : base(x => 
                (string.IsNullOrEmpty(paymentParams.Search) || 
                x.Entity.Name.ToLower().Contains(paymentParams.Search)||
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

        }
    }
}