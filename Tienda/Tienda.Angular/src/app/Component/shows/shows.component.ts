import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { UserService } from '../../Services/user.service';
import { DataFilters } from '../../Models/datafilters';

declare var jQuery: any;
declare var $: any;

@Component({
  selector: 'app-shows',
  templateUrl: './shows.component.html',
  styleUrls: ['./shows.component.css']
})
export class ShowsComponent implements OnInit {

  dataFilters: DataFilters;
  shows: any;
  show: any;
  cast: any;
  episodes: any;
  keyWords:String='';

  constructor(private _http: HttpClient, private userService: UserService) {
  }

  ngOnInit() {
    const header = { 'Authorization': 'Bearer ' + this.userService.GetUser().token };
    const configuration = { headers: header };
    this._http.get(environment.baseUrl + 'api/Shows/GetFilters', configuration).subscribe((data: DataFilters) => {
      this.dataFilters = data;
    },
      error => {
        console.log(error);
      });

    let request = {
      'keyWords': this.keyWords
    };
    this.GetShows(request);
  }
  Shearch(origin: String, value: String) {
    let request;
    switch (origin) {
      case "Genre":
        request = {
          'Genre': value
        };
        break;
      case "ScheduleTime":
        request = {
          'ScheduleTime': value
        };
        break;
      case "Channel":
        request = {
          'Channel': value
        };
        break;
      case "Language":
        request = {
          'Language': value
        };
        break;
    }
    this.GetShows(request);
  }

  GetShows(request) {
    const header = { 'Authorization': 'Bearer ' + this.userService.GetUser().token };
    const configuration = { headers: header };
    this._http.post(environment.baseUrl + 'api/Shows/FindShows', request, configuration).subscribe((data: any) => {
      this.shows = data;
    },
      error => {
        console.log(error);
      });
  }

  GetShow(id) {
    $('#showModal').modal('show');
    const header = { 'Authorization': 'Bearer ' + this.userService.GetUser().token };
    const configuration = { headers: header };
    this._http.get(environment.baseUrl + 'api/Shows/GetShow/' + id, configuration).subscribe((data: any) => {
      this.show = data.show;
      this.cast = data.cast;
      this.cast = data.cast;
    },
      error => {
        console.log(error);
      });
  }

  ShearchByKeywords() {
    let request = {
      'keyWords': this.keyWords
    };

    this.GetShows(request);
  }

  ClosedModal() {
    $('#showModal').modal('hide');
  }
}
