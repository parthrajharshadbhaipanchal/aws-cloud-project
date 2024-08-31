
import { useEffect, useState } from "react";
import axios from "axios";
import ApplicationTable from "../components/ApplicationsTable";

export default function Home() {
    const [jobApplicationList, setJobApplicationList] = useState([]);

    useEffect(() => {
        fetchJobApplicationList();
    }, []);

    // Define the async function inside the effect
    async function fetchJobApplicationList() {
        try {
            const response = await axios.get(`${process.env.REACT_APP_BACKEND_URL}/`);
            setJobApplicationList(response.data); // Update state with the fetched data
        } catch (error) {
            console.error("Error fetching job application list:", error);
        }
    }

    return (
        <div className="container-fluid col-12 mt-3 shadow rounded p-2">
            <ApplicationTable data={jobApplicationList} />
        </div>
    );
}
