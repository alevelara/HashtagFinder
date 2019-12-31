import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {
    baseUrl = environment.apiUrl;

    constructor(private http: HttpClient) { }

    getHistorySearches() {
        return this.http.get(this.baseUrl + 'HashtagHistory');
    }
}
