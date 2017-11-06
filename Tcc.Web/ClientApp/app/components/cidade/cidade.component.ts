import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'cidade',
    templateUrl: './cidade.component.html',
})
/** cidade component*/
export class CidadeComponent implements OnInit
{
    public cidade: string;
    /** cidade ctor */
    constructor(http: Http, route: ActivatedRoute) {
        route.queryParams.subscribe(
            params => this.cidade = params['cidade']);
    }

    /** Called by Angular after cidade component initialized */
    ngOnInit(): void { }
}