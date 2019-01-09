import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../services/auth.service'

@Component({
    selector: 'app-user-auth',
    templateUrl: './user-auth.component.html',
    styleUrls: ['./user-auth.component.scss']
})
/** user-auth component*/
export class UserAuthComponent implements OnInit, OnDestroy {
  public username: string;
  public password: string;
  public submitted = false;

  public returnUrl: string = '';

  /** user-auth ctor */
  constructor(private _router: Router, private _activatedRoute: ActivatedRoute, private _authService: AuthService) {

  }

  ngOnInit(): void {
    this._activatedRoute.queryParams.subscribe(data => this.returnUrl = data.returnUrl);

    this._authService.loginEvent.subscribe(data => {
      if (data)
        this._router.navigate([this.returnUrl]);
    });
  }

  ngOnDestroy(): void {
    this._authService.loginEvent.unsubscribe();
  }

  onSubmit() {
    this.submitted = true;
  }

  login() {
    this._authService.login(this.username, this.password);
  }
}
