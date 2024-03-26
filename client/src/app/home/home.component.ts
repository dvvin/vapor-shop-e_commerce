import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  logoImageUrl = '../assets/images/logo-color.png';
  bootImageUrl = '../assets/images/boot-ang1.png';
  boardImageUrl = '../assets/images/sb-ang1.png';
  gloveImageUrl = '../assets/images/glove-code1.png';
  hatImageUrl = '../assets/images/hat-core1.png';
}
