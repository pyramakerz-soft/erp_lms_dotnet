export class MedicalHistory {
  [key: string]: any;

  constructor(
    public id: number,
    public schoolId: number,
    public school: string = "",
    public gradeId: number,
    public grade: string = "",
    public classRoomID: number,
    public classRoom: string = "",
    public studentId: number,
    public student: string = "",
    public details: string,
    public permanentDrug: string,
    public insertedAt: string,
    public firstReport: File | null,
    public secReport: File | null,
    public updatedAt?: string,
    public doctorId?: number,
    public doctorName?: string
  ) {}
}