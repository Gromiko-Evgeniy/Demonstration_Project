import React, {useContext} from 'react';
import styleClasses from './Navbar.module.css'
import StyledButton from '../button/StyledButton';
import { useHistory } from 'react-router-dom';
import { UserContext } from '../../../context/UserContext';
import LoginService from '../../../API/LoginService';
import { useFetching } from '../../../hooks/useFetching';

const Navbar = ({children, visible, setVisible}) => {

    const router = useHistory()
    const {user, setUser} = useContext(UserContext);

    const logOutHandler = async ()=>{
        setUser({role: null, orders:[]})
        await logOut()
        router.push('/customerOrders')
    }

    const [logOut, logOutLoading, logOutError] = useFetching(async ()=>{
        const response = await LoginService.logOut();
        console.log(response)
    })

    return (
        <div className={styleClasses.navbar}>
            {user.role !== null ? <StyledButton onClick={logOutHandler}> Log out </StyledButton> :
                <StyledButton onClick={()=>router.push('/login')}> Log in </StyledButton>
            }
            <div className={styleClasses.navbarItem}>
                <StyledButton onClick={()=>router.push('/products')}> Products </StyledButton>
                {user.role === 0 && <StyledButton onClick={()=>router.push('/customerOrders')}> Orders </StyledButton>}
            </div>
    </div>
    );
};

export default Navbar;