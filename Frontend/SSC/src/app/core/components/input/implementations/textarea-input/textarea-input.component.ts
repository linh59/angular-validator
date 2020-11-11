import { Component, Input, OnInit } from '@angular/core';
import { BaseInputMaterial } from '../../base/BaseInputMaterial';

/**
 * @author Thông Hoàng
 */
@Component({
  selector: 'core-textarea-input',
  templateUrl: './textarea-input.component.html',
  styleUrls: ['./textarea-input.component.scss']
})
export class TextareaInputComponent extends BaseInputMaterial {

  constructor() {
    super();
    this.style = "material";
  }
}
