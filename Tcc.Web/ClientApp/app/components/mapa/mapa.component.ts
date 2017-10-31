import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'mapa',
    templateUrl: 'mapa.component.html',
    styleUrls: ['mapa.component.css'],
})
export class MapaComponent {
    title: string = 'My first AGM projasdasdasdect';
    lat: number = -22.4699587;
    lng: number = -42.0113264;

    public ruas: Rua[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/Logradouro').subscribe(result => {
            this.ruas = result.json() as Rua[];
        },
            error => console.error(error));
    }
    
}

interface Rua {
    latitude: number;
    longitude: number;
    nome: string;
    comentarios: Array<Comentario>;
}

interface Comentario {
    corrigido: string;
}
