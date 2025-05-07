import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DosesComponent } from './doses.component';

describe('DosesComponent', () => {
  let component: DosesComponent;
  let fixture: ComponentFixture<DosesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DosesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DosesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
