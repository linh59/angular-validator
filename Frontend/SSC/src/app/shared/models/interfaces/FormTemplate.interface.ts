import { InputMaterial } from 'src/app/core/models/InputMaterial.interface';

export interface FormTemplate {
    id: number;
    controls: InputMaterial[];
    tableControls: InputMaterial[];
}