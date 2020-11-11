import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from '../core/models/ResponseModel.interface';
import { HttpService } from '../core/services/http.service';
import { GET_FORM_TEMPLATE_URL } from '../shared/Defination';
import { FormTemplate } from '../shared/models/interfaces/FormTemplate.interface';

@Injectable({
  providedIn: 'root'
})
export class FormService {

  constructor(private http: HttpService) { }

  getTemplate(id: number) : Observable<ResponseModel<FormTemplate>> {
    return this.http.get<FormTemplate>(GET_FORM_TEMPLATE_URL, [id.toString()])
  }
}
