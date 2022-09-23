import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberFirmLocationComponent } from './member-firm-location.component';

describe('MemberFirmLocationComponent', () => {
  let component: MemberFirmLocationComponent;
  let fixture: ComponentFixture<MemberFirmLocationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MemberFirmLocationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MemberFirmLocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
