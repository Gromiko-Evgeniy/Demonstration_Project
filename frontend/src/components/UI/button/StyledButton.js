
import React from 'react';
import styleClasses from './StyledButton.module.css'
const StyledButton = ({children, ...props}) => {
    return(
        <button {...props} className={styleClasses.Btn}>
            {children}
        </button>
    )
}

export default StyledButton