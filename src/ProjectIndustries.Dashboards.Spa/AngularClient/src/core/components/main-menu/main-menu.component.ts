import {ChangeDetectionStrategy, Component, EventEmitter, OnInit, Output} from "@angular/core";
import {DisposableComponentBase} from "../../../shared/components/disposable.component-base";

interface MenuItem {
  route: string;
  title: string;
  badge?: string;
  icon: string;
}

interface MenuItemGroup {
  name: string;
  items: MenuItem[];
}

@Component({
  selector: "app-main-menu",
  templateUrl: "./main-menu.component.html",
  styleUrls: ["./main-menu.component.less"],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MainMenuComponent extends DisposableComponentBase implements OnInit {
  @Output()
  navigated = new EventEmitter<void>();

  menuItems: MenuItemGroup[] = [
    {
      name: "primary",
      items: [{
        route: "/analytics",
        title: "Analytics",
        icon: "#analytics"
      }, {
        route: "/releases",
        title: "Releases",
        icon: "#releases",
        badge: "Pro"
      }, {
        route: "/licenses",
        title: "Licenses",
        icon: "#licenses"
      }, {
        route: "/tickets",
        title: "Tickets",
        icon: "#tickets"
      }, {
        route: "/staff",
        title: "Staff",
        icon: "#staff"
      }]
    },
    {
      name: "advanced",
      items: [{
        route: "/embeds",
        title: "Embeds",
        icon: "#embeds"
      }, {
        route: "/settings",
        title: "Settings",
        icon: "#settings"
      }]
    }
  ];
  trackByRoute = (_: number, item: MenuItem) => item.route;

  ngOnInit(): void {
    document.addEventListener("click", e => {
      this.dispatchNavigated(e);
    }, true);
  }

  private dispatchNavigated(e: MouseEvent) {
    if ((e.target as Element)?.classList.contains("MainMenu-item")) {
      this.navigated.emit();
    }
  }
}
