import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class UserService {
  myAppUrl: string = "";

  constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;
  }

  getUserList() {
    return this._http.get(this.myAppUrl + 'api/Users')
      .map(res => res.json())
      .catch(this.errorHandler);
  }

  getUser(id: number) {
    return this._http.get(this.myAppUrl + "api/Users/" + id)
      .map((response: Response) => <any[]>response.json())
      .catch(this.errorHandler)
  }

  saveUser(user: User) {
    return this._http.post(this.myAppUrl + 'api/Users', user)
      .map((response: Response) => response.json())
      .catch(this.errorHandler)
  }

  updateUser(user: User) {
    return this._http.put(this.myAppUrl + 'api/Users/' + user.id, user)
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }

  deleteUser(id: number) {
    return this._http.delete(this.myAppUrl + "api/Projects/" + id)
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }

  errorHandler(error: Response) {
    console.log(error);
    return Observable.throw(error);
  }
}

export class User {
  id: number;
  firstName: string;
  lastName: string;
  dateOfBirth: Date;
  enterDate: Date;

  username: string;
  userAccess: UserAccess[];
}

export class UserAccess {
  id: number;
  path: string;
}

export class Holiday {
  id: number;
  startDate: Date;
  endDate: Date;
  isValidate: boolean;
}
