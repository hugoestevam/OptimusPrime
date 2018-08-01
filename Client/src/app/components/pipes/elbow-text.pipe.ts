import {Pipe, PipeTransform} from '@angular/core';

@Pipe({name: 'elbowText'})

export class ElbowTextPipe implements PipeTransform {
  transform(value: any, args: string[]): any {
    if (value === 180) {
      return 'Em Repouso';
    } else if (value === 135) {
      return 'Levemente Contraído';
    } else if (value === 90) {
      return 'Contraído';
    } else if (value === 45) {
      return 'Fortemente Contraído';
    }
  }
}
