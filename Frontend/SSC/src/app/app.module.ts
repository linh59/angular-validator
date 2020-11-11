import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader/';
import { MaterialModule } from './core/modules/material.module';
import { CoreModule } from './core/modules/core.module';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { UnauthorizedComponent } from './shared/components/unauthorized/unauthorized.component';
import { AppConfigService } from './core/services/app-config.service';
import { LayoutComponent } from './shared/components/layout/layout.component';
import { FormsCollectionComponent } from './components/user/forms-collection/forms-collection.component';
import { FormRegistrationComponent } from './components/user/form-registration/form-registration.component';
import { NgxMatNativeDateModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';
import { NgxMatDatetimePickerModule } from '@angular-material-components/datetime-picker';
import { FormRegistrationWaterComponent } from './components/user/form-registration-water/form-registration-water.component';
import { FormConfirmationComponent } from './components/user/form-confirmation/form-confirmation.component';
import { DndDirective } from './directives/dnd.directive';
import { ProgressComponent } from './components/user/progress/progress.component';


import { AccordionModule } from 'primeng/accordion';
import { TableModule } from 'primeng/table';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { ChildComponent } from './components/user/child/child.component';
import { Child2Component } from './components/user/child2/child2.component';
import { FormListConfirmationComponent } from './components/user/form-list-confirmation/form-list-confirmation.component';
import { CommentsComponent } from './components/comments/comments.component';

export function initializeApp(appConfig: AppConfigService): Function {
  return () => appConfig.load();
}
@NgModule({
  declarations: [
    AppComponent,
    NotFoundComponent,
    UnauthorizedComponent,
    LayoutComponent,
    FormsCollectionComponent,
    FormRegistrationComponent,
    FormRegistrationWaterComponent,
    FormConfirmationComponent,
    DndDirective,
    ProgressComponent,
    ChildComponent,
    Child2Component,
    FormListConfirmationComponent,
    CommentsComponent,

    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CoreModule,
    MaterialModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: (http: HttpClient) => new TranslateHttpLoader(http),
        deps: [HttpClient]
      }
    }),
    NgxMatTimepickerModule,
    NgxMatDatetimePickerModule,
    NgxMatNativeDateModule,

    ReactiveFormsModule,
    AccordionModule,
    AutoCompleteModule,
    TableModule,
  ],
  exports: [
    TranslateModule
  ],
  providers: [
    AppConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [AppConfigService],
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
