import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { User } from './user.service';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class ProjectService {
  myAppUrl: string = "";

  constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;
  }

  getProjectList() {
    return this._http.get(this.myAppUrl + 'api/Projects')
      .map(res => res.json())
      .catch(this.errorHandler);
  }

  getProject(id: number) {
    return this._http.get(this.myAppUrl + "api/Projects/" + id)
      .map((response: Response) => <any[]> response.json())
      .catch(this.errorHandler) 
  }

  saveProject(project: Project) {
    return this._http.post(this.myAppUrl + 'api/Projects', project)
      .map((response: Response) => response.json())
      .catch(this.errorHandler)
  }

  updateProject(project: Project) {
    return this._http.put(this.myAppUrl + 'api/Projects/' + project.id, project)
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }

  deleteProject(id: number) {
    return this._http.delete(this.myAppUrl + "api/Projects/" + id)
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }

  saveProjectTask(projectTask: ProjectTask) {
    return this._http.post(this.myAppUrl + 'api/ProjectTasks', projectTask)
      .map((response: Response) => response.json())
      .catch(this.errorHandler)
  }

  updateProjectTask(projectTask: ProjectTask) {
    return this._http.put(this.myAppUrl + 'api/ProjectTasks/' + projectTask.id, projectTask)
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }

  errorHandler(error: Response) {
    console.log(error);
    return Observable.throw(error);
  }
}

export class Project {
  private _owner: User;

  id: number;
  name: string;
  isActive: boolean;
  ownerId: number;
  projectTasks: ProjectTask[] = [];

  get owner(): User {
    return this._owner;
  }
  set owner(user: User) {
    this.ownerId = user.id;
    this._owner = user;
  }
}

export class ProjectTask {
  id: number;
  name: string;
  projectId: number;
  startDate: Date;
  endDate: Date;
  isActive: boolean;

  isEdited: boolean = false;
}
