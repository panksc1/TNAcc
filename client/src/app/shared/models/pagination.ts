import { IGig } from "./gig";

export interface IPagination {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: IGig[];
}
