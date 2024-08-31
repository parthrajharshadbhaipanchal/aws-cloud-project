import API from '../api';

export const getJobApplicationList=async()=>{
    console.log("calling api");
    let result;
    try{
        result=await API.get("/");
    }
    catch(error){
        result=error;
    }
    return result;
}