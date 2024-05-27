import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpEventType } from '@angular/common/http';
import { Observable, of, tap } from 'rxjs';
import { CacheService } from '../services/cache.service';

@Injectable()
export class CacheInterceptor implements HttpInterceptor {
  constructor(private cacheService: CacheService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.method !== 'GET') {
      return next.handle(req);
    }

    const cacheKey = this.getCacheKey(req);
    const cachedResponse = this.cacheService.get(cacheKey);

    if (cachedResponse) {
      return of(cachedResponse);
    }

    return next.handle(req).pipe(
      tap((event: HttpEvent<any>) => {
        if (event.type === HttpEventType.Response) {
          this.cacheService.put(cacheKey, event);
        }
      })
    );
  }

  private getCacheKey(req: HttpRequest<any>): string {
    const url = req.url;
    const params = req.params;
    const brandId = params.get('brandId');
    const typeId = params.get('typeId');
    const sort = params.get('sort');
    const pageNumber = params.get('pageIndex');
    const cacheKey = `${url}_brandId=${brandId}_typeId=${typeId}_sort=${sort}_pageNumber=${pageNumber}`;
    return cacheKey;
  }
}
