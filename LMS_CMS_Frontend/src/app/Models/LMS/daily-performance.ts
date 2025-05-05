export class DailyPerformance {
  constructor(
    public id: number = 0,
    public studentID: number = 0,
    public performanceTypeID: number = 0,
    public subjectID: number = 0,
    public stars: number = 0,
    public insertedByUserId: number = 0
  ) {}
}
