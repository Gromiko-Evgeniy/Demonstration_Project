import React, {useState, useEffect, useContext} from 'react'
import { useHistory, useParams } from 'react-router-dom'
import ProductForm from '../../components/ProductForm'
import { useFetching } from '../../hooks/useFetching'
import ProductService from '../../API/ProductService'
import { ProductsContext } from '../../context/ProductsContext';


const EditProduct = () => {

    const [types, setTypes] = useState([]);
    const [product, setProduct] = useState({});
    const id = useParams().id
    const router = useHistory()
    const {products, setProducts} = useContext(ProductsContext)


    useEffect(()=>{
        getAllTypes()
        getProduct([id])

    }, [])
    
    const [getAllTypes, isTypesLoading, fetchTypesError ] = useFetching(async ()=>{
        const typeResponse = await ProductService.getAllTypes();
        setTypes(typeResponse.data)
    })
    const [getProduct, getProductLoading, getProductError] = useFetching(async (id)=>{
        const productResponse = await ProductService.getSingleProduct(id)
        setProduct(productResponse.data);
    })
    const [updateProduct, updateProductLoading, updateProductError] = useFetching(async (product)=>{
        await ProductService.updateProduct(product)
    })

    const updateProductHandler = async (product) => {
        updateProduct([product])
        console.log({type: updateProductError})
        if(updateProductError === ''){
            console.log('in')

            for(var i = 0; i<products.length; i++){
                if(products[i].id != product.id) continue;
                products[i] = product;
            }
            await setProducts(products)
            router.push('/products')
        }
    }
    
    return(
        <div>
            {fetchTypesError !== ''  ? <h1 style={{textAlign: 'center'}}>{fetchTypesError} </h1> :
            updateProductError !== ''  ? <h1 style={{textAlign: 'center'}}>{updateProductError} </h1> :
            isTypesLoading || getProductLoading ? <h1 style={{textAlign: 'center'}}>Loading... </h1> : 
                <ProductForm types = {types} onSubmit = {updateProductHandler} title = {'Edit product № '+id}
                defaultProduct={product}/>
            }
        </div>
    )
}

export default EditProduct