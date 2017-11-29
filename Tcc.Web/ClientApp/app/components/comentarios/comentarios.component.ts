import { Component, OnInit, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'comentarios',
    templateUrl: './comentarios.component.html',
})
/** comentarios component*/
export class ComentariosComponent {
    public comentarios: ComentarioOriginal[];
    /** comentarios ctor */
    constructor(http: Http, route: ActivatedRoute, @Inject('BASE_URL') baseUrl: string) {
        var cidade: string;
        route.queryParams.subscribe(params => {
            cidade = params['cidade'];

            http.get(baseUrl + 'api/Comentarios/' + cidade + '?page=2').subscribe(result => {
                this.comentarios = result.json() as ComentarioOriginal[];
            }, error => console.error(error));
        });


    }
}

export interface From {
    _id: string;
    name: string;
}

export interface Tema {
    qualidades: string[];
    nomes: string[];
    pos_tags: string[][][];
}

export interface Bairro {
    NomeAlternativo: string;
    Nome: string;
    Cidade: string;
}

export interface Logradouro {
    IdBairro: number;
    Nome: string;
    Cep: string;
    Longitude: number;
    _id: number;
    Latitude: number;
    Tipo: string;
    Bairro: Bairro;
    IdCidade: number;
}

export interface ComentarioOriginal {
    _id: string;
    created_time: string;
    from: From;
    message: string;
    like_count: number;
    comments?: any;
    IdComentario: number;
    IdRespondido?: any;
    IdCidade: number;
    IdPagina: number;
    IdPost: number;
    palavras_unk: string[];
    corrigido: string;
    palavras_unk_c: string[];
    tema: Tema;
    TemLogradouro: boolean;
    Logradouros: Logradouro[];
}
