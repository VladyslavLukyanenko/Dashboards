import {ChangeDetectionStrategy, Component, EventEmitter, HostBinding, Input, OnInit, Output} from '@angular/core';
import {StaffMemberData} from "../../../dashboards-api";
import {KeyValuePair} from "../../../core/models/key-value-pair.model";

@Component({
  selector: 'app-staff-member-list-item',
  templateUrl: './staff-member-list-item.component.html',
  styleUrls: ['./staff-member-list-item.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  host: {
    class: "StaffMember"
  }
})
export class StaffMemberListItemComponent implements OnInit {
  @Input() roles: KeyValuePair<string, number>[] = [];
  @Input() selectedRole: number;
  @HostBinding("class.is-selected") @Input() isSelected: boolean;
  @Input() member: StaffMemberData;

  @Output() selectedRoleChange = new EventEmitter<number>();

  constructor() { }

  ngOnInit(): void {
  }

}
