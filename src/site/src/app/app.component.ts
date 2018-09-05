import { Component, OnInit } from '@angular/core';
import { ConfigService } from './services/config.service';
import { Config } from './models/config';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    public config: Config;
    public isBusy = false;

    constructor(private configService: ConfigService) { }

    async ngOnInit() {
        this.isBusy = true;
        try {
            this.config = await this.configService.getConfig();
        } catch (error) {
            this.config = {siteTitulo: 'error:' + error};
        }
        this.isBusy = false;
    }
}
