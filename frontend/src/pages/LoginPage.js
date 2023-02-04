import React, {useContext, useState} from 'react';
import StyledButton from '../components/UI/button/StyledButton';
import StyledInput from '../components/UI/input/StyledInput';
import { UserContext } from '../context/UserContext';
import LoginService from '../API/LoginService';
import { useFetching } from '../hooks/useFetching';
import { useHistory } from 'react-router-dom';

const LoginPage = () => {
    const {user, setUser} = useContext(UserContext);
    const [loginData, setLoginData] = useState({email: '', password: ''})
    const router = useHistory()

    const login = async (event) => {
        event.preventDefault();
        await authorize([loginData.email, loginData.password])

        if(user !== undefined){
            router.push('/products')
        }
    }

    const [authorize, authorizingLoading, authorizeError] = useFetching(async (email, password)=>{
        const response = await LoginService.authorize(email, password);
        console.log(response)
        await setUser(response.data)
    })


    return (
        authorizingLoading ? <h1> Loading ...</h1> :
        <div>
            { authorizeError !== '' && <h1>{authorizeError}</h1>}
            <h1>Log in</h1>
            <StyledInput onChange={e=>setLoginData({...loginData, email: e.target.value })}
                value={loginData.email}  type="text" placeholder="Email"
            />
            <StyledInput onChange={e=>setLoginData({...loginData, password: e.target.value })}
                value={loginData.password} type="password" placeholder="Password"
            />
            <StyledButton onClick={login} >Submit</StyledButton>
        </div>    
    );
};

export default LoginPage;