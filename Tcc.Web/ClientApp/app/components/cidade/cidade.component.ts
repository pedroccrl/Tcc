import { Component, OnInit, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'cidade',
    templateUrl: './cidade.component.html',
})
/** cidade component*/
export class CidadeComponent implements OnInit
{
    public cidade: any;
    /** cidade ctor */
    constructor(http: Http, route: ActivatedRoute, @Inject('BASE_URL') baseUrl: string) {
        var nome: string;
        route.queryParams.subscribe(params => {
            nome = params['cidade'];
            http.get(baseUrl + 'api/Cidade/' + nome).subscribe(result => {
                this.cidade = result.json();
            }, error => console.error(error));
        });
    }

    /** Called by Angular after cidade component initialized */
    ngOnInit(): void { }
}