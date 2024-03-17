import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./core/navbar/navbar.component";
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { CoreModule } from './core/core.module';
import { HomeModule } from './home/home.module';
import { SectionHeaderComponent } from './core/section-header/section-header.component';
import { Observable } from 'rxjs';
import { BusyService } from './core/services/busy.service';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  imports: [
    RouterOutlet,
    NavbarComponent,
    HttpClientModule,
    CommonModule,
    CoreModule,
    HomeModule,
    SectionHeaderComponent
  ],
  providers: [],
})
export class AppComponent implements OnInit {
  title = 'Vapor Shop';

  isLoading$: Observable<boolean>;

  constructor(private loadingService: BusyService, private cdRef: ChangeDetectorRef) {
    this.isLoading$ = this.loadingService.loading$;
  }

  ngOnInit(): void {
    this.isLoading$.subscribe(() => {
      this.cdRef.detectChanges();
    });
  }
}

