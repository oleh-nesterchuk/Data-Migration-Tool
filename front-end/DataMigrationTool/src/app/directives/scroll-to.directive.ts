import { Directive, ElementRef, AfterViewInit } from '@angular/core';

@Directive({
  selector: '[scrollTo]'
})
export class ScrollToDirective implements AfterViewInit {

  constructor(private elementRef: ElementRef) { }

  ngAfterViewInit() {
    this.elementRef.nativeElement.scrollIntoView();
  }
}
