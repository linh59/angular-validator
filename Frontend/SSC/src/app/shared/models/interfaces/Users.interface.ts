/**
 * @author Linh
*/

import { AddressInfomation } from './AddressInformation.interface';
export interface UsersInfomation {
  id: number;
  name: string;
  username: string;
  email: string;
  address: AddressInfomation[];
  phone: string;
}
