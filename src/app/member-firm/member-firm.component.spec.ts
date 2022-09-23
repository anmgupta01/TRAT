import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberFirmComponent } from './member-firm.component';

describe('MemberFirmComponent', () => {
  let component: MemberFirmComponent;
  let fixture: ComponentFixture<MemberFirmComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MemberFirmComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MemberFirmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
