import {Pipe, PipeTransform} from '@angular/core';

@Pipe({name: 'headDirection'})

export class HeadDirectionPipe implements PipeTransform {
  transform(value: any, args: string[]): any {
      if (value === 0) {
        return 'Em Repouso';
      } else {
        return value + 'º';
      }
   }
}
