import { Component, OnInit } from '@angular/core';
import { BaseInputMaterial } from '../../base/BaseInputMaterial';

/**
 * @author Thông Hoàng
 */
@Component({
  selector: 'app-selection-input',
  templateUrl: './selection-input.component.html',
  styleUrls: ['./selection-input.component.scss']
})
export class SelectionInputComponent extends BaseInputMaterial {

  constructor() {
    super();
  }
}
