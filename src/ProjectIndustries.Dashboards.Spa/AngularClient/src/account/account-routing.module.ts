import {RouterModule, Routes} from "@angular/router";
import {LoginComponent} from "./components/login/login.component";
import {NgModule} from "@angular/core";
import {DiscordCallbackComponent} from "./components/discord-callback/discord-callback.component";

const routes: Routes = [
  {
    path: "login/:dashboard",
    component: LoginComponent
  },
  {
    path: "oauth2/discord/callback",
    component: DiscordCallbackComponent
  }
]

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AccountRoutingModule {

}
