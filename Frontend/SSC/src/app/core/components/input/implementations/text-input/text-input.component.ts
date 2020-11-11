import { Component, Input, OnInit } from '@angular/core';
import { BaseInputMaterial } from '../../base/BaseInputMaterial';

/**
 * @author Thông Hoàng
 */
@Component({
  selector: 'core-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent extends BaseInputMaterial {

  constructor() {
    super();
    this.style = "material";
  }
}
