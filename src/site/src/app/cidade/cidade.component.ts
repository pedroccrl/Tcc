import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { CidadeTemasComponent } from './cidade-temas/cidade-temas.component';
import { ActivatedRoute } from '../../../node_modules/@angular/router';
import { Cidade } from '../models/cidade';
import { CidadeService } from '../services/cidade.service';

export interface DialogData {
    animal: string;
    name: string;
}

@Component({
    selector: 'app-cidade',
    templateUrl: './cidade.component.html',
    styleUrls: ['./cidade.component.css']
})
export class CidadeComponent implements OnInit {
    lat: number = 51.678418;
    lng: number = 7.809007;

    cidadeId;
    public cidade: Cidade;

    constructor(
        public dialog: MatDialog,
        private actRouter: ActivatedRoute,
        private cidadeService: CidadeService) { }

    async ngOnInit() {
        this.cidadeId = this.actRouter.snapshot.params.cidadeId;
        this.cidade = await this.cidadeService.getCidade(this.cidadeId);
    }

    clickMark() {
        debugger;
        const dialogRef = this.dialog.open(CidadeTemasComponent, {
            width: '250px'
        });

        dialogRef.afterClosed().subscribe(result => {
            console.log('The dialog was closed');
        });
    }
}