import { Component, OnInit } from '@angular/core';
import { HistoryService } from '../_services/history.service';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit {
    history: any = [];

    constructor(private historyService: HistoryService) { }

    ngOnInit() {
        this.getHistory();
    }

    getHistory() {
        this.historyService.getHistorySearches().subscribe(
            res => {
                this.history = res;
            }, error => {
                console.log(error);
            });
    }
}
