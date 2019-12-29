import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Filter } from '../_models/filter';

@Injectable({
  providedIn: 'root'
})
export class HashtagsService {
    baseUrl = environment.apiUrl;

    constructor(private http: HttpClient) { }

    searchTweetsByHashtag(filter: Filter) {
        return this.http.post(this.baseUrl + 'Twitter/search', filter);
    }
}
