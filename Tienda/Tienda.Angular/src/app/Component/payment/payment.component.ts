import { Component, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {

  constructor(private _router: Router, private _http: HttpClient, private _route: ActivatedRoute) { }

  ngOnInit() {
    let id = this._route.snapshot.params["id"];
    this._http.get(environment.baseUrl + 'api/payments/' + id).subscribe((data: any) => {
      this._router.navigate(['/order/' + data.orderId]);
    },
      error => {
        console.log(error);
      });
  }
}
