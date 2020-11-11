/**
 * @author Thông Hoàng
 */
export interface ValidateDefination<T> {
    errorMessage: string;
    rootValue: T;
    validationType: number;
    validationFunction: (fn: T) => boolean;
}