import React from 'react'

function Update({params}: {params: {id: string}}) {
  return (
    <div>Update for {params.id}</div>
  )
}

export default Update