import React, {useState, useEffect, useContext} from 'react'
import ProductItem from '../../components/ProductItem'
import StyledButton from '../../components/UI/button/StyledButton'
import { UserContext } from '../../context/UserContext'
import { useFetching } from '../../hooks/useFetching'
import { useHistory } from 'react-router-dom'
import OrderProductService from '../../API/OrderProductService'
import OrderService from '../../API/OrderService'
import StyledInput from '../../components/UI/input/StyledInput'
import Modal from '../../components/UI/modal/Modal'

const OrdersPage = () => {

    useEffect(()=>{
        getOrderProducts()
    }, [])

    const router = useHistory()
    const {user, setUser} = useContext(UserContext);
    const [orderProducts, setOrderProducts] = useState([])
    const [visible, setVisible] = useState(false)
    const [address, setAddress] = useState('')

    const [getOrderProducts, orderProductsLoading, orderProductsError] = useFetching(async ()=>{
        const response = await OrderProductService.getAllOrderProducts()
        await setOrderProducts(response.data)
    })
    const [createOrder, createOrderLoading, createOrderError] = useFetching(async (order)=>{
        const response = await OrderService.postOrder(order);
        setUser({...user, orders: response.data})
    })
    const [deleteOrder, deleteOrderLoading, deleteOrderError] = useFetching(async (id)=>{
        const response = await OrderService.deleteOrder(id);
        setUser({...user, orders: response.data})
    })
    const [payOrder, payOrderLoading, payOrderError] = useFetching(async (orderId, userId)=>{
        const response = await OrderService.payOrder(orderId, userId);
        setUser({...user, orders: response.data})
    })
    const [updateOrder, updateOrderLoading, updateOrderError] = useFetching(async (product)=>{//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        const response = await OrderService.postOrder(product);
    })

    const getProductsOfOrder = (orderId)=>{
        var products = []
        orderProducts.filter(op => op.orderId == orderId)
            .forEach(op => products.push({...op.product, quantity: op.productQuantity}))
        return products
    }

    const createOrderHandler = ()=>{
        createOrder([{deliveryAddress: address, userId: user.id}])
        setVisible(false)
    }
    const updateOrderHandler = ()=>{
        //request
    }
    
    
    return(
        <div className='column'>
            {user.orders.map(order=> 
                <div key={order.id} style={{border: "3px solid teal", padding: '35px', marginTop: '20px'}}>
                    <h1 style={{textAlign: 'center'}}>Order № {order.id}</h1>
                    {getProductsOfOrder(order.id).map(product=>
                        <ProductItem key={product.id} product={product}
                        addToOrder={() => console.log('add 1 to product quantity (new add request)')}/>)
                    }

                    <h3>Total price: {order.totalPrice}</h3>
                    <h3>Paid: {order.paid.toString()}</h3>
                    {!order.payd && <StyledButton onClick={()=>payOrder([order.id, user.id])}>Pay</StyledButton>}

                    <StyledButton onClick={()=>deleteOrder([order.id])}>Delete</StyledButton>
                    {/* <div style={{display: 'flex', flexDirection: 'row'}}>
                    <StyledInput value={order.deliveryAddress}
                        onChange={()=>{}}/>
                    <StyledButton onClick={updateOrderHandler}>Save address</StyledButton>
                    </div> */}
                </div>)
            }
            <StyledButton style={{marginTop: '40px'}} onClick={()=>router.push('/products')}>Back to products</StyledButton>
            <StyledButton onClick={()=>setVisible(true)}>Create new order</StyledButton>

            <Modal visible={visible} setVisible={setVisible}>           
                <StyledInput value={address} onChange={e=>setAddress(e.target.value)}/>
                <StyledButton onClick={createOrderHandler}>Add new order</StyledButton>
            </Modal>
        </div>
    )
}

export default OrdersPage