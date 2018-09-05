import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatCheckboxModule, MatToolbarModule, MatCardModule } from '@angular/material';

import { AppComponent } from './app.component';
import { ConfigService } from './services/config.service';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { RouterModule } from '../../node_modules/@angular/router';
import { routes } from './routes';
import { CidadeComponent } from './cidade/cidade.component';
import { CidadeMapaComponent } from './cidade/cidade-mapa/cidade-mapa.component';
import { CidadeComentariosComponent } from './cidade/cidade-comentarios/cidade-comentarios.component';
import { CidadeTemasComponent } from './cidade/cidade-temas/cidade-temas.component';

import { AgmCoreModule } from '@agm/core';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        CidadeComponent,
        CidadeMapaComponent,
        CidadeComentariosComponent,
        CidadeTemasComponent
    ],
    imports: [
        RouterModule.forRoot(routes),
        BrowserModule,
        BrowserAnimationsModule,
        MatButtonModule,
        MatCheckboxModule,
        MatToolbarModule,
        HttpClientModule,
        MatCardModule,
        AgmCoreModule.forRoot({
            apiKey: 'AIzaSyDERd7y7_S4PFWtxntd3YOtBHq1jOB_pM0'
        })
    ],
    providers: [
        ConfigService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
