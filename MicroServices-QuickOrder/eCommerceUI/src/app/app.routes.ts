import { OrdersComponent } from './Pages/orders/orders.component';
import { Routes } from '@angular/router';
import { HomeComponent } from './Layout/home/home.component';
import { LoginComponent } from './Auth/login/login.component';
import { ItemDetailsComponent } from './Pages/item-details/item-details.component';
import { RegisterComponent } from './Auth/register/register.component';
import { CartComponent } from './Pages/cart/cart.component';

export const routes: Routes = [
   { path: '', component: HomeComponent },
   { path: 'login', component: LoginComponent },
   { path: 'product/:id', component: ItemDetailsComponent },
   {path: 'register', component:RegisterComponent},
   {path: 'cart', component:CartComponent},
   {path: 'orders', component:OrdersComponent},
  // Optional: redirect unknown paths
  { path: '**', redirectTo: '' }

];
