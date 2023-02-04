import React, {useState, useEffect} from 'react';
import '../styles/App.css';
import StyledButton from './UI/button/StyledButton';
import StyledInput from './UI/input/StyledInput'
import TypeSelect from './UI/select/TypeSelect';

const ProductForm = ({onSubmit, types, title, defaultProduct}) => {
    
    useEffect(()=>{
        setProduct(defaultProduct)
        // console.log(defaultProduct)
    },[])

    const [product, setProduct] = useState({})

    function submitHandler() {
        var tempProduct = product;
        if(product.name === '') {tempProduct.name = 'no name'} //setProduct({...product, price: 0})}
        if(product.description === '') {tempProduct.description = 'no description'} //setProduct({...product, price: 0})}
        if(product.price === '') {tempProduct.price = 0} //setProduct({...product, price: 0})}
        if(product.discount === '') {tempProduct.discount = 0} //setProduct({...product, discount: 0})}
        if(product.quantity === '') {tempProduct.quantity = 0} //setProduct({...product, quantity: 0})}
        if(product.productTypeId === 0) {tempProduct.productTypeId = 1} //setProduct({...product, quantity: 0})}
        onSubmit(tempProduct)

        // setProduct({name: '',    
        //     description: '',
        //     price : '',
        //     discount : '',
        //     quantity : '',
        //     productTypeId : 0
        // }) 
    }

    return(
        <div>
            <h1 style={{textAlign: 'center'}}>{title}</h1>
            <StyledInput type='text' placeholder='New product name'
                value = {product.name || ''} 
                onChange = {e=>setProduct({...product, name: e.target.value})}
            />
            <StyledInput type='text' placeholder='New product description'
                value = {product.description || ''} 
                onChange = {e=>setProduct({...product, description: e.target.value})}
            />
            <StyledInput type='number' placeholder='New product price'
                value = {product.price || ''}
                onChange = {e=>setProduct({...product, price: parseInt(e.target.value)})}
            />
            <StyledInput type='number' placeholder='New product discount'
                value = {product.discount || ''}
                onChange = {e=>setProduct({...product, discount: e.target.value})}
            />
            <StyledInput type='number' placeholder='New product quantity'
                value = {product.quantity || ''}
                onChange = {e=>setProduct({...product, quantity: e.target.value})}
            />

            <TypeSelect onChange={(id)=>setProduct({...product, productTypeId: id})}
                defaultValue={'Choose product type'} types = {types} chosen = {defaultProduct.productTypeId}/><br/>

            <StyledButton onClick = {submitHandler}>Submit</StyledButton>
        </div> 
    )
}

export default ProductForm