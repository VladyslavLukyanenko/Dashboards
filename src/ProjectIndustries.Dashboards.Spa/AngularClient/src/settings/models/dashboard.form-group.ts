import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Base64FileFormGroup} from "../../shared/models/base64-file.resource";
import {StripeIntegrationConfigFormGroup} from "./stripe-integration-config.form-group";
import {DiscordConfigFormGroup} from "./discord-config.form-group";
import {HostingConfigFormGroup} from "./hosting-config.form-group";

export class DashboardFormGroup extends FormGroup {
  constructor() {
    super({
      id: new FormControl(),
      name: new FormControl(null, [Validators.required]),
      stripeConfig: new StripeIntegrationConfigFormGroup(),
      // expiresAt: new FormControl(),
      discordConfig: new DiscordConfigFormGroup(),
      uploadedLogoSrc: new Base64FileFormGroup(),
      logoSrc: new FormControl(),
      uploadedCustomBackgroundSrc: new Base64FileFormGroup(),
      customBackgroundSrc: new FormControl(),
      timeZoneId: new FormControl(null, [Validators.required]),
      hostingConfig: new HostingConfigFormGroup(),
      chargeBackersExportEnabled: new FormControl(),
    });
  }


  get idCtrl(): FormControl {
    return this.get("id") as FormControl;
  }

  get nameCtrl(): FormControl {
    return this.get("name") as FormControl;
  }

  get stripeConfigCtrl(): StripeIntegrationConfigFormGroup {
    return this.get("stripeConfig") as StripeIntegrationConfigFormGroup;
  }

  get expiresAtCtrl(): FormControl {
    return this.get("expiresAt") as FormControl;
  }

  get discordConfigCtrl(): DiscordConfigFormGroup {
    return this.get("discordConfig") as DiscordConfigFormGroup;
  }

  get logoSrcCtrl(): FormControl {
    return this.get("logoSrc") as FormControl;
  }

  get uploadedLogoSrcCtrl(): Base64FileFormGroup {
    return this.get("uploadedLogoSrc") as Base64FileFormGroup;
  }

  get customBackgroundSrcCtrl(): FormControl {
    return this.get("customBackgroundSrc") as FormControl;
  }

  get uploadedCustomBackgroundSrcCtrl(): Base64FileFormGroup {
    return this.get("uploadedCustomBackgroundSrc") as Base64FileFormGroup;
  }

  get timeZoneIdCtrl(): FormControl {
    return this.get("timeZoneId") as FormControl;
  }

  get hostingConfigCtrl(): HostingConfigFormGroup {
    return this.get("hostingConfig") as HostingConfigFormGroup;
  }

  get chargeBackersExportEnabledCtrl(): FormControl {
    return this.get("chargeBackersExportEnabled") as FormControl;
  }
}
