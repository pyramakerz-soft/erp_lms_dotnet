import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TestService } from './Services/test.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'LMS_CMS_Frontend';
  data = null

  constructor(private testService: TestService) { }

  ngOnInit(): void {
    this.getData()
  }

  getData(): void {
    this.testService.getData().subscribe(
      data => {
        this.data = data
        console.log(data)
    });
  }
}
