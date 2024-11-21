'use client'

import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ReactNode, useCallback, useEffect, useRef } from 'react'
import { useAuctionStore } from '../hooks/useAuctionStore';
import { useBidStore } from '../hooks/useBidStore';
import { useParams } from 'next/navigation';
import { Auction, AuctionFinished, Bid } from '../types';
import { User } from 'next-auth';
import toast from 'react-hot-toast';
import AuctionCreatedToast from '../components/AuctionCreatedToast';
import { getDetailedViewData } from '../actions/auctionAction';
import AuctionFinishedToast from '../components/AuctionFinishedToast';

type Props = {
  children: ReactNode;
  user: User | null;
  notifyUrl: string
}

function SignalRProvider({children, user, notifyUrl}: Props) {
  const connection = useRef<HubConnection | null>(null);
  const setCurrentPrice = useAuctionStore(state => state.setCurrentPrice);
  const addBid = useBidStore(state => state.addBid);
  const params = useParams<{id: string}>();

  const handleAuctionFinished = useCallback((finishedAuction: AuctionFinished) => {
    const auction = getDetailedViewData(finishedAuction.auctionId);
    return toast.promise(auction, {
      loading: 'Loading',
      success: (auction) => 
        <AuctionFinishedToast 
          auction={auction} 
          finishedAuction={finishedAuction}
        />,
      error: (err) => `Auction finished ${err}`
    }, {success: {duration: 10000, icon: null}})
  }, []) 

  const handleAuctionCreated = useCallback((auction: Auction) => {
    if (user?.username !== auction.seller) {
      return toast(<AuctionCreatedToast auction={auction} />, {
        duration: 10000
      })
    }
  }, [user?.username])

  // use useCallback to avoid re-render function every time component re-render
  const handleBidPlaced = useCallback((bid: Bid) => {
    if (bid.bidStatus.includes('Accepted')) {
      setCurrentPrice(bid.auctionId, bid.amount);
    }

    if (params.id === bid.auctionId) {
      addBid(bid);
    } 
  }, [setCurrentPrice, addBid, params.id])

  useEffect(() => {
    if (!connection.current) {
      connection.current = new HubConnectionBuilder()
        .withUrl(notifyUrl)
        .withAutomaticReconnect()
        .build();

      connection.current.start()
        .then(() => 'Connected to notification hub')
        .catch((err) => console.log(err))

    }

    connection.current.on('BidPlaced', handleBidPlaced)
    connection.current.on('AuctionCreated', handleAuctionCreated)
    connection.current.on('AuctionFinished', handleAuctionFinished)

    return () => {
      connection.current?.off('BidPlaced', handleBidPlaced);
      connection.current?.off('AuctionCreated', handleAuctionCreated);
      connection.current?.off('AuctionFinished', handleAuctionFinished);
    }
  }, [setCurrentPrice, handleBidPlaced, handleAuctionCreated, handleAuctionFinished, notifyUrl])

  return (
    children
  )
}

export default SignalRProvider