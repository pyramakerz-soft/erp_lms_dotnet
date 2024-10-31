export class TokenData {
    constructor(
        public aud: string, 
        public exp: number,
        public id: number, 
        public iss: string,   
        public jti: string, 
        public sub: string, 
        public type: string, 
        public user_Name: string  
    ) {}
}