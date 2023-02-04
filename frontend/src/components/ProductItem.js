import React, {useContext} from 'react';
import '../styles/App.css';
import StyledButton from '../components/UI/button/StyledButton'
import { useHistory } from 'react-router-dom';
import { UserContext } from '../context/UserContext';

const ProductItem = ({product, removeProduct, addToOrder}) => {
    const {user, setUser} = useContext(UserContext);
    const router = useHistory()
    return(
        <div className='productItem'>
            <div>
                <strong>{product.id}. {product.name}</strong>
                <div>{product.price} $</div>
                <div>{product.description}</div>
                <div>Quantity: {product.quantity}</div>
            </div>
            <div className='column'>
                {user.role === 1 && <div>
                    <StyledButton onClick={()=>{removeProduct(product.id)}}>Delete</StyledButton>
                    <StyledButton  onClick={()=>router.push('/editProduct/'+product.id)}>Edit</StyledButton>
                </div>}
                <StyledButton onClick={()=>{()=>router.push('/productDetails/'+product.id)}}>Delails</StyledButton>
                {user.role === 0 && <div>
                    <StyledButton onClick={()=>addToOrder(product.id)}>Add to order</StyledButton>
                </div>}
            </div>
        </div>
    )
}

export default ProductItem