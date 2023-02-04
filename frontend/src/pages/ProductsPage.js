import React, {useState, useEffect, useMemo, useContext} from 'react';
import ProductForm from '../components/ProductForm';
import ProductList from '../components/ProductList';
import ProductService from '../API/ProductService';
import OrderProductService from '../API/OrderProductService';
import FilterForm from '../components/FilterForm';
import Modal from '../components/UI/modal/Modal';
import StyledButton from '../components/UI/button/StyledButton';
import { useFetching } from '../hooks/useFetching';
import { ProductsContext} from '../context/ProductsContext';
import { UserContext } from '../context/UserContext';
import { useHistory } from 'react-router-dom';


const ProductsPage = () => {

    useEffect(()=>{
        getAllProducts()
        getAllTypes()
        console.log(user)
    }, [])


    const [types, setTypes] = useState([]);
    const [filter, setFilter] = useState({sorting: '', query: ''}); 
    const [modalVisible, setVisible] = useState({addProduct: false, addToOrder: false});
    const [productId, setProductId] = useState(0);

    const router = useHistory()

    const {user, setUser} = useContext(UserContext);
    const {products, setProducts} = useContext(ProductsContext)


    const [getAllProducts, isProductsLoading, fetchProductsError ] = useFetching(async ()=>{
        const productResponse = await ProductService.getAllProducts();
        if (productResponse !== null) setProducts(productResponse.data) // !== null не нужно, есть fetchProductsError
    })
    const [getAllTypes, isTypesLoading, fetchTypesError ] = useFetching(async ()=>{
        const typeResponse = await ProductService.getAllTypes();
        if (typeResponse !== null) setTypes(typeResponse.data)
    })
    const [postProduct, postingLoading, postError] = useFetching(async (product)=>{
        const newProduct = await ProductService.postProduct(product);
    })
    const [deleteProduct, deleteLoading, deleteError] = useFetching(async (id)=>{
        const products = await ProductService.deleteProduct(id);
    })
    // const [authorize, authorizingLoading, authorizeError] = useFetching(async (email, password)=>{
    //     const user = await ProductService.authorize(email, password);
    // })
    const [addProductToOrder, addProductToOrderLoading, addProductToOrderError] = useFetching(async (productId, orderId, quantity)=>{ //remove/ new one OrderProductService
        await OrderProductService.addProductToOrder(productId, orderId, quantity)
    })
    
    async function  addProduct(product){
        await postProduct([product])
        await getAllProducts()
        setVisible({...modalVisible, addProduct: true})
    }

    function  removeProduct(id){
        setProducts(products.filter(p=>p.id !== id))
        deleteProduct([id])
    }

    async function  addToOrder(orderId){
        addProductToOrder([productId, orderId, 1]) //quantity field need to be added
        router.push('/customerOrders')
    }

    const sortedProducts = useMemo(()=>{
        if(filter.sorting === '') return products
        if(filter.sorting === 'price' || filter.sorting === 'discount'){
            return([...products].sort((a,b)=>a[filter.sorting]-b[filter.sorting]))
        }
        return([...products].sort((a,b)=>a[filter.sorting].localeCompare(b[filter.sorting])))
    }, [filter.sorting, products])
    const searchedAndSortedProducts = useMemo(()=>{
        return(sortedProducts.filter(product=> product.name.toLowerCase().includes(filter.query.toLowerCase())))
    }, [filter.query, sortedProducts])

    return(
        <div style={{width: '900px'}}>
            {user.role !== 1 || fetchTypesError !== '' ? <h1 style={{textAlign: 'center'}}>{fetchTypesError} </h1> :
            isTypesLoading ? <h1 style={{textAlign: 'center'}}>Loading... </h1> : 
                <div>
                    <StyledButton onClick ={()=>setVisible({...modalVisible, addProduct: true})}>Add Product</StyledButton> 
                    <Modal visible={modalVisible.addProduct} setVisible={visible=> setVisible({...modalVisible, addProduct: visible})}>           
                        <ProductForm types = {types} onSubmit = {addProduct} title = 'Add product' defaultProduct={{
                            id : '',
                            name: '',
                            description: '',
                            price : '',
                            discount : '',
                            quantity : '',
                            productTypeId : 0
                        }}/>
                    </Modal>
                </div>
            }

            {user.role === 0 && <div>
                    <Modal visible={modalVisible.addToOrder} setVisible={visible=> setVisible({...modalVisible, addToOrder: visible})}>           
                        {user.orders.map(order=>
                            !order.paid && <StyledButton key={order.id} onClick={()=>addToOrder(order.id)}>Order {order.id}</StyledButton>)}
                        {/* <StyledButton>New Order</StyledButton> */}
                    </Modal>
                </div>
            }

            {fetchProductsError !== '' ? <h1 style={{textAlign: 'center'}}>{fetchProductsError} </h1> :
            isProductsLoading ? <h1 style={{textAlign: 'center'}}>Loading... </h1> : 
                <div>
                    <FilterForm filter={filter} setFilter = {setFilter}/>
                    <ProductList addToOrder={(id)=>{
                        setVisible({...modalVisible, addToOrder: true})
                        setProductId(id)
                    }}
                        removeProduct = {removeProduct} products = {searchedAndSortedProducts}
                    />
                </div> 
            }
        </div>
    )
}

export default ProductsPage