import { Component, OnInit, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
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
  styleUrl: './chat.component.css',
})
export class ChatComponent implements OnInit {

  public messages: Message[] = [];

  public userInput: string = '';
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

  sendMessage() {
    this.userInput = '';

    this.http.post<Message>('api/chat', this.userInput).subscribe(
      (result) => {
        console.log(result)
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'angularwithasp.client';
}
