import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CacheService {
  private cache: Map<string, any> = new Map();

  constructor() { }

  put(url: string, response: any) {
    console.log('cache miss', url);
    this.cache.set(url, response);
  }

  get(url: string): any {
    console.log('cache hit', url);
    return this.cache.get(url);
  }

  clear() {
    this.cache.clear();
  }
}
