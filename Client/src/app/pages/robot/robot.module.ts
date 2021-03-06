import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { RobotComponent } from './robot.component';
import { RobotInteractionComponent } from './robot-interaction/robot-interaction.component';
import { RobotRoutes } from './robot.routes';
import {  SharedModule  } from '../../shared/shared.module';

@NgModule({
  imports: [
      CommonModule,
      FormsModule,
      RouterModule.forChild(RobotRoutes),
      SharedModule
  ],
  declarations: [
    RobotComponent,
    RobotInteractionComponent
  ],
})

export class RobotModule { }
