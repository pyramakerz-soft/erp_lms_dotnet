import { EvaluationEmployeeStudentBookCorrectionAdd } from "./evaluation-employee-student-book-correction-add";
import { EvaluationTemplateGroups } from "./evaluation-template-groups";

export class EvaluationEmployee {
    constructor(
        public id: number = 0,
        public date: string = '',
        public period: number = 0,
        public evaluatorEnglishName: string = '',
        public evaluatorArabicName: string = '',
        public evaluatedArabicName: string = '',
        public evaluatedEnglishName: string = '',
        public feedback: string = '',
        public evaluationTemplateEnglishTitle: string = '',
        public narration: string = '',
        public evaluatorID: number = 0,
        public evaluatedID: number = 0,
        public evaluationTemplateID: number = 0,
        public evaluationTemplateName: string = '',
        public classroomID: number = 0,
        public evaluationBookCorrectionID: number = 0,
        public evaluationEmployeeQuestionGroups: EvaluationTemplateGroups[] = [],
        public evaluationEmployeeStudentBookCorrections: EvaluationEmployeeStudentBookCorrectionAdd[] = []
    ) { }
}
