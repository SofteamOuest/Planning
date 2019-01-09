import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs/Subject';
import { ProjectService, Project, ProjectTask } from '../services/project.service';
import { AuthService } from '../services/auth.service';

@Component({
    selector: 'app-project-detail',
    templateUrl: './project-detail.component.html',
    styleUrls: ['./project-detail.component.scss']
})
/** project-detail component*/
export class ProjectDetailComponent implements OnInit, OnDestroy {
  dtOptionsTask: DataTables.Settings = {};
  dtTriggerTask: Subject<any> = new Subject();

  public project: Project;

  /** project-detail ctor */
  constructor(private _route: ActivatedRoute, private _projectService: ProjectService, private _authService: AuthService, private _router: Router) {
    
  }

  ngOnInit(): void {
    this._route.params.subscribe(params => {
      if (params.hasOwnProperty('id'))
        this.getProject(params['id']);
      else {
        this.project = new Project();
        this.project.owner = this._authService.loggedUser;
      }
    });

    let dtOptions = {
      searching: false,
      info: false,
      lengthChange: false,
      responsive: true,
    };
    this.dtOptionsTask = dtOptions;
  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTriggerTask.unsubscribe();
  }

  getProject(id: number) {
    this._projectService.getProject(id).subscribe(
      data => this.project = data
    );
  }

  saveProject() {
    if (this.project.id > 0) {
      this._projectService.updateProject(this.project).subscribe(
        data => this.saveProjectTasks()
      );
    } else {
      this._projectService.saveProject(this.project).subscribe(data => {
        this.project = data;
        this.saveProjectTasks();
        this._router.navigate(["/project", data.id]);
      });
    }
  }

  saveProjectTasks() {
    this.project.projectTasks.filter(pt => pt.isEdited).forEach(
      pt => {
        if (pt.id > 0) {
          this._projectService.updateProjectTask(pt).subscribe(
            data => pt.isEdited = false
          );
        } else {
          this._projectService.saveProjectTask(pt).subscribe(
            data => {
              pt.id = data.id;
              pt.isEdited = false;
            }
          );
        }
      }
    );
  }

  deleteProject() {
    var ans = confirm("Do you want to delete project with Id: " + this.project.id);
    if (ans) {
      this._projectService.deleteProject(this.project.id).subscribe();
      this._router.navigate(['/project-admin']);
    }
  }

  addProjectTask() {
    let newTask = new ProjectTask();
    newTask.projectId = this.project.id;
    this.project.projectTasks.push(newTask);
  }
}
