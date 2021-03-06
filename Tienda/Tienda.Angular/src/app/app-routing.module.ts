import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductComponent } from './Component/product/product.component';
import { ResumeOrderComponent } from './Component/order/resume-order/resume-order.component';
import { PaymentComponent } from './Component/payment/payment.component';
import { OrdersComponent } from './Component/Order/orders/orders.component';

const routes: Routes = [
  {
    path: '',
    component: ProductComponent
  },
  {
    path: 'product',
    component: ProductComponent
  },
  {
    path: 'order/:id',
    component: ResumeOrderComponent
  },
  {
    path: 'payment/:id',
    component: PaymentComponent
  },
  {
    path: 'orders',
    component: OrdersComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
