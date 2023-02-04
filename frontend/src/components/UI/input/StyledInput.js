import React from 'react';
import styleClasses from './StyledInput.module.css'


const StyledInput = (props) => {
    return (
        <input className={styleClasses.Input} {...props}/>
    );
};

export default StyledInput;