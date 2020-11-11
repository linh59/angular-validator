import { InputMaterial } from 'src/app/core/models/InputMaterial.interface';

/**
 * @author Thông Hoàng
 */
export interface SSCFormControl extends InputMaterial {
    onClick(fn: (item: SSCFormControl) => any): void;
    onChangeValue(fn: (item: SSCFormControl) => any): void;
}