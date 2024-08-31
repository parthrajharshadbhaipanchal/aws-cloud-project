import { useRef, useState } from "react";
import { useNavigate } from 'react-router-dom';
import axios from "axios";

export default function CreateJobApplicationForm() {
    const navigate = useNavigate();

    const titleRef = useRef(null);
    const jobPostLinkRef = useRef(null);
    const descriptionRef = useRef(null);
    const resumeRef = useRef(null);

    const createApplicationURL = "/create"

    async function handleOnSubmit() {
        let jobApplication = {
            JobTitle: titleRef.current.value,
            JobDescription: descriptionRef.current.value,
            Resume: resumeRef.current.files[0] ? resumeRef.current.files[0].name : null,
            JobPostLink: jobPostLinkRef.current.value,
            Status: "Applied"
        }

        const formData = new FormData();
        formData.append('file', resumeRef.current.files[0]);
        formData.append('jobApplication', JSON.stringify(jobApplication));

        await axios.post(`${process.env.REACT_APP_BACKEND_URL}${createApplicationURL}`, formData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            }
        }).then((response) => {
            if (response.data.isSuccess) {
                alert("Application created successfully!")
                navigate('/');
            } else {
                alert("Unable to save application. Please try again.")
            }
        });
    }

    return (
        <>
            <div className="container col-6 m-4 border rounded shadow p-4">
                <div>
                    <span className="display-6">New job application</span>
                </div>
                <hr />
                <form>
                    <div className="mb-3">
                        <label className="form-label">Title:</label>
                        <input type="text" ref={titleRef} className="form-control"></input>
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Link to job post:</label>
                        <input type="text" ref={jobPostLinkRef} className="form-control"></input>
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Description:</label>
                        <textarea ref={descriptionRef} className="form-control"></textarea>
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Resume:</label>
                        <input type="file" ref={resumeRef} className="form-control"></input>
                    </div>
                    <div>
                        <button type="button" onClick={handleOnSubmit} className="btn btn-md btn-primary">Submit</button>
                    </div>

                </form>
            </div>
        </>
    );
}