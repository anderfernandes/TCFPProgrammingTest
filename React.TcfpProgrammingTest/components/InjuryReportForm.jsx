import React, { useState } from 'react'
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { string, z } from "zod";

 const schema = z.object({
  Status: string().min(1, {message: 'Name is required.'}),
  DepartmentId: string().min(1),
  Type: string().min(1),
  Name: string().min(1, {message: 'Name is required.'}),
  Description: string().min(1),
  Location: string().min(1),
  DateOfIncident: string().min(1),
}) 

export default function InjuryReportForm({toggleModal, mode, selected, departments, reports, setReports}) {

    const { register, handleSubmit } = useForm({
        defaultValues: {
          Status: selected?.Status ?? "",
          DepartmentId: selected?.Department?.Id ?? "",
          Type: selected?.Type ?? "",
          Name: selected?.Name ?? "",
          Description: selected?.Description ?? "",
          Location: selected?.Location ?? "",
          DateOfIncident: selected?.DateOfIncident?.toJSON().split(".")[0] ?? ""
        },
        resolver: zodResolver(schema),
    });

    

    // TODO: Save new and updated reports.
  const onSaveReport = (formValues) => {
    // TODO: prevent default
    
  
    // The form element is currently uncontrolled but you can change it if you want.
    //
    const departmentIndex = departments.findIndex(dept => dept.Id == formValues.DepartmentId);
    const dateOfIncidentFormat = new Date(formValues.DateOfIncident);

    if (mode == "create") {
      // TODO: A new report will need an Id and a Date on the CreatedAt property.
      const newId = reports.length + 1;
      const createdAt = new Date();
      
      const newReport = {
        Id: newId,
        CreatedAt: createdAt,
        Department: departments[departmentIndex],
        ...formValues,
        DateOfIncident: dateOfIncidentFormat,
      };
      setReports([...reports, newReport]);  

    } else {
      // Here you will need to find and update the report within the array of reports.
      const indexToUpdate = reports.findIndex((report) => report.Id === selected.Id);
      //Update the report with the new form values
      const updatedReport = {
      ...selected,
      ...formValues,
      Department: departments[departmentIndex],
      DateOfIncident: dateOfIncidentFormat,
    };

    //Create a new copy of the array and update it
    const newReports = [...reports];
    newReports[indexToUpdate] = updatedReport;

    //update the array state with the newly updated array
    setReports(newReports);
    }
    // TODO: Don't forget to close the modal and reset the form.
    toggleModal();
  };

  return (
    <>
    <div className='font-bold bg-black text-white p-3 md:text-lg lg:text-xl rounded-t-lg'>
                {mode === "create" && `New Incident Report`}
                {mode !== "create" && `Report #${selected.Id}`}
              </div>
              <div className="flex justify-start p-3">
                <form
                  className="flex flex-col gap-2 w-full"
                  onSubmit={handleSubmit(onSaveReport)}
                >
                  <div className="flex flex-col gap-2 text-left">
                    <label htmlFor="Status" className="font-semibold">
                      Status
                    </label>
                    <select
                      {...register("Status")}
            
                      className="border p-2 rounded w-full border-gray-300"
                     
                    >
                      <option disabled value="">
                        Select one
                      </option>
                      <option>Open</option>
                      <option>Pending</option>
                      <option>Closed</option>
                    </select>
                  
                  </div>
                  <div className="flex flex-col gap-2 text-left">
                    <label htmlFor="DepartmentId" className="font-semibold">
                      Department
                    </label>
                    <select
                      {...register("DepartmentId")}
                      
                      className="border p-2 rounded w-full border-gray-300"
                      
                      
                    >
                      <option disabled value="">
                        Select one
                      </option>
                      {departments.map((department) => (
                        <option value={department.Id} key={department.Id}>
                          {department.Name}
                        </option>
                      ))}
                    </select>
                  </div>
                  <div className="flex flex-col gap-2 text-left">
                    <label htmlFor="Type" className="font-semibold">
                      Type
                    </label>
                    <select
                      {...register("Type")}
                      
                      className="border border-gray-300 p-2 rounded w-full"
                      
                    >
                      <option disabled value="">
                        Select one
                      </option>
                      <option>Training</option>
                      <option>Accident</option>
                      <option>Other</option>
                    </select>
                  </div>
                  <div className="flex flex-col gap-2 text-left">
                    <label htmlFor="Name" className="font-semibold">
                      Name
                    </label>
                    <input
                      
                      type="text" 
                      
                      {...register("Name")}
                      className="border-gray-300 rounded"
                      placeholder="The name of the incident."
                      
                    />
                
                  </div>
                  <div className="flex flex-col gap-2 text-left">
                    <label htmlFor="Description" className="font-semibold">
                      Description
                    </label>
                    <textarea
                      
                      {...register("Description")}
                      placeholder="Short description of the incident."
                      className="border-gray-300 rounded"
                      
                    ></textarea>
                  </div>
                  <div className="flex flex-col gap-2 text-left">
                    <label htmlFor="Location" className="font-semibold">
                      Location
                    </label>
                    <input
                      {...register("Location")}
                       
                      type="text"
                      
                      className="border-gray-300 rounded"
                      placeholder="Address where the incident happened."
                      
                    />
                  </div>
                  <div className="flex flex-col gap-2 text-left">
                    <label htmlFor="DateOfIncident" className="font-semibold">
                      Date of Incident
                    </label>
                    <input
                      {...register("DateOfIncident")}
                      
                      type="datetime-local"
                      
                      className="border-gray-300 rounded"
                      placeholder="When the incident happened."
                     
                    />
                  </div>
                  <div className="flex justify-end gap-4 p-3">
                    <button
                      type="submit"
                      className="p-2 bg-blue-500 text-white rounded font-semibold"
                    >
                      Save
                    </button>
                    <button
                      className="p-2 bg-red-500 text-white rounded font-semibold"
                      onClick={() => {toggleModal()}}
                    >
                      Close
                    </button>
                  </div>
                </form>
              </div>
    </>
  )
}

