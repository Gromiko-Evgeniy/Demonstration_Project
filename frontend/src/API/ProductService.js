import axios from 'axios';

export default class ProductService {
    static async getAllProducts(){
        const response = await axios({
            method: 'get',
            url: 'https://localhost:7118/Product',
            withCredentials: true 
        })
        
        if(response.data.success === false) throw new Error(response.data.message)
        return response.data
    }

    static async getAllTypes(){
        const response = await axios.get('https://localhost:7118/ProductType', { withCredentials: true })
        if(response.data.success === false) throw new Error(response.data.message)
        return response.data
    }

    static async postProduct(product){
        const response = await axios.post('https://localhost:7118/Product', product, { withCredentials: true })
        if(response.data.success === false) throw new Error(response.data.message)
        return response.data
    }

    static async updateProduct(product){
        const response = await axios.put('https://localhost:7118/Product/'+product.id, product, { withCredentials: true })
        if(response.data.success === false) throw new Error(response.data.message)
        return response.data
    }

    static async deleteProduct(id){
        const response = await axios.delete('https://localhost:7118/Product/'+id, { withCredentials: true })
        if(response.data.success === false) throw new Error(response.data.message)
        return response.data
    }
    
    static async getSingleProduct(id){
        const response = await axios.get('https://localhost:7245/Product/'+id, { withCredentials: true })
        if(response.data.success === false) throw new Error(response.data.message)
        return response.data
    }
}