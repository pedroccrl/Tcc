import { Injectable } from '@angular/core';
import { HttpClient } from '../../../node_modules/@angular/common/http';
import { ApiUrl } from './api';
import { Comentario } from '../models/comentario';

@Injectable({
  providedIn: 'root'
})
export class ComentarioService {

    constructor(private http: HttpClient) { }

    async getComentariosCidade(cidadeId) {
        const response = await this.http.get<Comentario[]>(ApiUrl + 'comentario/cidade/' + cidadeId).toPromise();
        return response;
    }
}
