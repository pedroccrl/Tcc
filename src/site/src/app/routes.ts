import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CidadeComponent } from './cidade/cidade.component';

export const routes: Routes = [
    {
        path: '',
        component: HomeComponent
    },
    {
        path: 'cidade/:cidadeId',
        component: CidadeComponent
    }
]