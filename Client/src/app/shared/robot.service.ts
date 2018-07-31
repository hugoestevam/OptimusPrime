import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import {
    Robot,
    RobotCreateCommand,
    HeadAlignCommand,
    HeadRotateCommand,
    ElbowCommand,
    WristCommand
} from '../models/robot.model';


@Injectable()
export class RobotService {

    public api: string;
    private apiAgent: string;

    constructor(protected http: HttpClient) {
         this.api = `http://localhost:9000/v1/robot`;
    }

    public getUrldownload(): string {
      return `${this.api}`;
    }

    public getRobots():  Observable<any> {
        return this.http.get(`${this.api}`).map((response: any) => response);
    }

    public getRobot(id: string):  Observable<Robot> {
      return this.http.get(`${this.api}/${id}`).map((response: Robot) => response);
    }

    public deleteRobot(id: string): Observable<any> {
        return this.http.delete(`${this.api}/${id}`).map((result: any) => result);
    }

    public createRobot(command: RobotCreateCommand): Observable<Robot> {
        return this.http.post(`${this.api}`, command).map((response: Robot) => response);
    }

    public updateRobotHeadAlign(id: string, command: HeadAlignCommand): Observable<any> {
        return this.http.put(`${this.api}/${id}/headAlign`, command).map((result: any) => result);
    }

    public updateRobotHeadRotate(id: string, command: HeadRotateCommand): Observable<any> {
        return this.http.put(`${this.api}/${id}/headRotate`, command).map((result: any) => result);
    }

    public updateRobotElbow(id: string, command: ElbowCommand): Observable<any> {
        return this.http.put(`${this.api}/${id}/elbow`, command).map((result: any) => result);
    }

    public updateRobotWrist(id: string, command: WristCommand): Observable<any> {
        return this.http.put(`${this.api}/${id}/wrist`, command).map((result: any) => result);
    }
}
