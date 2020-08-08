import { Component, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

declare var jQuery: any;
declare var $: any;

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  products: any;
  productId: number;
  forma: FormGroup = new FormGroup({});

  constructor(private _http: HttpClient, private _router: Router) { }

  ngOnInit() {
    this.forma = new FormGroup({
      'name': new FormControl('', Validators.required),
      'email': new FormControl('', Validators.required),
      'mobile': new FormControl('', Validators.required)
    });
    this._http.get(environment.baseUrl + 'api/products').subscribe((data: any) => {
      this.products = data;
    },
      error => {
        console.log(error);
      });
  }

  Buy(id: number) {
    this.productId = id;
    $('#orderModal').modal('show');
  }

  ClosedModal() {
    $('#orderModal').modal('hide');
  }

  CreateOrder() {
    if (!this.forma.invalid) {
      let request = {
        CustomerName: this.forma.value.name,
        CustomerEmail: this.forma.value.email,
        CustomerMobile: this.forma.value.mobile,
        ProductId: this.productId
      };
      console.log(request);
      this._http.post(environment.baseUrl + 'api/orders', request).subscribe((data: any) => {
        this._router.navigate(['/order/' + data.id]);
      },
        error => {
          console.log(error);
        });
    }
  }
}
