import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RobotService } from '../../shared/robot.service';
import { Robot } from '../../models/robot.model';

@Component({
    selector: 'app-delete',
    templateUrl: './delete.component.html',
})
export class DeleteComponent implements OnInit, OnDestroy {
  private id;
  private sub;
  public robotName: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private robotService: RobotService
  ) { }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.id = params['id'];
      this.robotService.getRobot(this.id).subscribe((robot: Robot) => {
        this.robotName = robot.name;
      });
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  public deleteRobot() {
    this.robotService.deleteRobot(this.id).subscribe((result: any) => {
      this.router.navigate(['/']);
    });
  }
}
