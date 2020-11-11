import { Component, OnInit } from '@angular/core';
import { CountryserviceService } from '../../../services/countryservice.service';
@Component({
  selector: 'app-child2',
  templateUrl: './child2.component.html',
  styleUrls: ['./child2.component.scss']
})
export class Child2Component implements OnInit {

  textFromHello: string;
  constructor(private _dataCountries: CountryserviceService) { }

  ngOnInit(): void {
    this._dataCountries.textFromHello$.subscribe(text => this.textFromHello = text);
  }

}
