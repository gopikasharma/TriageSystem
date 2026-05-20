import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TriageForm } from "./components/triage-form/triage-form";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TriageForm],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('TriageSystem.Client');
}
