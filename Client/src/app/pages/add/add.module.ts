import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from '@angular/router';
import {FormsModule} from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import {AddComponent} from './add.component';
import {AddRoutes} from './add.routes';

@NgModule({
  imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      RouterModule.forChild(AddRoutes)
  ],
  declarations: [
    AddComponent
  ],
})

export class AddModule { }
