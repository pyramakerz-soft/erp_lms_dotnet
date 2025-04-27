import { EvaluationEmployeeQuestionAdd } from "./evaluation-employee-question-add";
import { EvaluationEmployeeStudentBookCorrectionAdd } from "./evaluation-employee-student-book-correction-add";

export class EvaluationEmployeeAdd {
    constructor(
        public date: string = '',
        public period: number = 0,
        public narration: string = '',
        public evaluatorID: number = 0,
        public evaluatedID: number = 0,
        public evaluationTemplateID: number = 0,
        public classroomID: number = 0, 
        public evaluationBookCorrectionID: number = 0, 
        public evaluationEmployeeQuestionsList: EvaluationEmployeeQuestionAdd[] = [],
        public evaluationEmployeeStudentBookCorrectionsList: EvaluationEmployeeStudentBookCorrectionAdd[] = [] 
    ) {}
}
