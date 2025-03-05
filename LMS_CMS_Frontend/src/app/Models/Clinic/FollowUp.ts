export class FollowUp {
  constructor(
    public id: number,
    public schoolId: number,
    public gradeId: number,
    public classroomId: number,
    public studentId: number,
    public complains: string,
    public diagnosisId: number,
    public followUpDrugs: any[],
    public recommendation: string,
    public sendSMSToParent: boolean,
    public insertedAt: any,
    public insertedByUserId: number
  ) {}
}