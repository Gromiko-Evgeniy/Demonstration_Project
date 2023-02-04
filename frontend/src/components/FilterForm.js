import React, {useState} from 'react';
import StyledInput from './UI/input/StyledInput';
import StyledSelect from './UI/select/StyledSelect';

const FilterForm = ({filter, setFilter}) => {

    return(
        <div>
            <StyledSelect defaultValue='no sorting'
                onChange = {sorting =>setFilter({...filter, sorting: sorting})}
                value={filter.sorting} options = {[
                    {name: 'by name', value: 'name'}, 
                    {name: 'by price', value: 'price'},
                    {name: 'by discount', value: 'discount'}]}
            />

            <StyledInput type='text' placeholder='Search...'
                value = {filter.query} 
                onChange = {e=>setFilter({...filter, query: e.target.value})}
            />
        </div> 
    )
}

export default FilterForm