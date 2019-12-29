import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HashtagsService } from '../../_services/hashtags.service';
import { Filter } from '../../_models/filter';

@Component({
  selector: 'app-searchfilter',
  templateUrl: './search.filter.component.html',
  styleUrls: ['./search.filter.component.css']
})

export class SearchFilterComponent implements OnInit {
    @Output() dataEmitter = new EventEmitter<any>();    
    searches: any;   
    filter: Filter = {hashtag: ''};
    isAlreadySearched: boolean;

  ngOnInit() {

  }
    changeHashtag($event) {
        if ($event != null) {
            this.filter.hashtag = $event;
        }        
    }

    changeFromDate($event) {
        if ($event != null) {
            this.filter.fromDate = $event;
        }        
    }

    changetoDate($event) {
        if ($event != null) {
            this.filter.toDate = $event;
        }        
    }

    clearData($event) {
        if ($event != null) {
            this.isAlreadySearched = false;
            this.dataEmitter.emit('clear');           
        }
    }

    exportToExcel($event) {
        this.dataEmitter.emit('export');
    }

    searchByFilter() {
        if (this.filter.hashtag != '') {
            this.isAlreadySearched = true;
            this.dataEmitter.emit(this.filter);            
        }        
    }

   
    

}
