import {Routes} from '@angular/router';
import {MainLayout} from '../components/layouts/main-layout/main-layout/main-layout';
import {Login} from '../components/main/login/login';
import {Home} from '../components/main/home/home';

export const routes: Routes = [

  {
    path: '', component: MainLayout, children: [
      {path: '', component: Home},
      {path: 'login', component: Login},
    ]
  }
];
