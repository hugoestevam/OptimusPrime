import { NgModule } from '@angular/core';
import { HeadAlignPipe } from '../components/pipes/head-align.pipe';
import { HeadDirectionPipe } from '../components/pipes/head-direction.pipe';
import { ElbowTextPipe } from '../components/pipes/elbow-text.pipe';
import { WristTextPipe } from '../components/pipes/wrist-text.pipe';

@NgModule({
  imports: [
  ],
  exports: [
    HeadAlignPipe,
    HeadDirectionPipe,
    ElbowTextPipe,
    WristTextPipe
  ],
  declarations: [
    HeadAlignPipe,
    HeadDirectionPipe,
    ElbowTextPipe,
    WristTextPipe
  ],
})

export class SharedModule { }
