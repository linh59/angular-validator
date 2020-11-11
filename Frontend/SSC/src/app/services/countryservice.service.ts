import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CountryserviceService {
  private _textFromHelloSubject: BehaviorSubject<string> = new BehaviorSubject<string>('');
  textFromHello$: Observable<string> = this._textFromHelloSubject.asObservable();


  setTextFromHello(text: string) {
    this._textFromHelloSubject.next(text);
  }
}
