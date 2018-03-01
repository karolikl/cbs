import { Component, Inject, OnInit } from '@angular/core';
import { DOCUMENT } from "@angular/platform-browser";
import { environment } from '../environments/environment'; 

@Component({
  selector: 'cbs-root',
  templateUrl: './app.component.html'
})



export class AppComponent implements OnInit {
  title = 'cbs';

  constructor(@Inject(DOCUMENT) private document) {
  }

  ngOnInit(): void {
    console.log('in ngOnInit');
    console.log(environment.baseHref);
    let bases = this.document.getElementsByTagName('base');

    if (bases.length > 0) {
      console.log('found base');
      bases[0].setAttribute('href', environment.baseHref);
      console.log(bases[0].getAttribute('href'));
    }
  }
}
