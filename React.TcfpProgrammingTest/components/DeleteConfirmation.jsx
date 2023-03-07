
import { GrCircleAlert } from 'react-icons/Gr'
export default function DeleteConfirmation({toggleModal, toggleDeleteConfirmation, mode, reports, setReports, selected, setMode, setSelected}) {

  /* Delete report upon confirmation */
  const handleDelete = () => {
    setReports(reports.filter(report => report.Id !== selected.Id));
    setSelected(null);
    setMode("create");
  } 

  return (
    <>
    <div className='font-bold bg-black text-white p-3 md:text-lg lg:text-xl rounded-t-lg'>
                {mode === "delete" && `Report Removal Confirmation`}
              </div>
    <div className='flex justify-center self-center py-8 gap-3'>
      <GrCircleAlert size={32}/>
      <div className='text-lg'>Are you sure you want to remove this report?</div>
    </div>
    <div className='flex justify-evenly mb-4 '>
      <button className='p-2 font-semibold  text-white  md:text-lg lg:text-xl rounded  bg-red-500 hover:bg-red-600' onClick={() => {
        toggleModal();
        handleDelete();
        toggleDeleteConfirmation();
      }}>Delete</button>
      <button className='p-2 font-semibold  text-white  md:text-lg lg:text-xl rounded  bg-red-500 hover:bg-red-600'  onClick={() => {
        toggleModal();
        toggleDeleteConfirmation();
      }}>close</button>
    </div>
    </>
  )
}
