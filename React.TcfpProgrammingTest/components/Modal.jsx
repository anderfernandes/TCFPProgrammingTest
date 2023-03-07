import React from 'react'

export default function Modal({ injuryReportForm , view, deleteConfirmation, showDeleteConfirmation, showView }) {


  return (
    <>
    {showDeleteConfirmation ? (
      <div className="bg-black/25 absolute top-0 left-0 w-full h-full flex items-center justify-center">
            <div className="bg-white rounded-lg w-1/3 grid gap-3">
              {deleteConfirmation()}
            </div>
          </div>
    ) : showView ? (
      <div className="bg-black/25 absolute top-0 left-0 w-full h-full flex items-center justify-center">
            <div className="bg-white rounded-lg w-1/2 grid gap-3">
              {view()}
            </div>
          </div>
    ) : (
      <div className="bg-black/25 absolute top-0 left-0 w-full h-full flex items-center justify-center">
            <div className="bg-white rounded-lg w-1/3 grid gap-3">
              {injuryReportForm()}
            </div>
          </div>
    )}
    </>
  )
}
