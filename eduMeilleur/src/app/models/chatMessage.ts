export class ChatMessage {
    constructor(
        public text: string,
        public isUser: boolean,
        public timeStamp: Date
    ){}
}