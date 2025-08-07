import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

interface Message {
  id: number;
  text: string;
  position: number;
}

@Component({
  selector: 'app-chat',
  standalone: false,
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})
export class ChatComponent implements OnInit {

  public messages: Message[] = [];
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getMessages();
  }

  getMessages() {
    this.http.get<Message[]>('/api/chat').subscribe(
      (result) => {
        this.messages = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'angularwithasp.client';
}
