import {ActivatedRoute, Router, RouterModule, Routes} from "@angular/router";
import {NgModule} from "@angular/core";
import {AuthenticatedGuard, NotAuthenticatedGuard} from "./core/services/guards";
import {LayoutComponent} from "./core/components/layout/layout.component";
import {NotFoundComponent} from "./core/components/not-found/not-found.component";
import {IdentityService} from "./core/services/identity.service";
import {RoutesProvider} from "./core/services/routes.provider";
import {AuthenticationSchedulerService} from "./core/services/authentication-scheduler.service";
import {skip} from "rxjs/operators";

const appRoutes: Routes = [
  {
    path: "",
    redirectTo: "analytics",
    pathMatch: "full"
  },
  {
    path: "account",
    runGuardsAndResolvers: "always",
    canActivateChild: [NotAuthenticatedGuard],
    loadChildren: () => import("./account/account.module").then(_ => _.AccountModule)
  },
  {
    path: "",
    component: LayoutComponent,
    runGuardsAndResolvers: "always",
    canActivateChild: [AuthenticatedGuard],
    children: [

      {
        path: "analytics",
        loadChildren: () => import("./analytics/analytics.module").then(_ => _.AnalyticsModule)
      },
      {
        path: "releases",
        loadChildren: () => import("./releases/releases.module").then(_ => _.ReleasesModule)
      },
      {
        path: "licenses",
        loadChildren: () => import("./licenses/licenses.module").then(_ => _.LicensesModule)
      },
      {
        path: "tickets",
        loadChildren: () => import("./tickets/tickets.module").then(_ => _.TicketsModule)
      },
      {
        path: "staff",
        loadChildren: () => import("./staff/staff.module").then(_ => _.StaffModule)
      },
      {
        path: "embeds",
        loadChildren: () => import("./embeds/embeds.module").then(_ => _.EmbedsModule)
      },
      {
        path: "settings",
        loadChildren: () => import("./settings/settings.module").then(_ => _.SettingsModule)
      },
      {
        path: "**",
        component: NotFoundComponent
      }
    ]
  },
  {
    path: "**",
    component: NotFoundComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes, {onSameUrlNavigation: "reload"})
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule {
  constructor(router: Router,
              activatedRoute: ActivatedRoute,
              routesProvider: RoutesProvider,
              authScheduler: AuthenticationSchedulerService,
              identityService: IdentityService) {
    identityService.currentUser$
      .pipe(skip(1))
      .subscribe(async u => {
        if (u) {
          authScheduler.scheduleTokenRenewal();
          await router.navigate(routesProvider.getAuthenticatedRedirectUrl());
        } else {
          authScheduler.cancelTokenRenewal();
          await router.navigate(routesProvider.getLoginUrl());
        }
      });
  }
}
