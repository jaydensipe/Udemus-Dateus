import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyRequestCount: number = 0;

  constructor(private spinnerService: NgxSpinnerService) { }

  busy() {
    this.busyRequestCount++;
    this.spinnerService.show(undefined, {
      type: 'fire',
      bdColor: 'rgba(255,255,255,70)',
      fullScreen: false,
      color: '#900C3F '
    });
  }

  idle() {
    this.busyRequestCount--;

    if (this.busyRequestCount <= 0) { 
      this.busyRequestCount = 0;
      this.spinnerService.hide();
    }
  }
}
