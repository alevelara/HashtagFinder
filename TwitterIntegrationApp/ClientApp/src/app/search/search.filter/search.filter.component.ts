import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Filter } from '../../_models/filter';
import { faFileExcel, faTrashAlt, faSearch } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-searchfilter',
  templateUrl: './search.filter.component.html',
  styleUrls: ['./search.filter.component.css']
})

export class SearchFilterComponent {
    @Output() dataEmitter = new EventEmitter<any>();    
    searches: any;   
    filter: Filter = {hashtag: ''};
    isAlreadySearched: boolean;
    faFileExcel = faFileExcel;
    faTrashAlt = faTrashAlt;
    faSearch = faSearch;

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
