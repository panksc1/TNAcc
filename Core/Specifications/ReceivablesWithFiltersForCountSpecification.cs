using Core.Entities;

namespace Core.Specifications
{
    public class ReceivablesWithFiltersForCountSpecification : BaseSpecification<Receivable>
    {
        public ReceivablesWithFiltersForCountSpecification(PaymentSpecParams paymentParams)
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
        }
    }
}