import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './shared/components/layout/layout.component';

import { FormRegistrationComponent } from './components/user/form-registration/form-registration.component';
import { FormsCollectionComponent } from './components/user/forms-collection/forms-collection.component';
import { FormRegistrationWaterComponent } from './components/user/form-registration-water/form-registration-water.component';
import { FormConfirmationComponent } from './components/user/form-confirmation/form-confirmation.component';
import { FormListConfirmationComponent } from './components/user/form-list-confirmation/form-list-confirmation.component';
import { CommentsComponent } from './components/comments/comments.component';


const routes: Routes = [{
  path: "",
  pathMatch: "prefix",
  component: LayoutComponent,
  // children: [{
  //   path: "admin",
  //   // children: [{
  //   //   path: "TaoDichvu",
  //   //   component: TaoDichVuComponent
  //   // }]
  // }]
  children: [{
    path: "DanhSachDichVu",
    component: FormsCollectionComponent
  }, {
    path: "DangKyDichVu",
    component: FormRegistrationComponent
  }, {
    path: "DangKyDichVuNuoc",
    component: FormRegistrationWaterComponent
  }, {
    path: "XetDuyetDichVuPhongHop",
    component: FormConfirmationComponent
    }, {
      path: "DanhSachXetDuyet",
      component: FormListConfirmationComponent
    }, {
      path: "Comments",
      component: CommentsComponent
    }]
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
