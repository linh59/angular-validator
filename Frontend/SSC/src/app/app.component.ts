import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core/';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(private translate: TranslateService) { }

  ngOnInit(): void {
    this.translate.addLangs(['vi']);
    this.translate.setDefaultLang('vi');
    this.translate.use("vi");
  }
}
