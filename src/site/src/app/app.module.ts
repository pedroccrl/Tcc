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

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent
    ],
    imports: [
        RouterModule.forRoot(routes),
        BrowserModule,
        BrowserAnimationsModule,
        MatButtonModule,
        MatCheckboxModule,
        MatToolbarModule,
        HttpClientModule,
        MatCardModule
    ],
    providers: [
        ConfigService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
