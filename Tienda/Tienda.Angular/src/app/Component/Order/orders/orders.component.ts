import { Component, OnInit } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  orders:any;

  constructor(private _http: HttpClient) { }

  ngOnInit() {
    this._http.get(environment.baseUrl + 'api/orders').subscribe((data: any) => {
      this.orders = data;
    },
      error => {
        console.log(error);
      });
  }

}
