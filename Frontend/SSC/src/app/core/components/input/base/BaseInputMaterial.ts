import { Component, EventEmitter, Input, Output } from '@angular/core';
import { InputMaterial } from 'src/app/core/models/InputMaterial.interface';
import { InputStyle } from 'src/app/core/models/types/InputStyle.type';
import { InputType } from 'src/app/core/models/types/InputType.type';

/**
 * @author Thông Hoàng
 */
@Component({
    template: ""
})
export abstract class BaseInputMaterial implements InputMaterial {
    @Input()
    title: string;
    @Input()
    style: InputStyle;
    @Input()
    type: InputType;
    @Input()
    hint: string;
    @Input()
    fullWidth: boolean;
    @Input()
    placeholder?: string;
    @Input()
    suffixIcon?: string;
    @Input()
    prefixIcon?: string;

    @Input()
    value: any;
    @Output()
    valueChange: EventEmitter<any> = new EventEmitter<any>();

    onValueChange(event) {
        this.valueChange.emit(event);
    }
}