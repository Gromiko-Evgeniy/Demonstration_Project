import React, {useState} from 'react';


const StyledSelect = ({options, defaultValue, value, onChange}) => {

    const [type, setType] = useState('')

    return (
        <select
            value={value}
            onChange={event => onChange(event.target.value)}
        >
            <option disabled value=''>{defaultValue}</option>
            {options.map(option =>
                <option key={option.value} value={option.value}>
                    {option.name}
                </option>
            )}
        </select>
    );
};

export default StyledSelect;

//Choose product type