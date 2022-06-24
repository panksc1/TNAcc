import { IGig } from "./gig";

export interface IReceivable {
    id: number;
    amountDue: number;
    amountPaid: number;
    dateReceived: Date;
    invoiceNumber: string;
    method: string;
    notes: string;
    entity: string;
    gig: IGig;
}