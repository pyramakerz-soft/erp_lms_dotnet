// hygiene-form.model.ts
export class HygieneForm {
  constructor(
    public id: number,
    public schoolId: number,
    public school: string,
    public gradeId: number,
    public grade: string,
    public classRoomID: number,
    public classRoom: string,
    public insertedAt: string,
    public date: string,
    public studentHygieneTypes: any[]
  ) {}
}