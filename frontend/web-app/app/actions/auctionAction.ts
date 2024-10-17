'use server'

import { FieldValues } from "react-hook-form";
import { fetchWrapper } from "../lib/fetchWrapper";
import { Auction, PagedResult } from "../types";

export async function getData(query: string): Promise<PagedResult<Auction>> {
  return await fetchWrapper.get(`search${query}`)
}

export async function createAuction(data: FieldValues) {
  return await fetchWrapper.post('/auctions', data);
}

export async function getDetailedViewData(id: string): Promise<Auction> {
  return await fetchWrapper.get(`/auctions/${id}`);
}