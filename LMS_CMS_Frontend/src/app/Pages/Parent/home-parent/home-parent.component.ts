import { Component } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-home-parent',
  standalone: true,
  imports: [TranslateModule],
  templateUrl: './home-parent.component.html',
  styleUrl: './home-parent.component.css'
})
export class HomeParentComponent {
}
