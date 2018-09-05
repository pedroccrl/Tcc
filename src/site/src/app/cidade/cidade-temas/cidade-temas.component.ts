import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'app-cidade-temas',
    templateUrl: './cidade-temas.component.html',
    styleUrls: ['./cidade-temas.component.css']
})
export class CidadeTemasComponent implements OnInit {

    @Input()
    public temas;

    constructor() { }

    ngOnInit() {
    }

}
