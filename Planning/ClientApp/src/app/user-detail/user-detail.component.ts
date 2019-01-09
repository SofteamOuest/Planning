import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User, UserService, UserAccess } from '../services/user.service'
import { AuthService } from '../services/auth.service'

@Component({
    selector: 'app-user-detail',
    templateUrl: './user-detail.component.html',
    styleUrls: ['./user-detail.component.scss']
})
/** user-detail component*/
export class UserDetailComponent implements OnInit {
  private editMode: boolean = true;
  private user: User = null;
  private routerPath: UserAccess[];
  private userPath: UserAccess[];
  private availablePath: UserAccess[];

  /** user-detail ctor */
  constructor(private _route: ActivatedRoute, private _router: Router, private _userService: UserService, private _authService: AuthService) {
  }

  ngOnInit(): void {
    this.routerPath = this._router.config.filter(r => !this._authService.ignorePath.some(ip => ip.toLowerCase() == r.path.toLowerCase()))
      .map(c => {
      let ua = new UserAccess();
      ua.path = c.path;
      return ua;
    });

    this._route.url.subscribe(url => {
      if (url.find(u => u.path == "new")) {
        this.user = new User();
        this.userPath = [];
        this.availablePath = this.routerPath;
      }
    });

    this._route.params.subscribe(params => {
      if (!this.user) {
        if (params.hasOwnProperty('id'))
          this.getUser(params['id']);
        else {
          this.getUser(this._authService.loggedUser.id);
        }
      }
    });
  }

  getUser(id: number) {
    this._userService.getUser(id).subscribe(data => {
      this.user = data;
      this.userPath = this.user.userAccess;
      this.availablePath = this.routerPath.filter(p => !this.userPath.map(up => up.path).includes(p.path));
    });
  }

  saveUser() {
    this.user.userAccess = this.userPath;

    if (this.user.id > 0) {
      this._userService.updateUser(this.user).subscribe(

      );
    } else {
      this._userService.saveUser(this.user).subscribe(data => {
        this.user = data;
        this._router.navigate(["/project", data.id]);
      });
    }
  }

  deleteUser() {
    var ans = confirm("Do you want to delete user with Id: " + this.user.id);
    if (ans) {
      this._userService.deleteUser(this.user.id).subscribe();
      this._router.navigate(['/project-admin']);
    }
  }
}
