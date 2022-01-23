import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  showRegister: boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

  registerToggle() {
    this.showRegister = !this.showRegister;
  }

  cancelRegisterMode(event: boolean) {
    this.showRegister = event;
  }

}
