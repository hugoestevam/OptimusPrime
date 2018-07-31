export class Robot {
  public id?: string;
  public name: string;
  public headAlign: number;
  public headDirection: number;
  public leftElbowPosition: number;
  public rightElbowPosition: number;
  public leftWristDirection: number;
  public rightWristDirection: number;
}

export class RobotCreateCommand {
  public RobotName: string;
}

export class HeadAlignCommand {
  public headMove: string;
}

export class HeadRotateCommand {
  public headRotate: string;
}

export class ElbowCommand {
  public elbowSide: string;
  public elbowAction: string;
}

export class WristCommand {
  public wristSide: string;
  public wristRotate: string;
}




