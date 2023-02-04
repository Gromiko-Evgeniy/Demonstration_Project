import axios from 'axios';

export default class OrderService {
    static async postOrder(order){
        const response = await axios.post('https://localhost:7118/Order', order, { withCredentials: true })
        if(response.data.success === false) throw new Error(response.data.message)
        return response.data
    }

    static async deleteOrder(id){
        console.log(id)
        const response = await axios.delete('https://localhost:7118/Order/'+id, { withCredentials: true })
        if(response.data.success === false) throw new Error(response.data.message)
        return response.data
    }

    static async payOrder(orderId, userId){
        console.log('https://localhost:7245/Order/'+orderId+'/'+userId)
        const response = await axios.put('https://localhost:7118/Order/'+orderId+'/'+userId,{} ,{ withCredentials: true })
        if(response.data.success === false) throw new Error(response.data.message)
        return response.data
    }
}
