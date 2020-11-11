import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TextInputComponent } from '../components/input/implementations/text-input/text-input.component';
import { TextareaInputComponent } from '../components/input/implementations/textarea-input/textarea-input.component';
import { MaterialModule } from './material.module';
import { NumberInputComponent } from '../components/input/implementations/number-input/number-input.component';
import { NumbericInputDirective } from '../directive/numberic-input.directive';

/**
 * @author Thông Hoàng
 */
@NgModule({
  declarations: [
    TextInputComponent,
    TextareaInputComponent,
    NumberInputComponent,
    NumbericInputDirective
  ],
  imports: [
    MaterialModule
  ],
  exports: [
    CommonModule,
  ]
})
export class CoreModule { }
