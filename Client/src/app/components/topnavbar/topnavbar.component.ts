import { Component, OnInit } from '@angular/core';
import {smoothlyMenu} from '../../app.helpers';

@Component({
    selector: 'app-topnavbar',
    templateUrl: 'topnavbar.component.html'
})
export class TopnavbarComponent implements OnInit {

    ngOnInit() { }

    toggleNavigation(): void {
        jQuery('body').toggleClass('mini-navbar');
        smoothlyMenu();
    }
    logout() {
        localStorage.clear();
        // location.href='http://to_login_page';
    }
}
