import {RouterModule, Routes} from "@angular/router";
import {NgModule} from "@angular/core";
import {AnalyticsGeneralComponent} from "./components/analytics-general/analytics-general.component";
import {AnalyticsDiscordComponent} from "./components/analytics-discord/analytics-discord.component";

const routes: Routes = [
  {
    path: "",
    redirectTo: "/analytics/general",
    pathMatch: "full"
  },
  {
    path: "general",
    component: AnalyticsGeneralComponent
  },
  {
    path: "discord",
    component: AnalyticsDiscordComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AnalyticsRoutingModule {
}
