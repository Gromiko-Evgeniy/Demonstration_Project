
import React, {useState, useEffect} from 'react';

const TypeSelect = ({types, defaultValue, onChange, chosen}) => {
    useEffect(()=>{
        setType(chosen)
        console.log(chosen)
    }
    ,[])
    const [type, setType] = useState(chosen)


    return (
        <select value={type} onChange = {e=> {onChange(parseInt(e.target.value)); setType(parseInt(e.target.value)); console.log(e.target.value)}}> 

            <option value={0} disabled> {defaultValue} </option>

            {types.map(t =>
                <option value={t.id} key = {t.id}>{t.typeName}</option>
            )}

        </select>    
    );
};

export default TypeSelect;

//Choose product type

