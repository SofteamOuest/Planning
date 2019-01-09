import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { ProjectService, Project } from '../services/project.service';

@Component({
    selector: 'app-project-admin',
    templateUrl: './project-admin.component.html',
    styleUrls: ['./project-admin.component.scss']
})
/** project-admin component*/
export class ProjectAdminComponent implements OnDestroy, OnInit {
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject();

  public dataSource: Project[];

    /** project-admin ctor */
  constructor(private _projectService: ProjectService) {
    this.getList();
  }

  ngOnInit(): void {
    this.dtOptions = {
      searching: false,
      info: false,
      lengthChange: false,
      responsive: true,
    }
  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  getList() {
    this._projectService.getProjectList().subscribe(
      data => {
        this.dataSource = data;
        this.dtTrigger.next();
      }
    );
  }

  removeProject(id: number) {
    var ans = confirm("Do you want to delete customer with Id: " + id);
    if (ans) {
      this._projectService.deleteProject(id).subscribe(
        data => this.dataSource.splice(this.dataSource.findIndex(p => p.id == id), 1)
      );
    }
  }
}
