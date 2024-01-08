import {NgModule} from "@angular/core";
import {SettingsPageComponent} from "./components/settings-page/settings-page.component";
import {SharedModule} from "../shared/shared.module";
import {SettingsRoutingModule} from "./settings-routing.module";
import {CropperComponent} from "./components/cropper/cropper.component";
import { ImageCropDialogComponent } from './components/image-crop-dialog/image-crop-dialog.component';


@NgModule({
  declarations: [SettingsPageComponent, CropperComponent, ImageCropDialogComponent],
  imports: [
    SharedModule,
    SettingsRoutingModule
  ]
})
export class SettingsModule { }
