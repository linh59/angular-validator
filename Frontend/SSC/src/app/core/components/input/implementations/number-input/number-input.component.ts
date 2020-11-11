import { Component, OnInit } from '@angular/core';
import { BaseInputMaterial } from '../../base/BaseInputMaterial';

@Component({
  selector: 'core-number-input',
  templateUrl: './number-input.component.html',
  styleUrls: ['./number-input.component.scss']
})
export class NumberInputComponent extends BaseInputMaterial { 
  constructor() {
    super();
  }
}
