import { InputStyle } from './types/InputStyle.type';
import { InputType } from './types/InputType.type';

/**
 * @author Thông Hoàng
 */
export interface InputMaterial {
    title: string;
    style: InputStyle;
    type: InputType;
    hint?: string;
    fullWidth: boolean;
    placeholder?: string;
    suffixIcon?: string;
    prefixIcon?: string;
}