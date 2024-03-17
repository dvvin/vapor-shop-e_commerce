import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterModule } from '@angular/router';
import { filter } from 'rxjs/operators';
import { BreadcrumbService } from '../breadcrumb.service';

interface IBreadcrumb {
  label: string;
  url: string;
}

@Component({
  selector: 'app-section-header',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './section-header.component.html',
  styleUrls: ['./section-header.component.scss']
})

export class SectionHeaderComponent implements OnInit {
  public breadcrumbs: IBreadcrumb[];
  public isHomePage: boolean = false;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private bcService: BreadcrumbService) {
    this.breadcrumbs = [];
  }

  ngOnInit() {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event) => {
        this.isHomePage = (event as NavigationEnd).urlAfterRedirects === '/';
        this.breadcrumbs = this.createBreadcrumbs(this.activatedRoute.root);
      });
  }

  createBreadcrumbs(route: ActivatedRoute, url: string = '', breadcrumbs: IBreadcrumb[] = []): IBreadcrumb[] {
    const children: ActivatedRoute[] = route.children;

    children.forEach(child => {
      const routeURL: string = child.snapshot.url.map(segment => segment.path).join('/');
      if (routeURL !== '') {
        if (!url.endsWith(routeURL)) {
          url += `/${routeURL}`;
        }
      }

      const label = child.snapshot.data['breadcrumb'];
      if (label) {
        const existingBreadcrumb = breadcrumbs.find(bc => bc.url === url);
        if (!existingBreadcrumb) {
          breadcrumbs.push({ label, url });
        }
      }

      this.createBreadcrumbs(child, url, breadcrumbs);
    });

    this.bcService.productTitle$.subscribe(title => {
      if (title) {
        if (this.breadcrumbs.length > 0) {
          this.breadcrumbs[this.breadcrumbs.length - 1].label = title;
        }
      }
    });

    return breadcrumbs.filter((breadcrumb, index, self) =>
      index === self.findIndex((bc) => (
        bc.url === breadcrumb.url && bc.label === breadcrumb.label
      ))
    );
  }

  hasValidLabel(): boolean {
    const lastBreadcrumb = this.breadcrumbs[this.breadcrumbs.length - 1];
    return lastBreadcrumb && typeof lastBreadcrumb.label === 'string';
  }
}


