import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { DataTablesModule } from 'angular-datatables';
import { DragulaModule } from 'ng2-dragula';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { DragDropListComponent } from './drag-drop-list/drag-drop-list.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { UserAdminComponent } from './user-admin/user-admin.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { ProjectAdminComponent } from './project-admin/project-admin.component';
import { ProjectDetailComponent } from './project-detail/project-detail.component';
import { UserHolidayComponent } from './user-holiday/user-holiday.component';
import { UserAuthComponent } from './user-auth/user-auth.component';
import { HomeComponent } from './home/home.component';

import { UserService } from './services/user.service';
import { ProjectService } from './services/project.service';
import { AuthService } from './services/auth.service';

@NgModule({
  declarations: [
    AppComponent,
    DragDropListComponent,
    NavMenuComponent,
    UserAdminComponent,
    UserDetailComponent,
    ProjectAdminComponent,
    ProjectDetailComponent,
    UserHolidayComponent,
    UserAuthComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpModule,
    FormsModule,
    DataTablesModule,
    NgbModule.forRoot(),
    DragulaModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'user-auth', component: UserAuthComponent, pathMatch: 'full' },
      { path: 'user-detail', component: UserDetailComponent, pathMatch: 'full', canActivate: [AuthService] },
      { path: 'user-detail/new', component: UserDetailComponent, pathMatch: 'full', canActivate: [AuthService] },
      { path: 'user-detail/:id', component: UserDetailComponent, pathMatch: 'full', canActivate: [AuthService] },
      { path: 'user-admin', component: UserAdminComponent, pathMatch: 'full', canActivate: [AuthService] },
      { path: 'project-admin', component: ProjectAdminComponent, pathMatch: 'full', canActivate: [AuthService] },
      { path: 'project-detail', component: ProjectDetailComponent, pathMatch: 'full', canActivate: [AuthService] },
      { path: 'project-detail/:id', component: ProjectDetailComponent, pathMatch: 'full', canActivate: [AuthService] },
      { path: 'user-holiday', component: UserHolidayComponent, pathMatch: 'full', canActivate: [AuthService] },
    ])
  ],
  providers: [
    UserService,
    ProjectService,
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
