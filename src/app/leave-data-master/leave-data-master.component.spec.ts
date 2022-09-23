import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveDataMasterComponent } from './leave-data-master.component';

describe('LeaveDataMasterComponent', () => {
  let component: LeaveDataMasterComponent;
  let fixture: ComponentFixture<LeaveDataMasterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LeaveDataMasterComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeaveDataMasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
