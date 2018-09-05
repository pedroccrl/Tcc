import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Cidade } from '../models/cidade';
import { ApiUrl } from './api';

@Injectable({
    providedIn: 'root'
})
export class CidadeService {

    constructor(private http: HttpClient) { }

    async getCidades() {
        const response = this.http.get<Cidade[]>(ApiUrl + 'cidade').toPromise();
        return response;
    }
}
