export class FollowUp {
  constructor(
    public id: number = 0,
    public schoolId: number = 0,
    public school: string = "",
    public gradeId: number = 0,
     public grade: string ="",

    public classroomId: number  = 0,
    public classroom: string  = "",
    public studentId: number  = 0,
    public student: string  = "",
    public complains: string = "",
    public diagnosisId: number  = 0,
    public diagnosis: string  = "",
    public followUpDrugs: any[] = [],
    public recommendation: string = "",
    public sendSMSToParent: boolean = false,
    public insertedAt: string = "",
    public insertedByUserId: number  = 0
  ) {}
}