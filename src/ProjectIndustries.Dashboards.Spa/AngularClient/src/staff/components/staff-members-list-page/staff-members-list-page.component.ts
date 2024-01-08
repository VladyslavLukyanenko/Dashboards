import {ChangeDetectionStrategy, Component, OnInit} from '@angular/core';
import {
  MemberRoleBindingsService,
  StaffMemberData,
  StaffRoleMembersData
} from "../../../dashboards-api";
import {BehaviorSubject, combineLatest} from "rxjs";
import {ConfirmationService, MenuItem} from "primeng/api";
import {DisposableComponentBase} from "../../../shared/components/disposable.component-base";
import {map} from "rxjs/operators";

@Component({
  selector: 'app-staff-members-list-page',
  templateUrl: './staff-members-list-page.component.html',
  styleUrls: ['./staff-members-list-page.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  host: {
    class: "Page StaffMembersListPage"
  }
})
export class StaffMembersListPageComponent extends DisposableComponentBase implements OnInit {
  list$ = new BehaviorSubject<StaffRoleMembersData[]>([]);
  items: MenuItem[];

  noData$ = combineLatest([this.list$, this.isLoading$])
    .pipe(map(([list, isLoading]) => !list.length && !isLoading));

  roles$ = this.list$.pipe(
    map(r => r.map(it => ({key: it.name, value: it.id})))
  );

  isStaffDialogVisible = false;
  isRolesDialogVisible = false;

  constructor(private membersService: MemberRoleBindingsService,
              private confirmService: ConfirmationService) {
    super();
  }

  async ngOnInit() {
    this.items = [
      {
        label: 'Add new staff',
        command: () => this.isStaffDialogVisible = true
      },
      {
        label: 'Add new role',
        command: () => this.isRolesDialogVisible = true
      }
    ];

    await this.refreshData();
  }

  async refreshData() {
    const membersResponse = await this.asyncTracker.executeAsAsync(
      this.membersService.memberRoleBindingsGetRoles()
    );

    this.list$.next(membersResponse.payload);
  }

  removeStaffMember(member: StaffMemberData, role: StaffRoleMembersData, e: Event) {
    this.confirmService.confirm({
      message: `Are you sure to remove "${member.name}#${member.discriminator} (${role.name})" from staff members?`,
      target: e.target,
      icon: 'pi pi-exclamation-triangle',
      blockScroll: true,
      accept: async () => {
        await this.asyncTracker.executeAsAsync(
          this.membersService.memberRoleBindingsRemoveMember(member.id, role.id)
        );

        await this.refreshData();
      }
    })
  }
}
