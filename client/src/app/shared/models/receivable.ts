import { IGig } from "./gig";

export interface IReceivable {
    id: number;
    amountDue: number;
    amountPaid: number;
    dateReceived: Date;
    invoiceNumber: string;
    method: string;
    notes: string;
    entityId: number;
    entity: string;
    gig: IGig;
}