import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { delay, finalize } from 'rxjs/operators';
import { BusyService } from '../services/busy.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

    constructor(private loadingService: BusyService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (request.method === 'POST' && request.url.includes('orders')) {
            return next.handle(request);
        }

        if (!request.url.includes('emailexists')) {
            this.loadingService.show();
        }
        
        return next.handle(request).pipe(
            delay(500),
            finalize(() => this.loadingService.hide())
        );
    }
}
