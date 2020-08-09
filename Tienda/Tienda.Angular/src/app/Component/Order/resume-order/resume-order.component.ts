import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-resume-order',
  templateUrl: './resume-order.component.html',
  styleUrls: ['./resume-order.component.css']
})
export class ResumeOrderComponent implements OnInit {
  order: any;
  userIp: string;
  constructor(private _router: Router, private _http: HttpClient, private _route: ActivatedRoute) { }

  ngOnInit() {
    this.GetUserIp();
    let id = this._route.snapshot.params["id"];
    this._http.get(environment.baseUrl + 'api/orders/' + id).subscribe((data: any) => {
      this.order = data;
    },
      error => {
        console.log(error);
      });
  }

  Pay() {
    let request = {
      OrderId: this.order.id,
      RemoteIpAddress: this.userIp
    };
    this._http.post(environment.baseUrl + 'api/payments/', request).subscribe((data: any) => {
      location.href = data.urlRedirect;
    },
      error => {
        console.log(error);
      });
  }

  GetUserIp() {
    this._http.get<{ ip: string }>('https://jsonip.com')
      .subscribe((data: any) => {
        this.userIp = data.ip;
      })
  }
}
