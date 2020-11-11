import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CountryserviceService } from '../../../services/countryservice.service';


@Component({
  selector: 'app-child',
  templateUrl: './child.component.html',
  styleUrls: ['./child.component.scss']
})
export class ChildComponent implements OnInit {
  @Input() text: string;
  @Output() ButtonClicked: EventEmitter<string> = new EventEmitter<string>();
  constructor(private _dataCountries: CountryserviceService) {
    
  }

  ngOnInit(): void {
    console.log("Child onInit ran");
    this._dataCountries.setTextFromHello(this.text);
  }

  onButtonClicked() {
    this.text = "Changed";
    this.ButtonClicked.emit(this.text);
    this._dataCountries.setTextFromHello(this.text);
  }

}
