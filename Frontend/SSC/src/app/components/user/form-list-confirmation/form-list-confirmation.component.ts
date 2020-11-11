import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../../services/users.service';

@Component({
  selector: 'app-form-list-confirmation',
  templateUrl: './form-list-confirmation.component.html',
  styleUrls: ['./form-list-confirmation.component.scss']
})
export class FormListConfirmationComponent implements OnInit {

  users = [];

  first = 0;

  rows = 10;

  constructor(private user: UsersService) {
    
  }

  ngOnInit(): void {
    //this.user.getData().subscribe(data => {
    //  this.users = data;
    //})
  }

  next() {
    this.first = this.first + this.rows;
  }

  prev() {
    this.first = this.first - this.rows;
  }

  reset() {
    this.first = 0;
  }

  isLastPage(): boolean {
    return this.users ? this.first === (this.users.length - this.rows) : true;
  }

  isFirstPage(): boolean {
    return this.users ? this.first === 0 : true;
  }


}
