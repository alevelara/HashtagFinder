import { Component, OnInit, Input } from '@angular/core';
import { Filter } from '../_models/filter';
import { HashtagsService } from '../_services/hashtags.service';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})

export class SearchComponent {    
    searches: any;
    filter: Filter;

    constructor(private hashtagService: HashtagsService) { }

    manageData($event) {
        console.log($event);
        if ($event == 'clear') {
            this.clearData();
        }
        else if ($event == 'export') {
            this.exportExcel();
        }
        else {
            this.filter = $event;
            this.searchTweet();
        }        
    }

    searchTweet() {        
        this.hashtagService.searchTweetsByHashtag(this.filter).subscribe(
            res => {               
                this.searches = res;                
            }, error => {
                console.log(error);
            });
    }

    clearData() {
        this.searches = [];
    }

    exportExcel() {
        /* table id is passed over here */
        let element = document.getElementById('excel-table');
        const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

        /* generate workbook and add the worksheet */
        const wb: XLSX.WorkBook = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

        /* save to file */
        XLSX.writeFile(wb, 'sheetTest.xlsx');
    }
}
