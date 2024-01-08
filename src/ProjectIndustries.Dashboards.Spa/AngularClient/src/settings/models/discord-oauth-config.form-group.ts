import {FormControl, FormGroup, Validators} from "@angular/forms";

export class DiscordOAuthConfigFormGroup extends FormGroup {
  constructor() {
    super({
      authorizeUrl: new FormControl(null, [Validators.required]),
      clientId: new FormControl(null, [Validators.required]),
      clientSecret: new FormControl(null, [Validators.required]),
      redirectUrl: new FormControl(null, [Validators.required]),
      scope: new FormControl(null, [Validators.required]),
    });
  }

  get authorizeUrlCtrl(): FormControl {
    return this.get("authorizeUrl") as FormControl;
  }

  get clientIdCtrl(): FormControl {
    return this.get("clientId") as FormControl;
  }

  get clientSecretCtrl(): FormControl {
    return this.get("clientSecret") as FormControl;
  }

  get redirectUrlCtrl(): FormControl {
    return this.get("redirectUrl") as FormControl;
  }

  get scopeCtrl(): FormControl {
    return this.get("scope") as FormControl;
  }
}
