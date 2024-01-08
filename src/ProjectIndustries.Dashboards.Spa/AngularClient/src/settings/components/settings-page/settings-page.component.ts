import {ChangeDetectionStrategy, Component, OnInit} from "@angular/core";
import {MessageService} from "primeng/api";
import {ReleaseKeysDataSource} from "../../../releases/services/release-keys.data-source";
import {DisposableComponentBase} from "../../../shared/components/disposable.component-base";
import {
  DashboardHostingMode,
  DashboardsService,
  TimeZoneData,
  TimeZonesService,
  UpdateDashboardCommand
} from "../../../dashboards-api";
import {filter, map} from "rxjs/operators";
import {KeyValuePair} from "../../../core/models/key-value-pair.model";
import {FormUtil} from "../../../core/services/form.util";
import {BehaviorSubject} from "rxjs";
import {NotificationService} from "../../../core/services/notifications/notification.service";
import {OperationStatusMessage} from "../../../core/services/notifications/messages.model";
import {DashboardFormGroup} from "../../models/dashboard.form-group";
import {ImageCropperResult} from "../cropper/cropper.component";
import {Base64FileFormGroup} from "../../../shared/models/base64-file.resource";


@Component({
  selector: "app-settings-page",
  templateUrl: "./settings-page.component.html",
  styleUrls: ["./settings-page.component.less"],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    MessageService,
    ReleaseKeysDataSource
  ],
  host: {
    class: "SettingsPage Page"
  }
})
export class SettingsPageComponent extends DisposableComponentBase implements OnInit {
  form = new DashboardFormGroup();

  hostingModes: KeyValuePair<string, DashboardHostingMode>[] = [
    {key: "Path segment", value: DashboardHostingMode.PathSegment},
    {key: "Sub domain", value: DashboardHostingMode.Subdomain},
    {key: "Dedicated domain", value: DashboardHostingMode.Dedicated},
  ];

  timeZones$ = new BehaviorSubject<TimeZoneData[]>([]);

  changedLogo$ = new BehaviorSubject<string>(null);
  changedBg$ = new BehaviorSubject<string>(null);

  cropBgDialogData$ = new BehaviorSubject<string>(null);
  cropLogoDialogData$ = new BehaviorSubject<string>(null);

  constructor(private dashboardsService: DashboardsService,
              private timezonesService: TimeZonesService,
              private notificationService: NotificationService) {
    super();
  }

  async ngOnInit(): Promise<void> {
    await this.refreshData();
    const timeZones = await this.asyncTracker.executeAsAsync(
      this.timezonesService.timeZonesGetSupportedTimeZones().pipe(map(_ => _.payload)));
    this.timeZones$.next(timeZones);
    // this.changeDetector.detectChanges();
  }

  async save(): Promise<void> {
    if (this.form.invalid) {
      FormUtil.validateAllFormFields(this.form);
      return;
    }
    try {
      const data: UpdateDashboardCommand = this.form.value;
      await this.asyncTracker.executeAsAsync(
        this.dashboardsService.dashboardsUpdate(data)
      );

      await this.refreshData();
      this.notificationService.success(OperationStatusMessage.UPDATED);
    } catch (e) {
      this.notificationService.error(OperationStatusMessage.FAILED);
    }
  }

  updateBase64BinaryData(field: Base64FileFormGroup, e: ImageCropperResult): void {
    field.patchValue({
      size: e.blob.size,
      content: e.dataUrl,
      contentType: e.blob.type
    });
  }

  resetSettings(): void {
    this.form.reset({});
    this.changedLogo$.next(null);
    this.changedBg$.next(null);
  }

  private async refreshData(): Promise<void> {
    const data = await this.asyncTracker.executeAsAsync(
      this.dashboardsService.dashboardsGetOwn().pipe(map(_ => _.payload))
    );

    this.resetSettings();
    this.form.patchValue(data, {emitEvent: false});
  }

  saveBgCropChanges(e: ImageCropperResult): void {
    this.form.uploadedCustomBackgroundSrcCtrl.patchValue({
      size: e.blob.size,
      content: e.dataUrl,
      contentType: e.blob.type
    });
    this.cropBgDialogData$.next(null);
    this.changedBg$.next(e.dataUrl);
  }

  saveLogoCropChanges(e: ImageCropperResult): void {
    this.form.uploadedLogoSrcCtrl.patchValue({
      size: e.blob.size,
      content: e.dataUrl,
      contentType: e.blob.type
    });
    this.cropLogoDialogData$.next(null);
    this.changedLogo$.next(e.dataUrl);
  }
}
