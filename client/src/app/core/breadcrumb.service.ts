import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class BreadcrumbService {
    private productTitleSubject = new BehaviorSubject<string | null>(null);
    public productTitle$ = this.productTitleSubject.asObservable();

    set(key: string, value: string) {
        if (key === '@OrderDetail') {
            this.productTitleSubject.next(value);
        }
    }

    updateProductTitle(title: string) {
        this.productTitleSubject.next(title);
    }
}
