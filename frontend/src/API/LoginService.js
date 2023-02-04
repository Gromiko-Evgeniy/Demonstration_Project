import axios from 'axios';

export default class LoginService {

    static async authorize(email, password){
        const response = await axios.post('https://localhost:7118/LogIn', 
        {
            "email": email,
            "password": password
        }, { withCredentials: true })
        if(response.data.success === false) throw new Error(response.data.message)
        return response.data
    }

    static async logOut(){
        const response = await axios.get('https://localhost:7118/LogIn', { withCredentials: true })
        if(response.data.success === false) throw new Error(response.data.message)
        return response.data
    }
}