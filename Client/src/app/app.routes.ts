import {HomeComponent} from './pages/home/home.component';

export const appRoutes = [
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full'
    },
    {
        path: 'home',
        component: HomeComponent
    },
    {
        path: 'add',
        loadChildren: './pages/add/add.module#AddModule',
    },
    {
        path: 'delete/:id',
        loadChildren: './pages/delete/delete.module#DeleteModule',
    },
    {
        path: 'robot/:id',
        loadChildren: './pages/robot/robot.module#RobotModule',
    },
];
