import { Component, OnInit, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'cidade',
    templateUrl: './cidade.component.html',
    styleUrls: ['cidade.component.css'],
})
/** cidade component*/
export class CidadeComponent implements OnInit
{
    public cidade: Cidade;
    public assuntos: Assunto;
    public geocomentarios: GeoComentario[];
    lat: number = -22.4699587;
    lng: number = -42.0113264;

    /** cidade ctor */
    constructor(http: Http, route: ActivatedRoute, @Inject('BASE_URL') baseUrl: string) {
        var nome: string;
        
        route.queryParams.subscribe(params => {
            nome = params['cidade'];
            
            http.get(baseUrl + 'api/Cidade/' + nome).subscribe(result => {
                this.cidade = result.json() as Cidade;
            }, error => console.error(error));

            http.get(baseUrl + 'api/Assuntos/' + nome).subscribe(result => {
                this.assuntos = result.json() as Assunto;
            }, error => console.error(error));

            http.get(baseUrl + 'api/GeoComentarios/' + nome).subscribe(result => {
                this.geocomentarios = result.json() as GeoComentario[];
            }, error => console.error(error));
        });
    }

    /** Called by Angular after cidade component initialized */
    ngOnInit(): void { }
}

interface Cidade {
    cidade: string;
    bairros: number;
    logradouros: number;
    paginas: number;
    posts: number;
    comentarios: number;
}

interface Assunto {
    temas: Mencao[];
    qualidades: Mencao[];
}

interface Mencao {
    key: string;
    value: number;
}

export interface Bairro {
    nomeAlternativo: string;
    nome: string;
    cidade: string;
}

export interface Logradouro {
    idBairro: number;
    nome: string;
    cep: string;
    longitude: number;
    _id: number;
    latitude: number;
    tipo: string;
    bairro: Bairro;
    idCidade: number;
}

export interface GeoComentario {
    comentario: string;
    id_comentario: number;
    logradouro: Logradouro[];
}




