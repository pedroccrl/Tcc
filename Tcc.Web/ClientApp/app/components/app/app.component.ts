import { Component } from '@angular/core';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
})
export class AppComponent {

    public header: string;
    constructor() {
        this.header = "Tcc - Pedro Camara"
    }
}
