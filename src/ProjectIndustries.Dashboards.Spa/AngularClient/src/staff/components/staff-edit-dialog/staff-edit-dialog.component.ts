import {ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {KeyValuePair} from "../../../core/models/key-value-pair.model";
import {MemberRoleAssignmentData, MemberRoleBindingsService, StaffMemberData} from "../../../dashboards-api";
import {BehaviorSubject, combineLatest} from "rxjs";
import {FormControl} from "@angular/forms";
import {DisposableComponentBase} from "../../../shared/components/disposable.component-base";
import {debounceTime, map} from "rxjs/operators";


interface StaffMemberCandidate extends StaffMemberData {
  selectedRole?: number;
}

@Component({
  selector: 'app-staff-edit-dialog',
  templateUrl: './staff-edit-dialog.component.html',
  styleUrls: ['./staff-edit-dialog.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class StaffEditDialogComponent extends DisposableComponentBase implements OnInit {
  @Input() isVisible = false;
  @Input() roles: KeyValuePair<string, number>[] = [];
  @Output() isVisibleChange = new EventEmitter<boolean>();
  @Output() rolesAssigned = new EventEmitter<MemberRoleAssignmentData[]>();

  members$ = new BehaviorSubject<StaffMemberCandidate[]>([]);
  searchCtrl = new FormControl();

  noData$ = combineLatest([this.isLoading$, this.members$])
    .pipe(
      map(([isLoading, members]) => !isLoading && !members.length)
    );

  assignedMembers: StaffMemberCandidate[] = [];

  constructor(private membersService: MemberRoleBindingsService) {
    super();
  }

  async ngOnInit() {
    this.searchCtrl.valueChanges
      .pipe(
        this.untilDestroy(),
        debounceTime(300)
      )
      .subscribe(async s => await this.fetchData(s));

    await this.fetchData();
  }

  private async fetchData(s?: string) {
    this.members$.next([]);

    const membersPage = await this.asyncTracker.executeAsAsync(
      this.membersService.memberRoleBindingsGetStaffMembersPage(false, s, 0, "name", 50)
    );

    this.members$.next(membersPage.payload.content);
  }

  toggleAssigningMember(member: StaffMemberCandidate, role?: number) {
    if (role) {
      this.assignedMembers.push(member);
      member.selectedRole = role;
    } else {
      member.selectedRole = null;
      const idx = this.assignedMembers.indexOf(member);
      this.assignedMembers.splice(idx, 1);
    }
  }

  async assignRoles() {
    if (!this.assignedMembers.length) {
      return;
    }

    this.members$.next([]);
    const payload: MemberRoleAssignmentData[] = this.assignedMembers.map(m => ({
      memberRoleId: m.selectedRole,
      userId: m.id
    }));

    this.assignedMembers = [];
    try {
      await this.asyncTracker.executeAsAsync(this.membersService.memberRoleBindingsAssignRoles(payload));
    } catch {

    }

    this.searchCtrl.setValue(null);
    await this.fetchData();
    this.rolesAssigned.emit(payload);
  }
}
