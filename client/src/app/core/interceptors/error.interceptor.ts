import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Observable, catchError, throwError } from "rxjs";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    status404: string = 'No entity found matching the specification.';

    constructor(private router: Router, private toastr: ToastrService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError(error => {
                if (error) {
                    if (error.status === 400) {
                        if (error.error.errors) {
                            throw error.error;
                        }
                        else {
                            this.toastr.error(error.error.message, error.status.toString());
                        }
                    }

                    if (error.status === 401) {
                        this.toastr.error(error.error.message, error.status.toString());
                    }

                    if (error.status === 500) {
                        const navigationExtras = { state: { error: error.error } };

                        if (error.error.message === this.status404) {
                            this.router.navigateByUrl('/not-found', navigationExtras);
                        } else {
                            this.router.navigateByUrl('/server-error', navigationExtras);
                        }
                    }
                }
                return throwError(error);
            })
        )
    }
}
