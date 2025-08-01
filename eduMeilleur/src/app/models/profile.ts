export class Profile{
    constructor(
        public username: string,
        public bio: string,
        public school: string,
        public schoolId: number,
        public schoolYear: number,
        public iQPoints: number,
        public email: string
    ) {}
}