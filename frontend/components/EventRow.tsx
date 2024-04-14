import Link from 'next/link'
import React from 'react'
import IEvent from '../interfaces/Event'

const EventRow = ({ event, index }: { event: IEvent, index: number }) => {
  return (
    <div className='mx-4 my-2 flex'>
        <Link className='flex h-full flex-1' href={`osalejad/` + event.id}>
          <div className='w-8/12'>
            <p>{index + 1}. {event.name}</p>
          </div>
          <div className='w-2/12 flex content-center h-full'>
            <p className='inline-block'>{event.startTime}</p>
          </div>
          <div className='w- flex content-center h-full ml-auto'>
            <p className='inline-block ml-auto'>Osavõtjad</p>
          </div>
          </Link>
          <div className='flex content-center h-full w-5 flex-none'>
            <div className="bg-cover bg-center h-3 w-3 m-1 " style={{ backgroundImage: "url('/images/remove.svg')", backgroundRepeat: "no-repeat", backgroundSize: "cover" }}></div>
          </div>
    </div>
  )
}

export default EventRow