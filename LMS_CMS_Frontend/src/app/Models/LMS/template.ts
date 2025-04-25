import { EvaluationTemplateGroups } from "./evaluation-template-groups";

export class Template {
    constructor(
        public id: number = 0,
        public englishTitle : string = '',
        public arabicTitle  : string = '',
        public weight: number = 0,
        public afterCount: number = 0,
        public insertedAt: string = '',
        public insertedByUserId: number = 0,
        public evaluationTemplateGroups : EvaluationTemplateGroups [] =[]
    ) {}
}
