import {OnInit, Component} from '@angular/core';
import { RobotService } from '../../shared/robot.service';

import {
  Robot,
} from '../../models/robot.model';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  public robots: any;

  constructor(private robotService: RobotService) { }

  ngOnInit() {
    this.robotService.getRobots().subscribe((robots: any) => {
      this.robots = robots;
    });
  }
}
