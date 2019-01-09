import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-drag-drop-list',
  templateUrl: './drag-drop-list.component.html',
  styleUrls: ['./drag-drop-list.component.scss']
})
/** dragDropList component*/
export class DragDropListComponent implements OnInit {
  @Input() titleList1: string = "";
  @Input() titleList2: string = "";

  private _dataSource1: any[];
  private _dataSource2: any[];

  @Output() dataSource1Change = new EventEmitter();
  @Output() dataSource2Change = new EventEmitter();

  @Input()
  get dataSource1(): any[] {
    return this._dataSource1;
  }
  set dataSource1(value: any[]) {
    this._dataSource1 = value;
    this.dataSource1Change.emit(this._dataSource1);
  }

  @Input()
  get dataSource2(): any[] {
    return this._dataSource2;
  }
  set dataSource2(value: any[]) {
    this._dataSource2 = value;
    this.dataSource2Change.emit(this._dataSource2);
  }

  @Input() propertyPath: string;

  /** dragDropList ctor */
  constructor() {

  }

  ngOnInit(): void {

  }

  display(obj: any) {
    if (this.propertyPath) {
      return obj[this.propertyPath];
    } else {
      return obj;
    }
  }
}
