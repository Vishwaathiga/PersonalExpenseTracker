import { Routes } from '@angular/router';
import { Register } from './features/auth/register/register';
import { Login } from './features/auth/login/login';
import { Profile } from './features/profile/profile/profile';
import { ExpenseComponent } from './features/expense/expense/expense';
import { CategoryComponent } from './features/category/category/category';
import { DashboardComponent } from './features/dashboard/dashboard/dashboard';

export const routes: Routes = [
  {
    path: 'register',
    component: Register
  },
  {
    path: 'login',
    component: Login
  },
  {
    path: 'profile',
    component: Profile
  },
  {
    path: 'expenses',
    component: ExpenseComponent
  },
   {
  path: 'categories',
  component: CategoryComponent
},
{
  path: 'dashboard',
  component: DashboardComponent
},
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  }
 
];