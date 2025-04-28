import { EvaluationEmployeeQuestionAdd } from "./evaluation-employee-question-add";
import { EvaluationEmployeeStudentBookCorrectionAdd } from "./evaluation-employee-student-book-correction-add";

export class EvaluationEmployeeAdd {
    constructor(
        public id : number =0 ,
        public date: string = '',
        public period: number = 0,
        public evaluatorEnglishName: string = '',
        public evaluatorArabicName: string = '',
        public evaluatedArabicName: string = '',
        public evaluatedEnglishName: string = '',
        public narration: string = '',
        public evaluatorID: number = 0,
        public evaluatedID: number = 0,
        public evaluationTemplateID: number = 0,
        public evaluationTemplateName: string = '',
        public classroomID: number = 0, 
        public evaluationBookCorrectionID: number = 0, 
        public evaluationEmployeeQuestionsList: EvaluationEmployeeQuestionAdd[] = [],
        public evaluationEmployeeStudentBookCorrectionsList: EvaluationEmployeeStudentBookCorrectionAdd[] = [] 
    ) {}
}

