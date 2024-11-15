import React from 'react'
import { Auction } from '../types'
import Link from 'next/link'
import Image from 'next/image'

type Props = {
  auction: Auction
}

function AuctionCreatedToast({auction}: Props) {
  return (
    <Link href={`/auctions/details/${auction.id}`} className='fkex flex-col items-center'>
      <div className='flex flex-row items-center gap-2'>
        <Image 
          src={auction.imageUrl}
          alt='Image of car'
          height={80}
          width={80}
          className='rounded-lg w-auto h-auto'  
        />
        <span>New Auction! {auction.make} {auction.model} has been added</span>
      </div>
    </Link>
  )
}

export default AuctionCreatedToast