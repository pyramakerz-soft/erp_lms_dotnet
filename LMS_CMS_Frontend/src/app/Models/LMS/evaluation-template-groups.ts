import { EvaluationEmployeeQuestionAdd } from "./evaluation-employee-question-add";
import { EvaluationTemplateGroupQuestion } from "./evaluation-template-group-question";

export class EvaluationTemplateGroups {
  constructor(
    public id: number = 0,
    public englishTitle: string = '',
    public arabicTitle: string = '',
    public evaluationTemplateID: number = 0,
    public insertedAt: string = '',
    public insertedByUserId: number = 0, 
    public evaluationTemplateGroupQuestions: EvaluationTemplateGroupQuestion[] = [],
    public isOpen: boolean = true,
    public evaluationEmployeeQuestions : EvaluationEmployeeQuestionAdd[] = [] 
  ) {}
}
