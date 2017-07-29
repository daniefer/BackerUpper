import { Component, OnInit } from '@angular/core';

import { BackupDetail } from './BackupDetail';
import { BackupDetailService } from './backup-detail.service';

@Component({
  selector: 'app-root',
  template: `
  <h1>{{title}}</h1>
  <h2>Backup locations:</h2>
  <ul>
    <li *ngFor="let location of locations">
      <app-backup-detail [detail]="location" [editMode]="location.edit"></app-backup-detail>
    </li>
  </ul>
  <input type="button" (click)="onAdd($event)" value="Add" />
  `,
  styleUrls: ['./app.component.css'],
  providers: [BackupDetailService]
})
export class AppComponent implements OnInit {
  title = 'BackerUpper Configuration';
  locations: BackupDetail[];

  constructor(private backupDetailService: BackupDetailService) {}

  ngOnInit(): void {
    this.locations = this.backupDetailService.getBackupDetails() as BackupDetail[];
  }

  public onAdd(event: any) {
    this.locations.push({
      name: '',
      targetDirectory: '',
      edit: true
    });
  }
}
