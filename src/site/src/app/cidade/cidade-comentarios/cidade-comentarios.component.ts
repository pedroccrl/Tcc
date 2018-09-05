import { Component, OnInit, Input } from '@angular/core';
import { Comentario } from '../../models/comentario';

@Component({
    selector: 'app-cidade-comentarios',
    templateUrl: './cidade-comentarios.component.html',
    styleUrls: ['./cidade-comentarios.component.css']
})
export class CidadeComentariosComponent implements OnInit {

    @Input()
    public comentarios: Comentario[];

    constructor() { }

    ngOnInit() {
    }

}
