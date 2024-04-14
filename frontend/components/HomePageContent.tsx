import React from 'react'
import Image from 'next/image'
import EventInfo from './EventInfo'
import EventRow from './EventRow'
import IEvent from '@/interfaces/Event'

const HomePageContent = () => {
    let events : IEvent[] = [{
        id: "12",
        startTime: 123,
        location: "Talinn",
        name: "maraton",
        additionalInformation: "cola",
        participantEvents: []
    },
    {
        id: "13",
        startTime: 124,
        location: "Rapla",
        name: "jooks",
        additionalInformation: "fanta",
        participantEvents: []
    }
    ]

  return (
    <div className='flex flex-col w-full h-full'>
        <div className='h-1/2 w-full flex flex-row mb-4 flex-1'>
            <div className='bg-primary h-full flex justify-center items-center p-10' style={{ width: 'calc(50% - 0.5rem)' }}>
                <p className='text-white font-light'>Sed nec elit vestibulum, <span className='text-white font-semibold'>tincidunt orci</span> et, sagittis ex. Vestibulum rutrum <span className='text-white font-semibold'>neque suscipit</span> ante mattis maximus. Nulla non sapien <span className='text-white font-semibold'>viverra, lobortis lorem non</span>, accumsan metus.</p>
            </div>
                <div className='h-full' style={{ backgroundImage: "url('/images/pilt.jpg')", backgroundRepeat: "no-repeat", backgroundSize: "cover", width: 'calc(50% + 0.5rem)'}}>
            </div>

        </div>
        <div className='flex flex-row space-x-4 w-full h-full flex-1'>
            <EventInfo title='Tulevased Üritused'>
            {events.map((event : any, index : number) => (
                <EventRow key={index} event={event} index={index}></EventRow>
      ))}
                </EventInfo>
            <EventInfo title='Toimunud Üritused'>
            {events.map((event : any, index : number) => (
                <EventRow key={index} event={event} index={index}></EventRow>
      ))}
            </EventInfo>
        </div>
    </div>
  )
}

export default HomePageContent