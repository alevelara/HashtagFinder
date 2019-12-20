import { Component, OnInit } from '@angular/core';
import { HashtagsService } from '../_services/hashtags.service';
import { TwitterStatus } from '../_models/twitterStatus';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
    searches: any;

    constructor(private hashtagService: HashtagsService) { }

    ngOnInit() {
        this.searchTweet('sevillalimpia');
    }

    searchTweet(hashtag: string) {
        this.hashtagService.searchTweetsByHashtag(hashtag).subscribe(
            res => {
                this.searches.push(res);
            }, error => {
                console.log(error);
            });
    }

}
