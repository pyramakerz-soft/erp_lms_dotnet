import { TestBed } from '@angular/core/testing';

import { EvaluationTemplateGroupService } from './evaluation-template-group.service';

describe('EvaluationTemplateGroupService', () => {
  let service: EvaluationTemplateGroupService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EvaluationTemplateGroupService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
