import {Pipe, PipeTransform} from '@angular/core';

@Pipe({name: 'headAlign'})

export class HeadAlignPipe implements PipeTransform {
  transform(value: any, args: string[]): any {
        if (value === 0) {
            return 'Para Baixo';
        } else if (value === 1) {
            return 'Normal';
        } else {
            return 'Para Cima';
        }
   }
}
