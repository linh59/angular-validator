import { AfterViewInit, Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { ActivatedRoute } from '@angular/router';
import { AppConfigService } from 'src/app/core/services/app-config.service';
import { AuthService } from 'src/app/services/auth.service';

/**
 * @author Thông Hoàng
 */
@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit, AfterViewInit {
  @ViewChild(MatSidenav)
  sidenav: MatSidenav;

  constructor(private route: ActivatedRoute, private authService: AuthService) { }

  ngOnInit(): void {
    const hrRefreshToken = this.route.snapshot.queryParamMap.get('refreshtoken');
    if (!this.authService.isAuthenicated) {
      if (hrRefreshToken !== null) {
        this.authService.login(hrRefreshToken);
      }
      // Not authorized and don't have refreshtoken on query
      else {
        window.location.href = `${AppConfigService.settings.authenticationUrl}NhanVien/DangNhap?redirect=${window.location.href}`;
      }
    }
  }
  ngAfterViewInit(): void {
    //setTimeout(() => this.changeSidenavMode(window.innerWidth), 1);
  }


  @HostListener("window:resize", ["$event"])
  onResize(event) {
    //this.changeSidenavMode(window.innerWidth);
  }

  //changeSidenavMode(width: number) {
  //  if (width < 500) {
  //    this.sidenav.mode = "over";
  //  } else {
  //    this.sidenav.mode = "side";
  //  }
  //}
}
