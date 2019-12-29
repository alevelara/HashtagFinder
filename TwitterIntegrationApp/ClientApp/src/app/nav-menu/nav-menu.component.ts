import { Component, Output, EventEmitter } from '@angular/core';
import { HashtagsService } from '../_services/hashtags.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})

export class NavMenuComponent { 
    isExpanded = false;

    constructor(private hashtagService: HashtagsService) { }

    collapse() {
      this.isExpanded = false;
    }

    toggle() {
      this.isExpanded = !this.isExpanded;
    }
}
