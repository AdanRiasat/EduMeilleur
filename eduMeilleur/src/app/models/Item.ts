export enum ItemType {
    Notes = "Notes",
    Exercises = "Exercises",
    Videos = "Videos"
}

export class RelatedItem {
    constructor(
        public id: number,
        public code: string
    ) {}
}

export class Item{
    constructor(
        public id: number,
        public title: string,
        public content: string,
        public chapter: string,
        public relatedItems: RelatedItem[],
        public type: ItemType,
        public relatedType: ItemType,
        public code: string
    ) {}
}


