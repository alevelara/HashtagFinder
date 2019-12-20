import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HashtagsService {
    baseUrl = environment.apiUrl;

    constructor(private http: HttpClient) { }

    searchTweetsByHashtag(hashtag: string) {
        return this.http.get(this.baseUrl + 'Twitter/' + hashtag);
    }
}
