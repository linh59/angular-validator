import { Injectable } from '@angular/core';
import { Type } from '@angular/core';
import { TextInputComponent } from '../components/input/implementations/text-input/text-input.component';
import { TextareaInputComponent } from '../components/input/implementations/textarea-input/textarea-input.component';
import { InputType } from '../models/types/InputType.type';
import { NumberInputComponent } from '../components/input/implementations/number-input/number-input.component';

@Injectable({
  providedIn: 'root'
})
export class ControlService {
  constructor() { }

  generateControl(type: InputType): Type<any> {
    switch (type) {
      case "text":
        return TextInputComponent;
      case "textarea":
        return TextareaInputComponent;
      case "number":
        return NumberInputComponent;
      default:
        throw new Error("This component haven't been defined yet");
    }
  }
}
