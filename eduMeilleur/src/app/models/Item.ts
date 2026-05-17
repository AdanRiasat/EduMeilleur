export enum ItemType {
    Notes = "Notes",
    Exercises = "Exercises",
    Videos = "Videos"
}

export class Item{
    constructor(
        public id: number,
        public title: string,
        public content: string,
        public chapter: string,
        public relatedItemIds: number[],
        public type: ItemType,
        public relatedType: ItemType
    ) {}
}
