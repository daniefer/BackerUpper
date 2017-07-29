import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';

import { BackupDetail } from './BackupDetail';

@Component({
    selector: 'app-backup-detail-view',
    template: `
    <div *ngIf="detail">
        <div>
            <label>Output File Name: </label>
            {{detail.name}}
        </div>
        <div>
            <label>Source Directory: </label>
            {{detail.targetDirectory}}
        </div>
        <input type="button" value="Edit" (click)="onClick($event)"/>
    </div>
    `
})
export class BackupDetailViewComponent implements OnInit {
    @Input() detail: BackupDetail;
    @Output() edit = new EventEmitter();

    ngOnInit(): void {
        if ((<any>this.detail).edit) {
                delete (<any>this.detail).edit;
            }
    }

    public onClick(event: any) {
        event.preventDefault();
        this.edit.emit(event);
    }
}
