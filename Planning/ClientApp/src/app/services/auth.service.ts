import { Injectable, Inject, OnInit, Output, EventEmitter } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { User } from '../services/user.service'

@Injectable()
export class AuthService implements CanActivate, OnInit {
  public authPath: string = "user-auth";
  public ignorePath: string[] = ["", "user-auth", "user-detail"]
  private myAppUrl: string;
  @Output() loginEvent: EventEmitter<User> = new EventEmitter();

  constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string, private _router: Router) {
    this.myAppUrl = baseUrl;
  }

  ngOnInit(): void {

  }

  canShowRouterLink(path: string): boolean {
    if (!this.loggedUser) {
      return false;
    } else if (this.ignorePath.some(p => p.toLowerCase() == path.toLowerCase())
      || this.loggedUser.userAccess.some(ua => ua.path.toLowerCase() == path.toLowerCase())) {
      return true;
    } else {
      return false;
    }
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (!this.loggedUser) {
      // not logged in so redirect to login page with the return url
      this._router.navigate(['/user-auth'], { queryParams: { returnUrl: state.url } });
      return false;
    } else if (route.routeConfig.path.toLowerCase() == this.authPath.toLowerCase()) {
      // logged but want to access the auth path, then redirect to root url
      this._router.navigate(['/']);
      return false;
    } else if (this.ignorePath.some(p => p.toLowerCase() == route.routeConfig.path.toLowerCase())
      || this.loggedUser.userAccess.some(ua => ua.path.toLowerCase() == route.routeConfig.path.toLowerCase())) {
      return true;
    } else {
      this._router.navigate(['/']);
      return false;
    }
  }

  get loggedUser(): User {
    return JSON.parse(localStorage.getItem('loggedUser'));
  }
  set loggedUser(user: User) {
    localStorage.setItem('loggedUser', JSON.stringify(user));
  }

  login(username: string, password: string) {
    this._http.post(this.myAppUrl + "api/UserCredentials/Login/", { username: username, password: password })
      .subscribe(data => {
        let user: User = data.json();
        this.loggedUser = user;
        this.loginEvent.emit(user);
      });
  }

  logout() {
    this.loggedUser = null;
    this.loginEvent.emit(null);
    this._router.navigate(['/user-auth']);
  }

  errorHandler(error: Response) {
    console.log(error);
    return Observable.throw(error);
  }
}
