import { Button, ButtonGroup } from 'flowbite-react';
import React from 'react'
import { useParamsStore } from '../hooks/useParamsStore';

const pageSizeButton = [4, 8, 12];

function Filters() {
  const pageSize = useParamsStore(state => state.pageSize);
  const setParams = useParamsStore(state => state.setParams);

  return (
    <div className='flex justify-between items-center mb-4'>
      <div>
        <span className='uppercase text-sm text-gray-500 mr-2'>Page Size</span>
        <ButtonGroup>
          {pageSizeButton.map((value, index) => (
            <Button key={index} 
              onClick={() => setParams({pageSize: value})} 
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