export class Drug {
  constructor(
    public id: number = 0,
    public name: string = "",
    public insertedAt:  Date | string = ""
  ) {}
}