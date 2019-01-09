import { Component } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { UserService, User } from '../services/user.service';

@Component({
    selector: 'app-user-admin',
    templateUrl: './user-admin.component.html',
    styleUrls: ['./user-admin.component.scss']
})
/** user-admin component*/
export class UserAdminComponent {
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject();
  public today: Date = new Date(Date.now());

  public dataSource: User[];
  public isEditing: boolean = false;
  public editingItem: User;

  /** user-admin ctor */
  constructor(private _userService: UserService) {
    this.getList();
  }

  getList() {
    this._userService.getUserList().subscribe(
      data => this.dataSource = data
    );
  }

  create() {
    let item = new User();
    this.dataSource.push(item);
    this.startEdit(item);
  }

  cancel() {
    if (!(this.editingItem.id > 0)) {
      this.dataSource.splice(this.dataSource.length - 1, 1);
    }
    this.stopEdit();
  }

  createUser() {
    let item = new User();
    this.dataSource.push(item);
    this.startEdit(item);
  }

  startEdit(item: User) {
    this.isEditing = true;
    this.editingItem = item;
  }

  stopEdit() {
    this.isEditing = false;
    this.editingItem = null;
  }
}


