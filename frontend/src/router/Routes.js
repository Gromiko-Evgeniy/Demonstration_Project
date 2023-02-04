import ProductsPage from "../pages/ProductsPage"
import EditProduct from "../pages/admin/EditProduct"
import LoginPage from "../pages/LoginPage"
import OrdersPage from "../pages/customer/OrdersPage"

export const adminRoutes = [
    {path: '/editProduct/:id', component: EditProduct}
]

export const customerRoutes = [
    {path: '/customerOrders', component: OrdersPage},  
]
export const commonRoutes = [
    {path: '/products', component: ProductsPage},
    {path: '/login', component: LoginPage},
    {path: '/productDetails/:id', component: LoginPage}, //детали продукта
]

