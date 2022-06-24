export class PaymentParams {
    gigId: number = 0;
    entityId: number = 0;
    sort = 'dateDesc';
    pageNumber = 1;
    pageSize = 30;
    search: string;
    month: number = 0;
    year: number = 0;
    paymentStatus: number = 0;
}