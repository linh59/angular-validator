import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';


/**
 * @author Thông Hoàng
 */
@Injectable()
export class AuthorizeInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService) { }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // Define default header
        let headersConfig = {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        };

        if (this.authService.isAuthenicated){
            headersConfig["Authorization"] = `Bearer ${this.authService.token}`;
        }       
        req = req.clone({
            setHeaders: headersConfig,
        });
        return next.handle(req);
    }
}
