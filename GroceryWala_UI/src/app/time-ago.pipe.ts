import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({
  name: 'timeAgo'
})
export class TimeAgoPipe implements PipeTransform {

  transform(value: string): string {
    const now = moment.utc();
    const timestamp = moment.utc(value);
    const duration = moment.duration(now.diff(timestamp));
    const hours = duration.asHours();

    if (hours < 1) {
      const minutes = duration.asMinutes();
      return `${Math.floor(minutes)} minutes ago`;
    } else if (hours < 24) {
      return `${Math.floor(hours)} hours ago`;
    } else {
      const days = Math.floor(hours / 24);
      return `${days} days ago`;
    }
  }
}
