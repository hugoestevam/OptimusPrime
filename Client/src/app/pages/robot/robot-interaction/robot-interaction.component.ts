import { Component, Input, OnInit } from '@angular/core';

import {
  Robot
} from '../../../models/robot.model';

@Component({
  selector: 'app-robot-interaction',
  templateUrl: './robot-interaction.component.html'
})
export class RobotInteractionComponent implements OnInit {
  @Input() robot: Robot;

  constructor( ) {
  }

  ngOnInit() {

  }
}
