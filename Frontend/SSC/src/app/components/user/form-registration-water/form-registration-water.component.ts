import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Location } from '@angular/common';
import { CountryService } from '../../../countryservice';
import { CountryserviceService } from '../../../services/countryservice.service';
import { StaffInformation } from '../../../shared/models/interfaces/StaffInformation.interface';




@Component({
  selector: 'app-form-registration-water',
  templateUrl: './form-registration-water.component.html',
  styleUrls: ['./form-registration-water.component.scss'],

})
export class FormRegistrationWaterComponent implements OnInit {
  panelOpenState: any;
  numWaterBottlesControl = new FormControl(1, Validators.min(1));
  options: StaffInformation[] = [
    {
      id: '1',
      avatar: 'https://i.pinimg.com/236x/c4/70/26/c47026a9fce3fc13e7f8c115b5d7676c.jpg',
      name: 'Linh',
      position: 'FE',
    },
    {
      id: '2',
      avatar: 'https://i.pinimg.com/236x/3c/21/b9/3c21b9b7044bf37b4f929e1a43f9e5d3.jpg',
      name: 'Thông',
      position: 'BE',
    },
  ];
  color: string;
  title = "Hello world";

  selectedUser: any;
  filteredUsers: any[];

  selectedCountry: any;

  countries: any[];

  filteredCountries: any[];

  selectedCountries: any[];

  selectedCountryAdvanced: any[];

  filteredBrands: any[];

  constructor(private location: Location, private countryService: CountryService, private _dataCountries: CountryserviceService) {

  }
  goBack() {
    this.location.back();
  }
  ngOnInit(): void {
    this.countryService.getCountries().then(countries => {
      this.countries = countries;
    });
  }

  filterCountry(event) {
    //in a real application, make a request to a remote url with the query and return filtered results, for demo we filter at client side
    let filtered: any[] = [];
    let query = event.query;
    for (let i = 0; i < this.countries.length; i++) {
      let country = this.countries[i];
      if (country.name.toLowerCase().indexOf(query.toLowerCase()) == 0) {
        filtered.push(country);
      }
    }

    this.filteredCountries = filtered;
  }

  filterUser(event) {
    console.log(event);
    let filtered: any[] = [];
    let query = event.query;
    for (let i = 0; i < this.options.length; i++) {
      let country = this.countries[i];
      if (country.name.toLowerCase().indexOf(query.toLowerCase()) == 0) {
        filtered.push(country);
      }
    }

    this.filteredUsers = filtered;
  }

  onButtonClickedParent() {
    this.title = "Changed Parent";
    this._dataCountries.setTextFromHello(this.title);
  }

  onButtonClickedChild(event: string) {
    this.title = event;
  }
}
