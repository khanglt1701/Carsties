import { Button, ButtonGroup } from 'flowbite-react';
import React from 'react'

type Props = {
  pageSize: number,
  setPageSize: (size: number) => void
}

const pageSizeButton = [4, 8, 12];

function Filters({pageSize, setPageSize}: Props) {
  return (
    <div className='flex justify-between items-center mb-4'>
      <div>
        <span className='uppercase text-sm text-gray-500 mr-2'>Page Size</span>
        <ButtonGroup>
          {pageSizeButton.map((value, index) => (
            <Button key={index} 
              onClick={() => setPageSize(value)} 
              color={`${pageSize === value ? 'red' : 'gray'}`}
            >
              {value}
            </Button>
          ))}
        </ButtonGroup>
      </div>
    </div>
  )
}

export default Filters