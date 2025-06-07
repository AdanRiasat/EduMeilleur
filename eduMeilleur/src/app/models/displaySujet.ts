export class DisplaySujet{
    constructor(
        public id: number,
        public name: string,
        public description: string,
        public type: string,
        public chapters: string[]
    ) {}
}