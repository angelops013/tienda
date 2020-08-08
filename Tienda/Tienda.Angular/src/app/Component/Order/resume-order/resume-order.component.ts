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
  constructor(private _router: Router, private _http: HttpClient, private _route: ActivatedRoute) { }

  ngOnInit() {
    let id = this._route.snapshot.params["id"];
    this._http.get(environment.baseUrl + 'api/orders/' + id).subscribe((data: any) => {
      this.order = data;
    },
      error => {
        console.log(error);
      });
  }

  Pagar() {

  }
}
