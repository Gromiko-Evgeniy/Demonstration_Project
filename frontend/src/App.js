import React, {useState} from 'react';
import { BrowserRouter} from 'react-router-dom';
import { ProductsContext } from './context/ProductsContext';
import { UserContext } from './context/UserContext';
import Navbar from './components/UI/navbar/Navbar';
import AppRouter from './components/AppRouter';


function App() {
  const [products, setProducts] = useState([]);
  const [user, setUser] = useState({role: null, orders:[]});

  return (
    <UserContext.Provider value={{user, setUser}}>
      <ProductsContext.Provider value={{products, setProducts}}>
        <BrowserRouter>
          <Navbar/>
          <AppRouter/>
        </BrowserRouter>
      </ProductsContext.Provider>
    </UserContext.Provider>
  );
} 

export default App;
