import { IGig } from "./gig";
import { IPayable } from "./payable";
import { IReceivable } from "./receivable";

export interface IPagination {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: IGig[] | IPayable[] | IReceivable[];
}
