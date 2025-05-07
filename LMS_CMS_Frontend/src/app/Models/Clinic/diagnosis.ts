export class Diagnosis {
  constructor(
    public id: number,
    public name: string,
    public insertedAt: Date | string, 
    public insertedByUserId: number
  ) {}
}