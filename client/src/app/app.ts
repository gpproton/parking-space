import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatButton, MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MatButtonModule, MatDividerModule, MatIconModule, MatButton],
  template: `
    <h1>Test - {{title}}!</h1>
    <button matButton="elevated">Check Component</button>

    <router-outlet />
  `,
  styles: [],
})
export class App {
  protected title = 'Parking Space';
}
