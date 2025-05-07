export class FollowUp {
  constructor(
    public id: number = 0,
  public schoolId: number | null = null,
    public school: string = "",
  public gradeId: number | null = null,
     public grade: string ="",

  public classroomId: number | null = null,
    public classroom: string  = "",
  public studentId: number | null = null,
    public student: string  = "",
    public complains: string = "",
  public diagnosisId: number | null = null,
    public diagnosis: string  = "",
  public followUpDrugs: { drugId: number, doseId: number }[] = [], 
    public recommendation: string = "",
    public sendSMSToParent: boolean = false,
    public insertedAt: string = "",
    public insertedByUserId: number  = 0
  ) {}
  
}

export interface FollowUpDrug {
  drugId: number;
  doseId: number;
}