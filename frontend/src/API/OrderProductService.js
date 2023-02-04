import axios from 'axios';

export default class OrderProductService {
    static async addProductToOrder(productId, orderId, quantity){
        const response = await axios.post('https://localhost:7118/OrderProduct', 
        {
            'productId' : productId,
            'orderId' : orderId,
            'quantity' : quantity,
        }, { withCredentials: true })
        if(response.data.success === false) throw new Error(response.data.message)
        return response.data
    }

    static async getAllOrderProducts(){
        const response = await axios.get('https://localhost:7118/OrderProduct',{withCredentials: true})
        if(response.data.success === false) throw new Error(response.data.message)
        return response.data
    }
}
