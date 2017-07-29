import { Component, OnInit, Input } from '@angular/core';

import { BackupDetail } from './BackupDetail';
import { BackupDetailViewComponent } from './backup-detail-view.component';
import { BackupDetailEditComponent } from './backup-detail-edit.component';

@Component({
    selector: 'app-backup-detail',
    template: `
    <div *ngIf="detail">
        <app-backup-detail-view *ngIf="!detail.edit" [detail]="detail" (edit)="onClick($event, detail)"></app-backup-detail-view>
        <app-backup-detail-edit *ngIf="detail.edit" [detail]="detail" (edit)="onClick($event, detail)" ></app-backup-detail-edit>
    </div>
    `
})
export class BackupDetailComponent implements OnInit {
    @Input() detail: BackupDetail;
    @Input() set editMode(value: boolean){
        this.edit = value ? true : false;
    }

    edit: boolean;
    ngOnInit(): void {}
    onClick(event: any, detail: BackupDetail): void {
        detail.edit = !detail.edit;
    }
}
