import { Routes } from '@angular/router';
import { Register } from './features/auth/register/register';
import { Login } from './features/auth/login/login';
import { Profile } from './features/profile/profile/profile';
import { ExpenseComponent } from './features/expense/expense/expense';
import { CategoryComponent } from './features/category/category/category';
import { DashboardComponent } from './features/dashboard/dashboard/dashboard';
import { ReportsComponent } from './features/reports/reports/reports';
import { BudgetComponent } from './features/budget/budget/budget';

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
  path: 'reports',
  component: ReportsComponent
},
{
  path: 'budget',
  component: BudgetComponent
},
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  }
 
];