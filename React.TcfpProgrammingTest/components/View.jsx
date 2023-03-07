import React from 'react'

export default function View({ selected, toggleViewModal }) {
  const { Name, Type, Location, Description, Status, Department, DateOfIncident, CreatedAt } = selected;

  /* Render view of selected report */
  return (

    <div className='max-h-screen overflow-y-auto'>
      <div className='font-bold bg-black text-white p-3 md:text-lg lg:text-xl rounded-t-lg'>
        <div>Report Details</div>
      </div>
      <div className="grid grid-col-1  md:grid-cols-2 gap-4 p-4">
  <div className="bg-red-50 rounded shadow-md p-6">
    <p className="font-bold text-gray-800 mb-2 border-b-2 border-black pb-2">Name</p>
    <p className="text-gray-700">{Name}</p>
  </div>
  <div className="bg-red-50 rounded shadow-md p-6">
    <p className="font-bold text-gray-800 mb-2 border-b-2 border-black pb-2">Type</p>
    <p className="text-gray-700">{Type}</p>
  </div>
  <div className='flex-col  self-center'>
    <div className="bg-red-50 rounded shadow-md p-6 h-min">
        <p className="font-bold text-gray-800 mb-2 border-b-2 border-black pb-2">Location</p>
        <p className="text-gray-700">{Location}</p>
    </div>
  </div>
  <div className="bg-red-50 rounded shadow-md p-6">
    <p className="font-bold text-gray-800 mb-2 border-b-2 border-black pb-2">Description</p>
    <p className="text-gray-700">{Description}</p>
  </div>
  <div className="bg-red-50 rounded shadow-md p-6">
    <p className="font-bold text-gray-800 mb-2 border-b-2 border-black pb-2">Status</p>
    <p className="text-gray-700">{Status}</p>
  </div>
  <div className="bg-red-50 rounded shadow-md p-6">
    <p className="font-bold text-gray-800 mb-2 border-b-2 border-black pb-2">Department</p>
    <p className="text-gray-700">{Department.Name}</p>
  </div>
  <div className="bg-red-50 rounded shadow-md p-6">
    <p className="font-bold text-gray-800 mb-2 border-b-2 border-black pb-2">Date of Incident</p>
    <p className="text-gray-700">{DateOfIncident.toLocaleString()}</p>
  </div>
  <div className="bg-red-50 rounded shadow-md p-6">
    <p className="font-bold text-gray-800 mb-2 border-b-2 border-black pb-2">Created At</p>
    <p className="text-gray-700">{CreatedAt.toLocaleString()}</p>
  </div>
</div>
      <div className='grid-col-1 p-4'>
        <div className='flex justify-center'>
          <button className='py-2 font-semibold w-full text-white  md:text-lg lg:text-xl rounded  bg-red-500 hover:bg-red-600' onClick={() => toggleViewModal()}>
            close
          </button>
        </div>
      </div>
    </div>

  )
}
