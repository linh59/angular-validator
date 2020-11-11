import { AbstractControl } from '@angular/forms';
import { ValidatorFn } from '@angular/forms';

export const emailValidator = (): ValidatorFn => {
  return (control: AbstractControl): { [key: string]: string } => {
    const result = /^[a-z][a-z0-9_\.]{5,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$/i.test(control.value);
    console.log(`emailValidator = ${result}`);
    return result == true ? null : { 'error': 'Wrong email format' };
  };
}
