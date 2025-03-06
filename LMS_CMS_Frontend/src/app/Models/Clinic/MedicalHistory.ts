export class MedicalHistory {
  [key: string]: any;  // Fix for TS7053 error

  constructor(
    public id: number,
    public schoolId: number,
    public gradeId: number,
    public classRoomID: number,
    public studentId: number,
    public details: string,
    public permanentDrug: string,
    public insertedAt: string,
    public firstReport: File | null,
    public secReport: File | null
  ) {}
}
