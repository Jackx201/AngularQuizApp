import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { QuizService } from '../quiz.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private quizService : QuizService, private router: Router) { }

  ngOnInit(): void {
  }

  SignOut() {
    localStorage.clear();
    clearInterval(this.quizService.timer);
    this.router.navigate(['/register']);
  }

}
