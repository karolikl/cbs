import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { CaseReportModule } from './case-report/case-report.module';
import { CoreModule } from './core/core.module';
import { APP_BASE_HREF } from '@angular/common';
import { environment } from '../environments/environment'; 

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    HttpModule,
    CoreModule,
    RouterModule.forRoot([]),
    CaseReportModule
  ],
  providers: [{provide: APP_BASE_HREF, useValue: environment.baseHref}],
  bootstrap: [AppComponent]
})
export class AppModule { }
