import {useState} from 'react'

export const useFetching = (callback)=>{
    const [isLoading, setLoading] = useState(false);
    const [error, setError] = useState('');
    
    const fetching = async (params = '')=> {
        try{
            setLoading(true)
            params !== '' ? await callback(...params) : await callback()
        }catch(e){
            setError('Error: ' + e.message)
            console.log(e.message)
        }finally{
            setLoading(false)
        }
    }
    return [fetching, isLoading, error]
}