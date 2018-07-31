import { Component, OnInit, OnDestroy, ViewContainerRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RobotService } from '../../shared/robot.service';
import { ToastsManager } from 'ng2-toastr';

import {
  Robot,
  HeadAlignCommand,
  HeadRotateCommand,
  ElbowCommand,
  WristCommand
} from '../../models/robot.model';

@Component({
  selector: 'app-robot',
  templateUrl: './robot.component.html'
})
export class RobotComponent implements OnInit, OnDestroy {
  public robot: Robot;
  private id;
  private sub;

  constructor(
    public toastr: ToastsManager,
    private vcr: ViewContainerRef,
    private route: ActivatedRoute,
    private router: Router,
    private robotService: RobotService
  ) {
      this.toastr.setRootViewContainerRef(vcr);
      this.robot = {
        id: '-',
        name: '-',
        headAlign: 0,
        headDirection: 0,
        leftElbowPosition: 0,
        rightElbowPosition: 0,
        leftWristDirection: 0,
        rightWristDirection: 0
      };
    }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.id = params['id'];
      this.loadRobot(this.id);
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  private loadRobot(id: string) {
    this.robotService.getRobot(this.id).subscribe((robot: Robot) => {
      this.robot = robot;
    });
  }

  public moveHeadUp() {
    const command: HeadAlignCommand = {
      headMove: 'Top'
    };
    this.robotService.updateRobotHeadAlign(this.id, command).subscribe(
      (result: any) => {
        this.loadRobot(this.id);
      },
      error => {
        this.toastr.error(
          error.error.errorMessage,
          'Oops! ' + error.status + '(' + error.statusText + ')'
        );
      }
    );
  }

  public moveHeadDown() {
    const command: HeadAlignCommand = {
      headMove: 'Down'
    };
    this.robotService.updateRobotHeadAlign(this.id, command).subscribe(
      (result: any) => {
        this.loadRobot(this.id);
      },
      error => {
        this.toastr.error(
          error.error.errorMessage,
          'Oops! ' + error.status + '(' + error.statusText + ')'
        );
      }
    );
  }

  public moveHeadLeft() {
    const command: HeadRotateCommand = {
      headRotate: 'Left'
    };
    this.robotService.updateRobotHeadRotate(this.id, command).subscribe(
      (result: any) => {
        this.loadRobot(this.id);
      },
      error => {
        this.toastr.error(
          error.error.errorMessage,
          'Oops! ' + error.status + '(' + error.statusText + ')'
        );
      }
    );
  }

  public moveHeadRight() {
    const command: HeadRotateCommand = {
      headRotate: 'Right'
    };
    this.robotService.updateRobotHeadRotate(this.id, command).subscribe(
      (result: any) => {
        this.loadRobot(this.id);
      },
      error => {
        this.toastr.error(
          error.error.errorMessage,
          'Oops! ' + error.status + '(' + error.statusText + ')'
        );
      }
    );
  }

  public moveLeftElbowCollapse() {
    const command: ElbowCommand = {
      elbowSide: 'Left',
      elbowAction: 'Collapse'
    };
    this.robotService.updateRobotElbow(this.id, command).subscribe(
      (result: any) => {
        this.loadRobot(this.id);
      },
      error => {
        this.toastr.error(
          error.error.errorMessage,
          'Oops! ' + error.status + '(' + error.statusText + ')'
        );
      }
    );
  }

  public moveLeftElbowExpand() {
    const command: ElbowCommand = {
      elbowSide: 'Left',
      elbowAction: 'Expand'
    };
    this.robotService.updateRobotElbow(this.id, command).subscribe(
      (result: any) => {
        this.loadRobot(this.id);
      },
      error => {
        this.toastr.error(
          error.error.errorMessage,
          'Oops! ' + error.status + '(' + error.statusText + ')'
        );
      }
    );
  }

  public moveRightElbowCollapse() {
    const command: ElbowCommand = {
      elbowSide: 'Right',
      elbowAction: 'Collapse'
    };
    this.robotService.updateRobotElbow(this.id, command).subscribe(
      (result: any) => {
        this.loadRobot(this.id);
      },
      error => {
        this.toastr.error(
          error.error.errorMessage,
          'Oops! ' + error.status + '(' + error.statusText + ')'
        );
      }
    );
  }

  public moveRightElbowExpand() {
    const command: ElbowCommand = {
      elbowSide: 'Right',
      elbowAction: 'Expand'
    };
    this.robotService.updateRobotElbow(this.id, command).subscribe(
      (result: any) => {
        this.loadRobot(this.id);
      },
      error => {
        this.toastr.error(
          error.error.errorMessage,
          'Oops! ' + error.status + '(' + error.statusText + ')'
        );
      }
    );
  }

  public moveLeftWristToLeft() {
    const command: WristCommand = {
      wristSide: 'Left',
      wristRotate: 'Left'
    };
    this.robotService.updateRobotWrist(this.id, command).subscribe(
      (result: any) => {
        this.loadRobot(this.id);
      },
      error => {
        this.toastr.error(
          error.error.errorMessage,
          'Oops! ' + error.status + '(' + error.statusText + ')'
        );
      }
    );
  }

  public moveLeftWristToRight() {
    const command: WristCommand = {
      wristSide: 'Left',
      wristRotate: 'Right'
    };
    this.robotService.updateRobotWrist(this.id, command).subscribe(
      (result: any) => {
        this.loadRobot(this.id);
      },
      error => {
        this.toastr.error(
          error.error.errorMessage,
          'Oops! ' + error.status + '(' + error.statusText + ')'
        );
      }
    );
  }

  public moveRightWristToLeft() {
    const command: WristCommand = {
      wristSide: 'Right',
      wristRotate: 'Left'
    };
    this.robotService.updateRobotWrist(this.id, command).subscribe(
      (result: any) => {
        this.loadRobot(this.id);
      },
      error => {
        this.toastr.error(
          error.error.errorMessage,
          'Oops! ' + error.status + '(' + error.statusText + ')'
        );
      }
    );
  }

  public moveRightWristToRight() {
    const command: WristCommand = {
      wristSide: 'Right',
      wristRotate: 'Right'
    };
    this.robotService.updateRobotWrist(this.id, command).subscribe(
      (result: any) => {
        this.loadRobot(this.id);
      },
      error => {
        this.toastr.error(
          error.error.errorMessage,
          'Oops! ' + error.status + '(' + error.statusText + ')'
        );
      }
    );
  }
}
