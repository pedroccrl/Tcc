import { Injectable } from '@angular/core';
import { Config } from '../models/config';
import { HttpClient, HttpBackend } from "@angular/common/http";
import { ApiUrl } from './api';

@Injectable({
    providedIn: 'root'
})
export class ConfigService {

    constructor(private http: HttpClient) { }

    async getConfig(): Promise<Config> {
        const response = this.http.get<Config>(ApiUrl + 'config').toPromise();
        return response;
    }
}
