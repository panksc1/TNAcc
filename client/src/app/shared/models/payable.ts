import { IGig } from "./gig";

export interface IPayable {
    id: number;
    amountDue: number;
    amountPaid: number;
    datePaid: Date;
    method: string;
    notes: string;
    entityId: number;
    entity: string;
    gig: IGig;
}