import React, {useContext} from 'react';
import { Redirect, Route, Switch } from 'react-router-dom';
import { adminRoutes, customerRoutes, commonRoutes } from '../router/Routes';
import { UserContext } from '../context/UserContext';
const AppRouter = () => {
    const {user, setUser} = useContext(UserContext);

    return(
        <Switch>
            {commonRoutes.map(route=> 
                <Route path={route.path} component = {route.component} key={route.path}/>)
            }
            {user.role === 1 &&
            adminRoutes.map(route=> 
                <Route path={route.path} component = {route.component} key={route.path}/>)
            }
            {user.role === 0 &&
            customerRoutes.map(route=> 
                <Route path={route.path} component = {route.component} key={route.path}/>)
            }
            <Redirect to={'/products'}/>
        </Switch>
    )
}

export default AppRouter