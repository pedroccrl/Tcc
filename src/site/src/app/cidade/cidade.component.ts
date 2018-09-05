import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-cidade',
    templateUrl: './cidade.component.html',
    styleUrls: ['./cidade.component.css']
})
export class CidadeComponent implements OnInit {
    lat: number = 51.678418;
    lng: number = 7.809007;
    constructor() { }

    ngOnInit() {
    }

}
