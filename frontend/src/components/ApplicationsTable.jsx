import { useEffect, useState } from "react";
import axios from "axios";

export default function ApplicationTable(data) {
    const [tableData, setTableData] = useState([]);

    useEffect(() => {
        setTableData(data.data);
    }, [data]);

    function downloadResumeFile(id) {
        axios.get(`${process.env.REACT_APP_BACKEND_URL}/download_resume?id=${id}`, {
            responseType: 'blob' // Important: Set responseType to 'blob' for binary data
        })
            .then(response => {
                const url = window.URL.createObjectURL(new Blob([response.data]));
                const link = document.createElement('a');
                link.href = url;
                link.setAttribute('download', `resume_${id}.pdf`); // Replace with appropriate filename
                document.body.appendChild(link);
                link.click();
            })
            .catch(error => {
                console.error('Error downloading resume:', error);
            });
    }


    const setJobApplicationTableData = () => {
        return tableData.map(
            (row, index) => {
                return (
                    <tr key={index}>
                        <td>{index + 1}</td>
                        <td>{row.jobTitle}</td>
                        <td>{row.jobDescription}</td>
                        <td>{row.status}</td>
                        <td>{row.createdAt}</td>
                        <td>
                            <i className="bi bi-file-earmark-arrow-down-fill text-secondary" onClick={() => downloadResumeFile(row.id)} style={{ fontSize: '1.7em' }}></i>
                        </td>
                        <td><a href={row.jobPostLink}><button type="button" className="btn btn-sm btn-primary">Link</button></a></td>
                    </tr>
                )
            });
    }
    return (
        <div>
            <label className="display-5">Job applications</label>
            <table className="table table-bordered table-hover mt-3">
                <thead>
                    <tr>
                        <th>No</th>
                        <th>Job post</th>
                        <th>Description</th>
                        <th>Status</th>
                        <th>Created</th>
                        <th>Resume</th>
                        <th>Link</th>
                    </tr>
                </thead>
                <tbody>
                    {setJobApplicationTableData()}
                </tbody>
            </table>
        </div>
    );
}