import { EvaluationTemplateGroups } from "./evaluation-template-groups";

export class Template {
    constructor(
        public id: number = 0,
        public englishTitle : string = '',
        public arabicTitle  : string = '',
        public weight: number | null = null,
        public afterCount: number | null = null,
        public insertedAt: string = '',
        public insertedByUserId: number = 0,
        public evaluationTemplateGroups : EvaluationTemplateGroups [] =[]
    ) {}
}
