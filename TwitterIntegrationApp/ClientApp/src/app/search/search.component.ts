import { Component, OnInit, Input } from '@angular/core';
import { Filter } from '../_models/filter';
import { HashtagsService } from '../_services/hashtags.service';
import * as XLSX from 'xlsx';
import { faEyeSlash } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})

export class SearchComponent {    
    searches: any = [];
    filter: Filter;
    faEyeSlash = faEyeSlash;

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
      const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.searches);          
      const wb: XLSX.WorkBook =
      {
        Sheets: { 'data': ws },
        SheetNames: ['data']  
      };
      //const excelBuffer: any = XLSX.write(wb, { bookType: 'xlsx', type: 'array' });

      XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
      XLSX.writeFile(wb, 'sheetTest.xlsx');
    }
}
