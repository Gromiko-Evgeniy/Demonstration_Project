import React from 'react';
import ProductItem from './ProductItem';
import '../styles/App.css';

const ProductList = ({products, removeProduct, addToOrder}) => {

    if(!products.length){
        return( <h1 style={{textAlign: 'center'}}>No products yet</h1> )
    }

    return(
        <div>  
            <h1 style={{textAlign: 'center'}}>Products: </h1>

            {products.map( p => 
                <ProductItem addToOrder={addToOrder} removeProduct = {removeProduct} key = {p.id} product = {p}  />
            )}
        </div>
    )
}

export default ProductList