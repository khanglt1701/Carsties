'use client'

import React from 'react'
import { AiOutlineCar } from 'react-icons/ai'
import { useParamsStore } from '../hooks/useParamsStore'

function Logo() {
  const reset = useParamsStore(state => state.reset);

  return (
    <div>
      <div onClick={reset} className='flex items-center gap-2 text-3xl font-semibold text-red-500 cursor-pointer'>
            <AiOutlineCar size={34} />
            <div>Carsties Auctions</div>
        </div>
    </div>
  )
}

export default Logo