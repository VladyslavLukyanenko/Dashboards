import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {HttpClientModule} from "@angular/common/http";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";

import {RouterModule} from "@angular/router";

import {LocalDatePipe} from "./pipes/LocalDatePipe";
import {ScrollingModule} from "@angular/cdk/scrolling";
import {DateFromNowPipe} from "./pipes/date-from-now.pipe";
// import {InfiniteScrollModule} from "ngx-infinite-scroll";
import {NgxEchartsModule} from "ngx-echarts";
import {SidebarModule} from "primeng/sidebar";
import {ButtonModule} from "primeng/button";
import {AccordionModule} from "primeng/accordion";
import {CardComponent} from "./components/card/card.component";
import {StatsEntryComponent} from "./components/stats-entry/stats-entry.component";
import {ChartLegendComponent} from "./components/chart-legend/chart-legend.component";
import {HorizontalBarComponent} from "./components/horizontal-bar/horizontal-bar.component";
import {DropdownModule} from "primeng/dropdown";
import {CalendarModule} from "primeng/calendar";
import {ProgressBarModule} from "primeng/progressbar";
import {OverlayPanelModule} from "primeng/overlaypanel";
import {ProgressSpinnerModule} from "primeng/progressspinner";
import {MemberSummaryComponent} from "./components/member-summary/member-summary.component";
import {MenuModule} from "primeng/menu";
import {ConfirmPopupModule} from "primeng/confirmpopup";
import {ConfirmationService} from "primeng/api";
import {DialogModule} from "primeng/dialog";
import {InputSwitchModule} from "primeng/inputswitch";
import {SkeletonModule} from "primeng/skeleton";
import {FieldErrorRequiredComponent} from "./components/errors/field-error-required/field-error-required.component";
import {RadioButtonModule} from "primeng/radiobutton";
import {ToastModule} from "primeng/toast";
import {ConfirmDialogComponent} from "./components/confirm-dialog/confirm-dialog.component";
import {FileUploadComponent} from "./components/file-upload/file-upload.component";

const componentsModules = [
  NgxEchartsModule,
  // InfiniteScrollModule,
  ScrollingModule,
  SidebarModule,
  CalendarModule,
  ButtonModule,
  AccordionModule,
  DropdownModule,
  ProgressBarModule,
  ProgressSpinnerModule,
  OverlayPanelModule,
  MenuModule,
  ConfirmPopupModule,
  DialogModule,
  InputSwitchModule,
  SkeletonModule,
  RadioButtonModule,
  ToastModule
];

const sharedSystemModules = [
  CommonModule,
  HttpClientModule,
  ReactiveFormsModule,
  FormsModule
];

const sharedDeclarations = [
  LocalDatePipe,
  DateFromNowPipe,
  CardComponent,
  StatsEntryComponent,
  ChartLegendComponent,
  HorizontalBarComponent,
  MemberSummaryComponent,
  FieldErrorRequiredComponent,


  ConfirmDialogComponent,
  FileUploadComponent,
];

@NgModule({
  imports: [
    RouterModule,

    ...sharedSystemModules,
    ...componentsModules
  ],
  declarations: [
    ...sharedDeclarations,
  ],
  entryComponents: [],
  exports: [
    ...sharedSystemModules,
    ...componentsModules,
    ...sharedDeclarations,
  ],
  providers: [
    ConfirmationService
  ]
})
export class SharedModule {
}
