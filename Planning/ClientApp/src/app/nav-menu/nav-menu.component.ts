import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service'
import { User } from '../services/user.service'

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  loggedUser: User;

  constructor(private _authService: AuthService) {

  }

  ngOnInit(): void {
    this.loggedUser = this._authService.loggedUser;
    this._authService.loginEvent.subscribe(data =>
      this.loggedUser = data
    );
  }

  canShowRouterLink(path: string): boolean {
    return this._authService.canShowRouterLink(path);
  }

  logout() {
    this._authService.logout();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
