import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn } from '@angular/forms';
import { Location } from '@angular/common';
import { FormService } from 'src/app/services/form.service';
import { ActivatedRoute } from '@angular/router';
import { UsersService } from '../../../services/users.service';

/**
 * @author Linh Nguyen
 */
@Component({
  selector: 'app-form-registration',
  templateUrl: './form-registration.component.html',
  styleUrls: ['./form-registration.component.scss']
})
export class FormRegistrationComponent implements OnInit {

  constructor(
    private activatedRoute: ActivatedRoute,
    private formService: FormService,
    private location: Location,
    private user: UsersService
  ) {}
  form: FormGroup;
  value_startTime: any;
  value_endTime: any;
  panelOpenState: any;

  selectedIndex: number = null;
  meetingRoom = [
    {
      id: '1',
      name: 'Meeting 1',

    },
    {
      id: '2',
      name: 'Meeting 2',
    },
    {
      id: '3',
      name: 'Meeting 3',
    }
  ];

  ngOnInit(): void {
    const id = Number.parseInt(this.activatedRoute.snapshot.paramMap.get("id"));
    this.formService.getTemplate(id).subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
    })
  }
  goBack() {
    this.location.back();
  }

  validateDate(value) {
    return isNaN(Date.parse(value));
  };

  selectMeetingRoom(index: number) {
    this.selectedIndex = index;
  }
}
