import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from '@angular/router';
import {FormsModule} from '@angular/forms';
import {DeleteComponent} from './delete.component';
import {DeleteRoutes} from './delete.routes';

@NgModule({
  imports: [
      CommonModule,
      FormsModule,
      RouterModule.forChild(DeleteRoutes)
  ],
  declarations: [
    DeleteComponent
  ],
})

export class DeleteModule { }
