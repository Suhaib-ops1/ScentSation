import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PerfumeBuilderComponent } from './ali/perfume-builder/perfume-builder.component';

const routes: Routes = [
  { path: "perfume-builder", component: PerfumeBuilderComponent }


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
