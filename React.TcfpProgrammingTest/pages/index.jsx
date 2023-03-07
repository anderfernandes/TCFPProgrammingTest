import Head from "next/head";
import { useState } from "react";
import InjuryReportForm from '../components/InjuryReportForm';
import DeleteConfirmation from '../components/DeleteConfirmation';
import Modal from '../components/Modal'
import View from '../components/View'

const Home = () => {
  /**
   * An array of Fire Departments. Do not modify!
   */
  const departments = [
    { Id: 1, Name: "Austin Fire Department" },
    { Id: 2, Name: "Round Rock Fire Department" },
    { Id: 3, Name: "Georgetown Fire Department" },
    { Id: 4, Name: "Pflugerville Fire Department" },
  ];

  // Here is where you will store the reports.
  const [reports, setReports] = useState([
    {
      Id: 1,
      DepartmentId: 1,
      Department: departments[0],
      CreatedAt: new Date(2022, 1, 15, 20, 12, 15),
      Status: "Closed",
      DateOfIncident: new Date(2022, 1, 13, 10, 0, 0),
      Name: "Injured left index finger during training",
      Description:
        "Injury to left index finger at knuckle. Occurred while participating in forcible entry training exercises at fire academy.",
      Location: "123 Main St Austin, Texas",
      Type: "Training",
    },
    {
      Id: 2,
      DepartmentId: 2,
      Department: departments[1],
      CreatedAt: new Date(2022, 4, 14, 17, 44, 36),
      Status: "Pending",
      DateOfIncident: new Date(2022, 4, 14, 15, 30, 0),
      Name: "Vehicle Accident",
      Description: "Crashed car during training",
      Location: "Round Rock, Texas",
      Type: "Accident",
    },
    {
      Id: 3,
      DepartmentId: 1,
      Department: departments[2],
      CreatedAt: new Date(2023, 2, 14, 17, 44, 36),
      Status: "Open",
      DateOfIncident: new Date(2023, 2, 14, 15, 30, 0),
      Name: "Slipped and fell off a roof",
      Description: "Slipped and fell off a roof",
      Location: "Interstate 35, Georgetown, Texas",
      Type: "Other",
    },
  ]);



  // Here is a state for you to keep track the report the user has selected.
  const [selected, setSelected] = useState();

  // This state conrols the modal.
  const [showModal, setShowModal] = useState(false);
 
  // Conditionally render deleteConfirmation Component
  const [showDeleteConfirmation, setShowDeleteConfirmation] = useState(false);

  // Conditionally render View Component
  const [showView, setShowView] = useState(false);

  // Could be `view`, `create`, `edit` or `delete`
  const [mode, setMode] = useState("create");

 

  /**
   * Toggles the modal.
   */
  const toggleModal = () => setShowModal(!showModal);
  const toggleDeleteConfirmation = () => setShowDeleteConfirmation(!showDeleteConfirmation);
  const toggleViewModal = () => setShowView(!showView);
  // TODO: Prepare the application to receive a new Injury Report.
  const onNewReportClick = () => {
    // Set the mode to create, clear the selected report and toggle the modal.
    setMode("create");
    setSelected(null);
    toggleModal();
  };

  // TODO: When you click on the report, set the report you clicked on as the selected one and change the
  // mode to view.
  const onReportClick = (r) => {
    // Set the selected report, change mode to view and toggle modal.
    setSelected(r);
    setMode("view");
    toggleViewModal();
  };

  //Added to handle conditionally rendering InjuryReportForm
  const onEditClick = (r) => {
    setSelected(r);
    setMode("edit");
    toggleModal();
  }

  

  // TODO: Ask the user if they are sure they want to remove reports beforehand.
  const onDeleteReport = (r) => {
    setMode("delete");
    setSelected(r);
    toggleDeleteConfirmation();
    toggleModal();
  };

  const props = {toggleModal, mode, selected, setSelected, departments, reports, setReports,
  showDeleteConfirmation, setShowDeleteConfirmation, toggleDeleteConfirmation, toggleViewModal, setMode }

  /* Return components with props to conditionally rrender in Modal later */
  const renderInjuryReportForm = () => {
    return <InjuryReportForm {...props} />
  }

  const renderDeleteConfirmation = () => {
    return <DeleteConfirmation {...props} />
  }

  const renderView = () => {
    return <View {...props} />
  }

  return (
    <div className="flex min-h-screen flex-col items-center justify-center py-2">
      <Head>
        <title>Injury Reports | Texas Commission on Fire Protection</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <main className="flex w-full flex-1 flex-col px-20 text-center gap-4">
        <h1 className="text-4xl font-bold my-3">Injury Reports</h1>
        <div className="flex justify-center">
          <button
            className="p-2 bg-black font-semibold text-white rounded"
            onClick={onNewReportClick}
          >
            New Report
          </button>
        </div>
        <table className="table-auto w-full border-collapse ">
  <thead>
    <tr className="border-b-2 border-gray-300">
      <th className="px-4 py-2">#</th>
      <th className="px-4 py-2">Department</th>
      <th className="px-4 py-2">Status</th>
      <th className="px-4 py-2">Type</th>
      <th className="px-4 py-2">Created At</th>
      <th className="px-4 py-2">Actions</th>
    </tr>
  </thead>
  <tbody>
    {/* TODO: Loop through the reports and display them along with view, edit and delete buttons. */}
    {reports.map((report) => (
      <tr key={report.Id} className="border-b border-gray-200 hover:bg-gray-100">
        <td className="px-4 py-2">{report.Id}</td>
        <td className="px-4 py-2">{report.Department.Name}</td>
        <td className="px-4 py-2">{report.Status}</td>
        <td className="px-4 py-2">{report.Type}</td>
        <td className="px-4 py-2">{report.CreatedAt.toLocaleString()}</td>
        <td className="px-4 py-2">
          <div className="flex justify-center gap-3 my-2">
            <button className="p-1 font-semibold text-white rounded bg-blue-500 hover:bg-blue-600" onClick={() => onReportClick(report)}>View</button>
            <button className="p-1 font-semibold text-white rounded bg-slate-900 hover:bg-slate-600" onClick={() => onEditClick(report)}>Edit</button>
            <button className="p-1 font-semibold text-white rounded bg-red-500 hover:bg-red-600" onClick={() => onDeleteReport(report)}>Delete</button>
          </div>
        </td>
      </tr>
    ))}
  </tbody>
</table>

        {showModal && (
          <Modal 
          injuryReportForm={renderInjuryReportForm}
          deleteConfirmation={renderDeleteConfirmation}
          view={renderView}
          showDeleteConfirmation={showDeleteConfirmation}
          showView={showView}/>
        ) || showView && (
          <Modal 
          injuryReportForm={renderInjuryReportForm}
          deleteConfirmation={renderDeleteConfirmation}
          view={renderView}
          showDeleteConfirmation={showDeleteConfirmation}
          showView={showView}/>
        )}

      </main>

      <footer className="flex h-24 w-full items-center justify-center border-t">
        <a
          className="flex items-center justify-center gap-2"
          href="https://vercel.com?utm_source=create-next-app&utm_medium=default-template&utm_campaign=create-next-app"
          target="_blank"
          rel="noopener noreferrer"
        >
          &copy; {new Date().getFullYear()} Texas Commission on Fire Protection
        </a>
      </footer>
    </div>
  );
};

const Label = ({ children, type }) => {
  const classes = ["text-xs p-1 border rounded font-bold"];

  if (type === "success")
    classes.push("bg-green-300 text-green-600 border-green-300");
  else if (type === "warning")
    classes.push("bg-yellow-300 text-yellow-600 border-yellow-300");
  else if (type === "error")
    classes.push("bg-red-300 text-red-600 border-red-300");
  else classes.push("bg-white text-black border-black");

  return <span className={classes.join(" ")}>{children}</span>;
};

export default Home;
