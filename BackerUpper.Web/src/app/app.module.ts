import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { BackupDetailEditComponent } from './backup-detail-edit.component';
import { BackupDetailViewComponent } from './backup-detail-view.component';
import { BackupDetailComponent } from './backup-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    BackupDetailComponent,
    BackupDetailViewComponent,
    BackupDetailEditComponent
  ],
  imports: [
    BrowserModule,
    FormsModule
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
