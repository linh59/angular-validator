/**
 * @author Thông Hoàng
 */
export class ResponseModel<T> {
    isSuccess: boolean;
    messageCode: number;
    data: T;
}