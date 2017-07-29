import { Injectable } from '@angular/core';

import { BackupDetail } from './BackupDetail';

const LOCATIONS: BackupDetail[] = [
  {name: 'dan\'s home folder', targetDirectory: 'C:\\Users\\daniel.ferguson', edit: false},
  {name: 'kelsey\'s home folder', targetDirectory: 'C:\\Users\\kelsey.ferguson', edit: false}
];

@Injectable()
export class BackupDetailService {
    getBackupDetails(): BackupDetail[] {
        return LOCATIONS;
    }
}
