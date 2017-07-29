import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';

import { BackupDetail } from './BackupDetail';

@Component({
    selector: 'app-backup-detail-edit',
    template: `
    <div *ngIf="detail">
        <div>
            <label>Output File Name: </label>
            <input [(ngModel)]="detail.name" placeholder="name to give the backup file" />
        </div>
        <div>
            <label>Source Directory: </label>
            <input [(ngModel)]="detail.targetDirectory" placeholder="directory that should be backed up" />
        </div>
        <input type="button" value="Save" (click)="onClick($event)" />
    </div>
    `
})
export class BackupDetailEditComponent implements OnInit {
    @Input() detail: BackupDetail;
    @Output() edit = new EventEmitter();

    ngOnInit(): void {

    }

    public onClick(event: any) {
        event.preventDefault();
        this.edit.emit(event);
    }
}
