import { Component, OnInit } from '@angular/core';
import { Cidade } from '../models/cidade';
import { CidadeService } from '../services/cidade.service';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    public cidades: Cidade[];
    public isBusy = false;

    constructor(private cidadeService: CidadeService) { }

    async ngOnInit() {
        this.isBusy = true;

        try {
            this.cidades = await this.cidadeService.getCidades();
        } catch (error) {
            
        }

        this.isBusy = false;
    }

}
