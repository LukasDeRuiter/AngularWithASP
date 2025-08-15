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
    if (
      this.userInput !== null &&
      this.userInput !== ''
    ) {

      let currentHighestPosition = 0;

      if (this.messages.length > 0) {
        currentHighestPosition = Math.max(...this.messages.map(message => message.position));
      }

      const userMessage: Message = {
        id: 0,
        text: this.userInput,
        position: currentHighestPosition
      }

      this.messages.push(userMessage);

      this.http.post<Message>('/api/chat', { userInput: this.userInput }).subscribe(
        (result) => {
          this.messages.push(result);
          this.userInput = '';

          setTimeout(() => {
            const chatContainer = document.getElementById('chat-container');
            if (chatContainer) chatContainer.scrollTop = chatContainer.scrollHeight;
          }, 0);
        },
        (error) => {
          console.error(error);
        }
      );
    }
  }

  title = 'angularwithasp.client';
}
