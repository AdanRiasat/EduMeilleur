export class Profile{
    constructor(
        public username: string,
        public bio: string,
        public school: string | null,
        public schoolId: number | null,
        public schoolYear: number | null,
        public iQPoints: number,
        public email: string
    ) {}
}