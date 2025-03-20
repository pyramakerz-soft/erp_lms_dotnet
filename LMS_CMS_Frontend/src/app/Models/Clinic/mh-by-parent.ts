export class MedicalHistoryByParent {
  id: number; // Unique identifier for the medical history entry
  details: string | null; // Description or details of the medical history
  permanentDrug: string | null; // Information about permanent drugs
  firstReport: string | null; // URL or path to the first attached report
  secReport: string | null; // URL or path to the second attached report
  insertedAt: string; // Timestamp when the record was inserted
  updatedAt: string | null; // Timestamp when the record was last updated

  constructor(
    id: number,
    details: string | null,
    permanentDrug: string | null,
    firstReport: string | null,
    secReport: string | null,
    insertedAt: string,
    updatedAt: string | null
  ) {
    this.id = id;
    this.details = details;
    this.permanentDrug = permanentDrug;
    this.firstReport = firstReport;
    this.secReport = secReport;
    this.insertedAt = insertedAt;
    this.updatedAt = updatedAt;
  }
}