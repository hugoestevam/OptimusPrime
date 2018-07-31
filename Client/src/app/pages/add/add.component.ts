import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RobotService } from '../../shared/robot.service';
import { RobotCreateCommand } from '../../models/robot.model';
import { FormGroup, FormControl, NgForm, Validators } from '@angular/forms';

@Component({
    selector: 'app-add',
    templateUrl: './add.component.html',
})
export class AddComponent implements OnInit {
    public myForm: FormGroup;

    constructor(
      private route: ActivatedRoute,
      private router: Router,
      private robotService: RobotService
    ) { }

    ngOnInit() {
      this.myForm = new FormGroup({
        'name': new FormControl('', Validators.required)
      });
    }

    register (myForm: NgForm) {
      const command: RobotCreateCommand = {
        RobotName: myForm.value.name
      };

      this.robotService.createRobot(command).subscribe((result: any) => {
        this.router.navigate(['/']);
      });
    }
}
